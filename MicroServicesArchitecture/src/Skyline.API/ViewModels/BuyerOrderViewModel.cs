using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.API.ViewModels
{
    public class BuyerOrderViewModel
    {
        public string BuyerId { get; set; }
        public string BuyerName { get; set; }

        public int ItemCount { get; set; }
        // etc...
    }
}
