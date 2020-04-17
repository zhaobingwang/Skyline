using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Skyline.Utilities.UnitTests.Extensions.System
{
    [Trait("System扩展", "System.Boolean")]
    public class BooleanExtensionsTest
    {
        [Fact(DisplayName = "True的描述信息返回是")]
        public void Description_ReturnYes_WithTrue()
        {
            Assert.True(true.Description() == "是");
        }

        [Fact(DisplayName = "False的描述信息返回否")]
        public void Description_ReturnNo_WithTrue()
        {
            Assert.True(false.Description() == "否");
        }

        [Fact(DisplayName = "Null Boolean的描述信息返回空字符串")]
        public void Description_ReturnEmpty_WithNull()
        {
            bool? flag = null;
            Assert.True(flag.Description() == "");
        }
    }
}
