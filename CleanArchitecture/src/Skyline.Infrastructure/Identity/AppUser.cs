using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Skyline.Infrastructure.Identity
{
    // Add profile data for application users by adding properties to the AppUser class
    public class AppUser : IdentityUser
    {
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public DateTime? DOB { get; set; }
    }
}
