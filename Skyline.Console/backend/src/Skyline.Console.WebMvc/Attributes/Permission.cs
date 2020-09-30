using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.Console.WebMvc
{
    public class CanAccess
    {
        public string Controller { get; set; }
        public List<string> Actions { get; set; }
    }
    public class OwnedPermission
    {
        public List<CanAccess> CanAccesses { get; set; }
        public OwnedPermission()
        {
            CanAccesses = new List<CanAccess>();
        }

        public bool Can(string controller, string action)
        {
            if (controller.IsNullOrWhiteSpace() || action.IsNullOrWhiteSpace())
            {
                return false;
            }
            var ctrl = CanAccesses.Where(x => string.Equals(controller, x.Controller, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault(x => x.Controller == controller);
            if (ctrl.IsNull())
            {
                return false;
            }
            return ctrl.Actions.Contains(action, StringComparer.OrdinalIgnoreCase);
        }
    }
}
