
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Skyline.Tests.Extensions.Skyline
{
    public class ValidateExtensionsTest
    {
        [Fact]
        public void CheckNull_ThrowEx_WithNull()
        {
            object hereIsNullobj = null;
            var paramName = Assert.Throws<ArgumentNullException>(() => hereIsNullobj.CheckNull(nameof(hereIsNullobj))).ParamName;
            Assert.True(paramName == nameof(hereIsNullobj));
        }
    }
}
