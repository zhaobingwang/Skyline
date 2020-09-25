using Skyline.Console.ApplicationCore.BO;
using Skyline.Console.ApplicationCore.Entities;
using Skyline.Console.ApplicationCore.Interfaces;
using Skyline.Console.ApplicationCore.Specifications;
using Skyline.Console.ApplicationCore.VO;
using Skyline.Console.WebMvc.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Console.ApplicationCore.Services
{
    public class RoleService : ISkylineAutoDependence
    {
        private readonly IAsyncRepository<Role> roleRepository;
        public RoleService(IAsyncRepository<Role> roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<LayuiTablePageVO> GetRolesAsync(int page, int limit, string keyword)
        {
            var findRoleSpec = new FindRoleSpecification(page, limit, keyword);
            var countRoleSpec = new CountRoleSpecification(keyword);

            var roleEntities = await roleRepository.ListAsync(findRoleSpec);
            var roelCount = await roleRepository.CountAsync(countRoleSpec);
            var vo = ToRoleTableVO(roleEntities);
            return new LayuiTablePageVO(vo, roelCount, 1);
        }

        public async Task<IEnumerable<RoleTableVO>> GetRolesAsync()
        {
            var roles = await roleRepository.ListAsync(new FindRoleSpecification(Enums.Status.Normal, Enums.IsDeleted.No));
            var vo = ToRoleTableVO(roles);
            return vo;
        }

        public async Task<BizServiceResponse> AddAsync(AddOrEditRoleVO vo, Guid currentUserId, string currentUserName)
        {
            var existRole = await roleRepository.FirstOrDefaultAsync(new FindRoleSpecification(code: vo.Code));
            if (existRole != null)
                return new BizServiceResponse(BizServiceResponseCode.Failed, "已存在相同编码的角色");

            var now = DateTime.UtcNow;
            var entity = new Role
            {
                Code = vo.Code,
                Name = vo.Name,
                Status = vo.Status,
                IsSuperAdministrator = false,
                Builtin = false,
                CreateTime = now,
                CreateUserGuidId = currentUserId,
                CreateUserName = currentUserName,
                Description = vo.Description,
                IsDeleted = Enums.IsDeleted.No,
                ModifiyUserId = currentUserId,
                ModifyTime = now,
                ModifyUserName = currentUserName,
            };
            var addResult = await roleRepository.AddAsync(entity);
            if (addResult == null)
                return new BizServiceResponse(BizServiceResponseCode.Failed, "添加角色失败");
            else
                return new BizServiceResponse(BizServiceResponseCode.Success, "添加角色成功");
        }

        public async Task<AddOrEditRoleVO> GetAddOrEditRoleVOAsync(string code)
        {
            var result = await roleRepository.FirstOrDefaultAsync(new FindRoleSpecification(code));
            if (result == null)
                return null;
            return new AddOrEditRoleVO
            {
                Code = result.Code,
                Name = result.Name,
                Status = result.Status,
                Description = result.Description
            };
        }

        public async Task<BizServiceResponse> EditAsync(AddOrEditRoleVO vo, Guid currentUserId, string currentUserName)
        {
            var existRole = await roleRepository.FirstOrDefaultAsync(new FindRoleSpecification(code: vo.Code));
            if (existRole == null)
                return new BizServiceResponse(BizServiceResponseCode.Failed, "不存在该角色");

            var now = DateTime.UtcNow;

            existRole.Name = vo.Name;
            existRole.Description = vo.Description;
            existRole.Status = vo.Status;
            existRole.ModifiyUserId = currentUserId;
            existRole.ModifyTime = now;
            existRole.ModifyUserName = currentUserName;

            var addResult = await roleRepository.UpdateAsync(existRole);
            if (addResult)
                return new BizServiceResponse(BizServiceResponseCode.Success, "添加角色成功");
            else
                return new BizServiceResponse(BizServiceResponseCode.Failed, "添加角色失败");
        }

        private IEnumerable<RoleTableVO> ToRoleTableVO(IEnumerable<Role> entities)
        {
            var vo = new List<RoleTableVO>();
            foreach (var entity in entities)
            {
                vo.Add(new RoleTableVO
                {
                    Code = entity.Code,
                    Name = entity.Name,
                    Status = entity.Status,
                    IsDeleted = entity.IsDeleted,
                    IsSuper = entity.IsSuperAdministrator,
                    Builtin = entity.Builtin,
                    LastModifyTime = entity.ModifyTime ?? default,
                    LastModifyUserLoginName = entity.ModifyUserName,
                    Description = entity.Description
                });
            }
            return vo;
        }
    }
}
