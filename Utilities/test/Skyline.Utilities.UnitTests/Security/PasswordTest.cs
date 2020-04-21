using System;
using System.Collections.Generic;
using System.Text;
using Skyline.Utilities.Security;
using Xunit;

namespace Skyline.Utilities.UnitTests.Security
{
    [Trait("安全", "密码")]
    public class PasswordTest
    {
        [Fact(DisplayName = "产生一个密码-正常测试")]
        public void GenerateShouldSuccess()
        {
            string punctuations = "!@#$%^&*()_-+=[{]};:>|./?";
            int len = 10;
            int numberOfNonAlphanumericCharacters = 4;
            var password = Password.Generate(len, numberOfNonAlphanumericCharacters);
            var pwdArray = password.ToCharArray();

            int _tmpAlphanumericCharacter = 0;
            foreach (var val in pwdArray)
            {
                if (punctuations.IndexOf(val) < 0)
                    _tmpAlphanumericCharacter += 1;

            }
            Assert.True(password?.Length == len);
            Assert.True(_tmpAlphanumericCharacter == len - numberOfNonAlphanumericCharacters);
        }
    }
}
