using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.VO
{
    public class LayuiTransferVO
    {
        public IEnumerable<LayuiTransferDataVO> Data { get; set; }
        public string[] Selected { get; set; }
    }
    public class LayuiTransferDataVO
    {
        public string Value { get; set; }
        public string Title { get; set; }
        public bool Disabled { get; set; } = false;
        public bool Checked { get; set; } = false;
        public bool Selected { get; set; }
    }
}
