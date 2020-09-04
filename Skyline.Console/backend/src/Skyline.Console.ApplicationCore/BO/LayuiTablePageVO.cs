using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.Console.WebMvc.Models
{
    public class LayuiTablePageVO
    {
        public LayuiTablePageVO()
        {

        }
        public LayuiTablePageVO(IEnumerable data, int totalCount, int code, string msg = null)
        {
            Data = data;
            Count = totalCount;
        }

        public int Code { get; set; }
        public string Msg { get; set; }
        public int Count { get; set; }
        public object Data { get; set; }
    }
}
