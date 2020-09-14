using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Skyline.Tests.Extensions.System
{
    public class ObjectExtensionTest
    {
        [Fact]
        public void ToJson()
        {
            var user = new User { Id = 1, Name = "张三" };
            var expected = "{\"Id\":1,\"Name\":\"张三\"}";
            var result = user.ToJson();
            Assert.Equal(expected, result);
        }

        class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
