﻿using Skyline.Console.ApplicationCore.BO;
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
