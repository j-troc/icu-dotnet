using System;
using NUnit.Framework;
using Icu;
using System.Globalization;
using System.Linq;


namespace Icu.Tests
{
	[TestFixture]
	class CalendarTests
	{

		[Test]
		public void GetTimeZoneDisplayNameTest()
		{
			var timezone = new TimeZone("AST");
			var cal = new GregorianCalendar(timezone);

			var displayName = cal.GetTimeZoneDisplayName(Calendar.UCalendarDisplayNameType.Standard);

			Assert.AreEqual("Alaska Standard Time", displayName);
		}

		[Test]
		public void ClearTest()
		{
			var cal = new GregorianCalendar();
			cal.Month = Calendar.UCalendarMonths.September;
			cal.DayOfMonth = 4;

			cal.Clear();

			Assert.AreEqual(Calendar.UCalendarMonths.January, cal.Month);
			Assert.AreEqual(1, cal.DayOfMonth);
		}

		[Test]
		public void ClearFieldTest()
		{
			var cal = new GregorianCalendar();
			cal.Month = Calendar.UCalendarMonths.September;

			cal.Clear(Calendar.UCalendarDateFields.Month);

			Assert.AreEqual(Calendar.UCalendarMonths.January, cal.Month);
		}

		[Test]
		public void CloneTest()
		{
			var cal1 = new GregorianCalendar();

			cal1.DayOfMonth = 5;
			var cal2 = cal1.Clone();
			cal2.DayOfMonth = 10;

			Assert.AreEqual(5, cal1.DayOfMonth);
		}

		[Test]
		public void RollTest()
		{
			var cal = new GregorianCalendar();
			var startMonth = cal.Month;

			cal.Roll(Calendar.UCalendarDateFields.DayOfMonth, 100);

			Assert.AreEqual(startMonth, cal.Month);
		}

		[Test]
		public void SetTest()
		{
			var cal = new GregorianCalendar(new TimeZone("UTC"));
			cal.Clear();

			cal.Set(Calendar.UCalendarDateFields.DayOfYear, 2);

			Assert.AreEqual(24 * 60 * 60 * 1000, cal.GetTime());
		}

		[Test]
		public void GetTest()
		{
			var cal = new GregorianCalendar(new TimeZone("UTC"));
			var expected = 15;
			cal.Set(Calendar.UCalendarDateFields.DayOfYear, expected);

			var result = cal.Get(Calendar.UCalendarDateFields.DayOfYear);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void FieldDifferenceTest()
		{
			var cal = new GregorianCalendar();
			var time = cal.GetTime();

			cal.Add(Calendar.UCalendarDateFields.Hour, 2);
			cal.Add(Calendar.UCalendarDateFields.Minute, 2);

			var difference = cal.FieldDifference(time, Calendar.UCalendarDateFields.Minute);

			Assert.AreEqual(time, cal.GetTime());
			Assert.AreEqual(-122, difference);
		}

		[Test]
		public void IsSetTest()
		{
			var cal = new GregorianCalendar();
			cal.Month = Calendar.UCalendarMonths.September;
			cal.DayOfMonth = 4;

			var setBefore = cal.IsSet(Calendar.UCalendarDateFields.Month);
			cal.Clear();
			var setAfter = cal.IsSet(Calendar.UCalendarDateFields.Month);

			Assert.AreEqual(true, setBefore);
			Assert.AreEqual(false, setAfter);
		}

		[Test]
		public void InDaylightTime()
		{
			var cal = new GregorianCalendar(new TimeZone("Europe/Warsaw"));

			cal.Month = Calendar.UCalendarMonths.September;
			cal.DayOfMonth = 1;

			Assert.AreEqual(true, cal.InDaylightTime());
		}
		
		[Test]
		public void SetTimeTest()
		{
			var cal = new GregorianCalendar(new TimeZone("UTC"));
			cal.SetTime(0);

			Assert.AreEqual(1970, cal.Year);
			Assert.AreEqual(0, cal.Hour);
			Assert.AreEqual(0, cal.Minute);
			Assert.AreEqual(0, cal.Millisecond);
		}

		[Test]
		public void ToDateTimeTest()
		{
			var cal = new GregorianCalendar();
			var datetime = cal.ToDateTime();

			Assert.AreEqual(cal.Year, datetime.Year);
			Assert.AreEqual((int)cal.Month + 1, datetime.Month);
			Assert.AreEqual(cal.DayOfMonth, datetime.Day);
			Assert.AreEqual(cal.HourOfDay, datetime.Hour);
			Assert.AreEqual(cal.Minute, datetime.Minute);
			Assert.AreEqual(cal.Second, datetime.Second);
			Assert.AreEqual(cal.Millisecond, datetime.Millisecond);
		}

		[Test]
		public void SetTimeZoneTest()
		{
			var expected = new TimeZone("AST");
			var cal = new GregorianCalendar(new TimeZone("UTC"));

			cal.SetTimeZone(expected);
			var result = cal.GetTimeZone();

			Assert.AreEqual(expected.Id, result.Id);
		}

		[Test]
		public void FirstDayOfWeekTest()
		{
			var cal = new GregorianCalendar(new TimeZone("UTC"));
			var newDay = Calendar.UCalendarDaysOfWeek.Thursday;

			var val0 = cal.FirstDayOfWeek;
			cal.FirstDayOfWeek = newDay;
			var val1 = cal.FirstDayOfWeek;

			Assert.AreEqual(Calendar.UCalendarDaysOfWeek.Sunday, val0);
			Assert.AreEqual(newDay, val1);
		}

		[Test]
		public void WeekOfYearTest()
		{
			var cal = new GregorianCalendar(new TimeZone("UTC"));
			cal.Clear();
			cal.DayOfMonth = 4;
			var newDay = Calendar.UCalendarDaysOfWeek.Thursday;

			var val0 = cal.WeekOfYear;
			cal.FirstDayOfWeek = newDay;
			var val1 = cal.WeekOfYear;


			Assert.AreEqual(2, val0);
			Assert.AreEqual(1, val1);
		}

		[Test]
		public void MinimalDaysInFirstWeekTest()
		{
			var cal = new GregorianCalendar(new TimeZone("UTC"));
			cal.Clear();
			cal.DayOfMonth = 4;
			var newMinimum = 5;

			var val0 = cal.WeekOfYear;
			cal.MinimalDaysInFirstWeek = newMinimum;
			var val1 = cal.WeekOfYear;

			Assert.AreEqual(2, val0);
			Assert.AreEqual(1, val1);
		}

		[Test]
		public void SkippedWallTimeTest()
		{
			var cal = new GregorianCalendar(new TimeZone("America/New_York"));
			cal.Year = 2011;
			cal.HourOfDay = 0;
			cal.Minute = 0;
			cal.Month = Calendar.UCalendarMonths.March;
			cal.DayOfMonth = 13;

			cal.SkippedWallTimeOption = Calendar.UCalendarWallTimeOption.WalltimeFirst;
			cal.Minute = 30;
			cal.HourOfDay = 2;
			var hour = cal.HourOfDay;
			var minute = cal.Minute;

			Assert.AreEqual(1, hour);
			Assert.AreEqual(30, minute);
		}

		[Test]
		public void RepeatedWallTimeTest()
		{
			var cal = new GregorianCalendar(new TimeZone("America/New_York"));
			cal.Year = 2011;
			cal.HourOfDay = 0;
			cal.Minute = 0;
			cal.Month = Calendar.UCalendarMonths.November;
			cal.DayOfMonth = 6;

			cal.RepeatedWallTimeOption = Calendar.UCalendarWallTimeOption.WalltimeFirst;
			cal.Minute = 30;
			cal.HourOfDay = 1;
			cal.Add(Calendar.UCalendarDateFields.Minute, 60);
			var hour = cal.HourOfDay;
			var minute = cal.Minute;

			Assert.AreEqual(1, hour);
			Assert.AreEqual(30, minute);
		}

		[Test]
		public void LenientTest_ThrowsArgumentException()
		{
			
			var cal = new GregorianCalendar(new TimeZone("America/New_York"));
			cal.Year = 2011;
			cal.HourOfDay = 0;
			cal.Minute = 0;
			cal.Month = Calendar.UCalendarMonths.March;
			cal.DayOfMonth = 13;

			cal.Lenient = false;
			cal.SkippedWallTimeOption = Calendar.UCalendarWallTimeOption.WalltimeFirst;
			cal.Minute = 30;
			cal.HourOfDay = 2;

			Assert.Throws<ArgumentException>(() => cal.GetTime());
		}

		[Test]
		public void WeekOfMonthTest()
		{
			var cal = new GregorianCalendar(new TimeZone("UTC"));
			cal.Clear();
			cal.DayOfMonth = 4;
			var newMinimum = 5;

			var val0 = cal.WeekOfMonth;
			cal.MinimalDaysInFirstWeek = newMinimum;
			var val1 = cal.WeekOfMonth;

			Assert.AreEqual(2, val0);
			Assert.AreEqual(1, val1);
		}

		[Test]
		public void EraTest()
		{
			var cal = new GregorianCalendar(new TimeZone("UTC"));

			var era1 = cal.Era;
			cal.Year = -1;
			var era0 = cal.Era;

			Assert.AreEqual(1, era1);
			Assert.AreEqual(0, era0);
		}

		[Test]
		public void ZoneOffsetTest()
		{
			var expected = 60 * 60 * 1000;
			var zone = new TimeZone("Europe/Paris");
			var cal = new GregorianCalendar(zone);

			var offset = cal.ZoneOffset;

			Assert.AreEqual(expected, offset);
		}

		[Test]
		public void DstOffsetTest()
		{
			var expected0 = 60 * 60 * 1000;
			var expected1 = 0;

			var zone = new TimeZone("Europe/Paris");
			var cal = new GregorianCalendar(zone);
			cal.Month = Calendar.UCalendarMonths.July;
			cal.DayOfMonth = 20;

			var offset0 = cal.DstOffset;
			cal.Month = Calendar.UCalendarMonths.January;
			var offset1 = cal.DstOffset;

			Assert.AreEqual(expected0, offset0);
			Assert.AreEqual(expected1, offset1);

		}

		[Test]
		public void AmPmTest()
		{
			var cal = new GregorianCalendar(new TimeZone("UTC"));

			cal.HourOfDay = 3;
			var val0 = cal.AmPm;
			cal.HourOfDay = 14;
			var val1 = cal.AmPm;

			Assert.AreEqual(0, val0);
			Assert.AreEqual(1, val1);
		}

		[Test]

		public void SetTimeZone2Test()
		{
			var winId = "Romance Standard Time";
			var expected = "Europe/Paris";
			var timezone = TimeZoneInfo.FindSystemTimeZoneById(winId);
			var cal = new GregorianCalendar(new TimeZone("UTC"));

			cal.SetTimeZone(timezone);

			var tz = cal.GetTimeZone();

			Assert.AreEqual(expected, tz.Id);
		}

		[Test]
		public void GetTimeZoneTest()
		{
			var timezone = new TimeZone("Europe/Zagreb");
			var expected = "Central European Standard Time";

			var cal = new GregorianCalendar(timezone);

			var tz = cal.GetTimeZoneInfo();

			Assert.AreEqual(expected, tz.Id);
		}
		

	}
}