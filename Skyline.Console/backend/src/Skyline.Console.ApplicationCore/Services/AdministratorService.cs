using Skyline.Console.ApplicationCore.Entities;
using Skyline.Console.ApplicationCore.Enums;
using Skyline.Console.ApplicationCore.Interfaces;
using Skyline.Console.ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Console.ApplicationCore.Services
{
    public class AdministratorService : ISkylineAutoDependence
    {
        private readonly IAsyncRepository<User> _userRepository;
        public AdministratorService(IAsyncRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> LoginCheckAsync(string loginName, string password)
        {
            var loginCheckSpec = new LoginCheckSpecification(loginName, password);
            var user = await _userRepository.FirstOrDefaultAsync(loginCheckSpec);
            if (user == null)
                return false;
            if (user.Status != Status.Normal)
                return false;
            if (user.IsDeleted == IsDeleted.Yes)
                return false;
            return true;
        }
    }
}
