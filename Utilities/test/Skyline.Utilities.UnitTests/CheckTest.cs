using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Skyline.Utilities;

namespace Skyline.Utilities.UnitTests
{
    public class CheckTest
    {
        [Fact]
        public void IsNullOrEmpty_ThrowNullException_WithNull()
        {
            string str = null;
            Assert.Throws<ArgumentException>(() => Check.IsNullOrEmpty(str, nameof(str)));
        }

        [Fact]
        public void IsNullOrWhiteSpace_ThrowNullException_WithNull()
        {
            string str = null;
            Assert.Throws<ArgumentException>(() => Check.IsNullOrWhiteSpace(str, nameof(str)));
        }

        [Fact]
        public void IsNull_ThrowNullException_WithNullObject()
        {
            Sample sample = null;
            Assert.Throws<ArgumentNullException>(() => Check.IsNull(sample, nameof(sample)));
        }

        [Fact]
        public void IsInRange_ThrowArgumentException_WithOutRangeParameters()
        {
            string str = "hello,world.";    // len = 12
            Assert.Throws<ArgumentException>(() => Check.IsInRange(str, nameof(str), 11, 2));
        }

        [Fact]
        public void IsInRange_ReturnSourceValue_WithInRangeParameters()
        {
            string str = "hello,world.";    // len = 12
            var result = Check.IsInRange(str, nameof(str), 12, 2);
            Assert.True(str == result);
        }
    }
}
