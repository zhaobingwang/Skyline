using Skyline.Console.ApplicationCore.Interfaces;
using Skyline.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.Services
{
    public class CommonService : ISkylineAutoDependence
    {
        public CommonService()
        {

        }

        public IDictionary<int, string> GetEnumDict<TEnum>() where TEnum : struct
        {
            return EnumUtil.GetDictionary<TEnum>();
        }
    }
}
