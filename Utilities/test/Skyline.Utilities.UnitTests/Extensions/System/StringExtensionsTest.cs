using System;
using Xunit;

namespace Skyline.Utilities.UnitTests.System
{
    [Trait("扩展方法", "String扩展")]
    public class StringExtensionsTest
    {
        [Theory(DisplayName = "指示指定的字符串是 null 还是空字符串")]
        [InlineData(true, "")]
        [InlineData(true, null)]
        [InlineData(false, "test")]
        public void IsNullOrEmpty(bool isNullOrEmpty, string source)
        {
            Assert.True(source.IsNullOrEmpty() == isNullOrEmpty);
        }

        [Theory(DisplayName = "指示指定的字符串是 null、空还是仅由空白字符组成")]
        [InlineData(true, "")]
        [InlineData(true, null)]
        [InlineData(true, " ")]
        [InlineData(false, "test")]
        public void IsNullOrWhiteSpace(bool isNullOrWhiteSpace, string source)
        {
            Assert.True(source.IsNullOrWhiteSpace() == isNullOrWhiteSpace);
        }

        [Theory(DisplayName = "使用正则表达式验证是否匹配")]
        [InlineData(true, "abc123def")]
        [InlineData(false, "abcdef")]
        [InlineData(false, null)]
        public void IsMatch(bool isMatch, string source)
        {
            var pattern = "[0-9]+";
            Assert.True(source.IsMatch(pattern) == isMatch);
        }

        [Theory(DisplayName = "使用正则表达式验证匹配字符")]
        [InlineData("123", "abc123def")]
        [InlineData("", "abcdef")]
        public void Match(string target, string source)
        {
            var pattern = "[0-9]+";
            Assert.True(source.Match(pattern) == target);
        }

        [Fact(DisplayName = "计算字符串的MD5值 32位大写")]
        public void ToMd5()
        {
            string str = "123456";
            string expected = "E10ADC3949BA59ABBE56E057F20F883E";
            var md5 = str.ToMd5();
            Assert.True(md5 == expected);
        }

        [Fact(DisplayName = "计算字符串的MD5值 32位小写")]
        public void ToMd5Lower()
        {
            string str = "123456";
            string expected = "e10adc3949ba59abbe56e057f20f883e";
            var md5 = str.ToMd5(true);
            Assert.True(md5 == expected);
        }
    }
}
