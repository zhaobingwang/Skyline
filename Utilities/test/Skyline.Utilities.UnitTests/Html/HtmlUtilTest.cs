using Skyline.Utilities.Html;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Skyline.Utilities.UnitTests.Html
{
    [Trait("HTML工具类", "数据清洗")]
    public class HtmlUtilTest
    {
        [Fact(DisplayName = "HTML数据清洗-成功测试")]
        public void SanitizeHtml_ShouldSuccess_WithExpectedParameters()
        {
            var html = @"<script>alert('xss')</script><div onload=""alert('xss')"""
                + @"style=""background-color: rgba(255,0,0,0.3);"">Test<img src=""test.gif"""
                + @"style=""background-image: url(javascript:alert('xss')); margin: 10px""></div>";

            var expectedHtml = @"<div style=""background-color: rgba(255, 0, 0, 0.3)"">"
                + @"Test<img src=""test.gif"" style=""margin: 10px""></div>";

            var sanitizedHtml = HtmlUtil.SanitizeHtml(html);

            Assert.True(expectedHtml == sanitizedHtml);
        }
    }
}
