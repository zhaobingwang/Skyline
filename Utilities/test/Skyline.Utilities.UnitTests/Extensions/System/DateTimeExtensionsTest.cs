using System;
using Xunit;

namespace Skyline.Utilities.UnitTests.System
{
    [Trait("扩展方法", "System.DateTime")]
    public class DateTimeExtensionsTest
    {
        #region 时间格式 测试
        [Fact(DisplayName = "转中文日期格式-成功测试")]
        public void ToChineseDateStringShouldSuccess()
        {
            var datetime = Convert.ToDateTime("2019-12-11 23:10:10");
            var expected = "2019年12月11日";

            DateTime? nullDateTime1 = null;
            DateTime? nullDateTime2 = datetime;

            Assert.True(datetime.ToChineseDateString() == expected);
            Assert.True(nullDateTime1.ToChineseDateString() == string.Empty);
            Assert.True(nullDateTime2.ToChineseDateString() == expected);
        }

        [Fact(DisplayName = "转中文日期时间格式-成功测试")]
        public void ToChineseDateTimeStringShouldSuccess()
        {
            var datetime = Convert.ToDateTime("2019-12-11 23:10:10");
            var expected = "2019年12月11日23时10分10秒";

            DateTime? nullDateTime1 = null;
            DateTime? nullDateTime2 = datetime;

            Assert.True(datetime.ToChineseDateTimeString() == expected);
            Assert.True(nullDateTime1.ToChineseDateTimeString() == string.Empty);
            Assert.True(nullDateTime2.ToChineseDateTimeString() == expected);
        }

        [Fact(DisplayName = "转为yyyy-MM-dd日期格式-成功测试")]
        public void ToDateStringShouldSuccess()
        {
            var datetime = Convert.ToDateTime("2019-12-11 23:10:10");
            var expected = "2019-12-11";

            DateTime? nullDateTime1 = null;
            DateTime? nullDateTime2 = datetime;

            Assert.True(datetime.ToDateString() == expected);
            Assert.True(nullDateTime1.ToDateString() == string.Empty);
            Assert.True(nullDateTime2.ToDateString() == expected);
        }

        [Fact]
        public void ToDateTimeString_ShouldSuccess()
        {
            var datetime = Convert.ToDateTime("2019-12-11 23:10:10");
            var expected = "2019-12-11 23:10:10";
            var expectedWithOutSeconds = "2019-12-11 23:10";

            DateTime? nullDateTime1 = null;
            DateTime? nullDateTime2 = datetime;

            Assert.True(datetime.ToDateTimeString() == expected);
            Assert.True(nullDateTime1.ToDateTimeString() == string.Empty);
            Assert.True(nullDateTime2.ToDateTimeString() == expected);

            Assert.True(datetime.ToDateTimeString(false) == expectedWithOutSeconds);
            Assert.True(nullDateTime2.ToDateTimeString(false) == expectedWithOutSeconds);
        }
        #endregion

        #region 友好时间提示 测试
        [Theory(DisplayName = "转为友好的时间信息-成功测试")]
        [InlineData("刚刚", 0)]
        [InlineData("刚刚", 59)]
        [InlineData("1分钟前", 60)]
        [InlineData("1分钟前", 119)]
        [InlineData("2分钟前", 60 * 2)]
        [InlineData("2分钟前", 60 * 2 + 59)]
        [InlineData("1小时前", 60 * 60)]
        [InlineData("1小时前", 60 * 60 * 2 - 1)]
        [InlineData("2小时前", 60 * 60 * 2)]
        [InlineData("2小时前", 60 * 60 * 3 - 1)]
        [InlineData("3小时前", 60 * 60 * 3)]
        [InlineData("3小时前", 60 * 60 * 4 - 1)]
        [InlineData("昨天", 60 * 60 * 24)]
        [InlineData("1天前", 60 * 60 * 24 + 1)]
        [InlineData("1天前", 60 * 60 * 24 * 2 - 1)]
        [InlineData("2天前", 60 * 60 * 24 * 2)]
        [InlineData("6天前", 60 * 60 * 24 * 6)]
        [InlineData("6天前", 60 * 60 * 24 * 7 - 1)]
        [InlineData("上周", 60 * 60 * 24 * 7)]
        [InlineData("上周", 60 * 60 * 24 * 14 - 1)]
        [InlineData("2周前", 60 * 60 * 24 * 14)]
        [InlineData("2周前", 60 * 60 * 24 * 21 - 1)]
        [InlineData("3周前", 60 * 60 * 24 * 21)]
        [InlineData("3周前", 60 * 60 * 24 * 28 - 1)]
        [InlineData("上个月前", 60 * 60 * 24 * 28)]
        [InlineData("上个月前", 60 * 60 * 24 * 60 - 1)]
        [InlineData("2个月前", 60 * 60 * 24 * 60)]
        [InlineData("2个月前", 60 * 60 * 24 * 90 - 1)]
        [InlineData("3个月前", 60 * 60 * 24 * 90)]
        [InlineData("3个月前", 60 * 60 * 24 * 120 - 1)]
        [InlineData("12个月前", 60 * 60 * 24 * 365 - 1)]
        [InlineData("去年", 60 * 60 * 24 * 365)]
        [InlineData("去年", 60 * 60 * 24 * 730 - 1)]
        [InlineData("2年前", 60 * 60 * 24 * 365 * 2)]
        [InlineData("2年前", 60 * 60 * 24 * 365 * 3 - 1)]
        [InlineData("3年前", 60 * 60 * 24 * 365 * 3)]
        public void ToFriendlyDateStringShouldSuccess(string friendlyInfo, int beSubtractSeconds)
        {
            var publishTime = new DateTime(2019, 12, 12, 18, 30, 0);
            var now = new DateTime(2019, 12, 12, 18, 30, 0);

            Assert.True(publishTime.AddSeconds(-1 * beSubtractSeconds).ToFriendlyDateString(now) == friendlyInfo);
        }
        #endregion

        #region unix时间戳 测试
        [Fact]
        public void ToUnixTimeSeconds_ShouldSuccess()
        {
            var dateTime = new DateTime(2020, 4, 24, 12, 10, 1);
            double expected = 1587701401D;
            Assert.True(dateTime.ToUnixTimeSeconds() == expected);
        }

        [Fact]
        public void ToUnixTimeSeconds_ShouldSuccess_WithNullable()
        {
            DateTime? dateTime = new DateTime(2020, 4, 24, 12, 10, 1);
            DateTime? dateTime2 = null;
            double expected = 1587701401D;
            double expected2 = 0D;
            Assert.True(dateTime.ToUnixTimeSeconds() == expected);
            Assert.True(dateTime2.ToUnixTimeSeconds() == expected2);
        }

        [Fact]
        public void ToUnixTimeMilliseconds_ShouldSuccess()
        {
            var dateTime = new DateTime(2020, 4, 24, 12, 10, 1);
            double expected = 1587701401000D;
            Assert.True(dateTime.ToUnixTimeMilliseconds() == expected);
        }

        [Fact]
        public void ToUnixTimeMilliseconds_ShouldSuccess_WithNullable()
        {
            DateTime? dateTime = new DateTime(2020, 4, 24, 12, 10, 1);
            DateTime? dateTime2 = null;
            double expected = 1587701401000D;
            double expected2 = 0D;
            Assert.True(dateTime.ToUnixTimeMilliseconds() == expected);
            Assert.True(dateTime2.ToUnixTimeMilliseconds() == expected2);
        }
        #endregion

        #region 时间比较和判断 测试
        [Theory(DisplayName = "是否早于指定时间")]
        [InlineData(true, 1)]
        [InlineData(false, 0)]
        [InlineData(false, -1)]
        public void IsBefore(bool isBefore, int beAddSeconds)
        {
            var now = DateTime.Now;
            var target = now.AddSeconds(beAddSeconds);
            Assert.True(now.IsBefore(target) == isBefore);
        }

        [Theory(DisplayName = "是否晚于指定时间")]
        [InlineData(false, 1)]
        [InlineData(false, 0)]
        [InlineData(true, -1)]
        public void IsAfter(bool isBefore, int beAddSeconds)
        {
            var now = DateTime.Now;
            var target = now.AddSeconds(beAddSeconds);
            Assert.True(now.IsAfter(target) == isBefore);
        }

        [Theory(DisplayName = "是否是周末")]
        [InlineData(false, 0)]
        [InlineData(false, 1)]
        [InlineData(false, 2)]
        [InlineData(false, 3)]
        [InlineData(false, 4)]
        [InlineData(true, 5)]
        [InlineData(true, 6)]
        public void IsWeekend(bool isWeekend, int beAddDays)
        {
            DateTime monday = new DateTime(2020, 4, 13);
            Assert.True(monday.AddDays(beAddDays).IsWeekend() == isWeekend);
        }

        [Theory(DisplayName = "是否是工作日")]
        [InlineData(true, 0)]
        [InlineData(true, 1)]
        [InlineData(true, 2)]
        [InlineData(true, 3)]
        [InlineData(true, 4)]
        [InlineData(false, 5)]
        [InlineData(false, 6)]
        public void IsWeekDay(bool isWeekend, int beAddDays)
        {
            DateTime monday = new DateTime(2020, 4, 13);
            Assert.True(monday.AddDays(beAddDays).IsWeekday() == isWeekend);
        }

        [Theory(DisplayName = "是否是今天")]
        [InlineData(true, 0)]
        [InlineData(false, 1)]
        [InlineData(false, -1)]
        public void IsToday(bool isToday, int beAddDays)
        {
            DateTime today = new DateTime(2019, 12, 16, 23, 18, 12);
            var source = today.AddDays(beAddDays);

            Assert.True(source.AddDays(beAddDays).IsToday(today) == isToday);
        }

        [Theory]
        [InlineData(false, "1900-1-1")]
        [InlineData(true, "2000-1-1")]
        [InlineData(true, "1996-1-1")]
        [InlineData(true, "2004-1-1")]
        public void IsLeapYear(bool isLeapYear, string dateTimeVal)
        {
            var dateTime = Convert.ToDateTime(dateTimeVal);
            Assert.True(isLeapYear == dateTime.IsLeapYear());
        }
        #endregion

        #region 获取天数
        [Theory]
        [InlineData(364, "1900-1-1")]
        [InlineData(364, "2019-1-1")]
        [InlineData(365, "2020-1-1")]
        [InlineData(0, "2020-12-31")]
        public void RemainingDaysOfYear(int days, string dateTimeVal)
        {
            var dateTime = Convert.ToDateTime(dateTimeVal);
            Assert.True(days == dateTime.RemainingDaysOfYear());
        }
        [Theory]
        [InlineData(6, "2020-4-24")]
        [InlineData(28, "2020-2-1")]
        [InlineData(30, "2020-1-1")]
        public void RemainingDaysOfMonth(int days, string dateTimeVal)
        {
            var dateTime = Convert.ToDateTime(dateTimeVal);
            Assert.True(days == dateTime.RemainingDaysOfMonth());
        }
        #endregion

        #region 特殊时间
        [Fact(DisplayName = "转为当天开始时间-成功测试")]
        public void ToStartOfDayShouldSuccess()
        {
            var dateTime = new DateTime(2019, 12, 12, 16, 24, 12);
            var expected = new DateTime(2019, 12, 12, 0, 0, 0);
            Assert.True(dateTime.ToStartOfDay() == expected);
        }

        [Fact(DisplayName = "转为当天结束时间-成功测试")]
        public void ToEndOfDayShouldSuccess()
        {
            var dateTime = new DateTime(2019, 12, 12, 16, 24, 12);
            var expected = new DateTime(2019, 12, 12, 23, 59, 59);
            Assert.True(dateTime.ToEndOfDay() == expected);
        }
        #endregion
    }
}
