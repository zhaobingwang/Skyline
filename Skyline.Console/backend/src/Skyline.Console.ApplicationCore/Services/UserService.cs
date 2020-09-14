using Skyline.Console.ApplicationCore.BO;
using Skyline.Console.ApplicationCore.Constants;
using Skyline.Console.ApplicationCore.Entities;
using Skyline.Console.ApplicationCore.Enums;
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
    /// <summary>
    /// 管理员服务
    /// </summary>
    public class UserService : ISkylineAutoDependence
    {
        private readonly IAsyncRepository<User> _userRepository;
        public UserService(IAsyncRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LayuiTablePageVO> GetAllUsersAsync(int page, int limit, string keyword)
        {
            var userSpec = new FindUserSpecification(page, limit, keyword);
            var countSpec = new CountUserSpecification(keyword);

            var userEntities = await _userRepository.ListAsync(userSpec);
            var totalCount = await _userRepository.CountAsync(countSpec);
            var vo = ToMenuVO(userEntities);
            return new LayuiTablePageVO(vo, totalCount, 1);
        }

        public async Task<EditUserVO> GetUserVOAsync(Guid guid)
        {
            var userSpec = new FindUserSpecification(guid);
            var entity = await _userRepository.FirstOrDefaultAsync(userSpec);
            return new EditUserVO
            {
                DOB = entity.DOB,
                Id = entity.Guid,
                LoginName = entity.LoginName,
                NickName = entity.NickName,
                Type = entity.Type
            };
        }

        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<BizServiceResponse> LoginCheckAsync(string loginName, string password)
        {
            var loginCheckSpec = new LoginCheckSpecification(loginName, password);
            var user = await _userRepository.FirstOrDefaultAsync(loginCheckSpec);
            if (user == null)
                return BizServiceResponse.IsFailed(AccountConst.USER_NOT_EXIST);
            if (user.Status != Status.Normal)
                return BizServiceResponse.IsFailed(AccountConst.USER_STATUS_ABNORMAL);
            if (user.IsDeleted == IsDeleted.Yes)
                return BizServiceResponse.IsFailed(AccountConst.USER_DELETED);
            return BizServiceResponse.IsSuccess(AccountConst.VALID_SUCCESS, ToAdministratorBO(user));
        }

        public async Task<BizServiceResponse> AddUserAsync(AddUserVO vo, Guid currentUserId, string currentUserLoginName)
        {
            if (vo.Type == UserType.SuperAdmin)
                return new BizServiceResponse(BizServiceResponseCode.Failed, "添加用户失败：不允许添加超级管理员");
            var user = await _userRepository.ListAsync(new FindUserSpecification(vo.LoginName));
            if (user != null && user.Count > 0)
                return new BizServiceResponse(BizServiceResponseCode.Failed, "添加用户失败：用户已存在");
            // TODO
            var now = DateTime.UtcNow;
            var entity = new User
            {
                Guid = Guid.NewGuid(),
                Avatar = "",
                CreateTime = now,
                CreateUserId = currentUserId,
                CreateUserName = currentUserLoginName,
                LastModifyTime = now,
                LastModifyUserId = currentUserId,
                LastModifyUserName = currentUserLoginName,
                Description = null,
                Status = Status.Normal,
                DOB = vo.DOB,
                IsDeleted = IsDeleted.No,
                LoginName = vo.LoginName,
                NickName = vo.NickName,
                PasswordHash = vo.Password.MD5Hash(),
                Type = vo.Type
            };
            var addResult = await _userRepository.AddAsync(entity);
            if (addResult.IsNull())
                return new BizServiceResponse(BizServiceResponseCode.Failed, "添加用户失败");
            else
                return new BizServiceResponse(BizServiceResponseCode.Success, "添加用户成功");
        }

        public async Task<BizServiceResponse> EditAsync(EditUserVO vo, Guid currentUserId, string currentUserLoginName)
        {
            var now = DateTime.UtcNow;
            var user = await _userRepository.FirstOrDefaultAsync(new FindUserSpecification(vo.LoginName));
            if (user == null)
                return new BizServiceResponse(BizServiceResponseCode.Failed, "修改用户失败：用户不存在");
            // TODO
            if (user.Type != UserType.SuperAdmin && vo.Type == UserType.SuperAdmin)
                return new BizServiceResponse(BizServiceResponseCode.Failed, "修改用户失败：不允许修改为超级管理员");

            //user.LoginName = vo.LoginName;
            user.NickName = vo.NickName;
            user.Type = vo.Type;
            if (vo.Password.IsNotNullOrWhiteSpace())
                user.PasswordHash = vo.Password.MD5Hash();
            user.DOB = vo.DOB;

            var addResult = await _userRepository.UpdateAsync(user);
            if (addResult)
                return new BizServiceResponse(BizServiceResponseCode.Success, "修改用户成功");
            else
                return new BizServiceResponse(BizServiceResponseCode.Failed, "修改用户失败");
        }

        public async Task<BizServiceResponse> DeleteAsync(Guid id)
        {
            var user = await _userRepository.FirstOrDefaultAsync(new FindUserSpecification(id));
            if (user == null)
                return new BizServiceResponse(BizServiceResponseCode.Failed, "删除用户失败：用户不存在");
            var deleted = await _userRepository.DeleteAsync(user);
            if (deleted)
            {
                return new BizServiceResponse(BizServiceResponseCode.Success, "删除用户成功");
            }
            else
            {
                return new BizServiceResponse(BizServiceResponseCode.Failed, "删除用户失败");
            }
        }

        // TODO: add automapper
        private UserBO ToAdministratorBO(User entity)
        {
            return new UserBO
            {
                Id = entity.Guid,
                Avatar = entity.Avatar,
                DOB = entity.DOB,
                UserType = entity.Type,
                LoginName = entity.LoginName,
                NickName = entity.NickName
            };
        }

        private IEnumerable<UserVO> ToMenuVO(IEnumerable<User> entities)
        {
            var vo = new List<UserVO>();
            foreach (var entity in entities)
            {
                vo.Add(new UserVO
                {
                    id = entity.Guid,
                    loginName = entity.LoginName,
                    nickName = entity.NickName,
                    DOB = entity.DOB.Value,
                    Type = entity.Type,
                    status = entity.Status,
                    IsDeleted = entity.IsDeleted,
                    lastModifyTime = entity.LastModifyTime,
                    lastModifyUserLoginName = entity.LastModifyUserName
                });
            }
            return vo;
        }
    }
}
