using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Skyline.Elasticsearch
{
    public class ESConfig : IOptions<ESConfig>
    {
        public List<string> Urls { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ESConfig Value => this;
    }
}
