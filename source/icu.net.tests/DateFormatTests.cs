using System;
using NUnit.Framework;
using Icu;
using System.Globalization;
using System.Linq;
using System.Diagnostics;

namespace Icu.Tests
{
	[TestFixture]
	class DateFormatTests
	{
		[Test]
		public void CloneTest()
		{
			var pattern = "dd-mm-yyyy";
			var dateFormat = new DateFormat(pattern);

			var cloned = dateFormat.Clone();



			Assert.AreEqual(pattern, cloned.ToPattern());
		}

		[Test]
		public void FormatDateTest()
		{
			var expected = "01-01-1970";

			var dateFormat = new DateFormat("dd-MM-yyyy");

			var result = dateFormat.Format(0);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void FormatDateTest2()
		{
			var dateFormat = new DateFormat();

			var pos = new FieldPosition { field = 2 };

			var result = dateFormat.Format(0, pos);

			Assert.AreEqual(0, pos.beginIndex);
			Assert.AreEqual(3, pos.endIndex);
		}

		[Test]
		public void FormatCalendarTest()
		{
			var expected = "01-01-1971";

			var cal = new GregorianCalendar();
			cal.SetTime(0);
			cal.Year = 1971;

			var dateFormat = new DateFormat("dd-MM-yyyy");

			var result = dateFormat.Format(cal);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void FormatCalendarTest2()
		{
			var cal = new GregorianCalendar();
			cal.SetTime(0);

			var dateFormat = new DateFormat();

			var pos = new FieldPosition { field = 2 };

			var result = dateFormat.Format(cal, pos);

			Assert.AreEqual(0, pos.beginIndex);
			Assert.AreEqual(3, pos.endIndex);
		}
		
		[Test]
		public void ParseTest()
		{
			string toParse = "15-07-2020";
			var dateFormat = new DateFormat("dd-MM-yyyy");

			double time = dateFormat.Parse(toParse);

			var formatted = dateFormat.Format(time);

			Assert.AreEqual(toParse, formatted);
		}


		[Test]
		public void ParseCalendarTest()
		{

			string toParse = "15-07-2020";
			var dateFormat = new DateFormat("dd-MM-yyyy");
			var cal = new GregorianCalendar();

			dateFormat.Parse(toParse, cal);

			Assert.AreEqual(2020, cal.Year);
			Assert.AreEqual(Calendar.UCalendarMonths.July, cal.Month);
			Assert.AreEqual(15, cal.DayOfMonth);
		}


		[Test]
		public void LenientTest()
		{
			var calendar = new GregorianCalendar();
			calendar.Lenient = false;
			var dateFormat = new DateFormat();
			dateFormat.SetCalendar(calendar);

			var initialLenient = dateFormat.Lenient;
			dateFormat.Lenient = true;

			Assert.AreEqual(false, initialLenient);
			Assert.AreEqual(true, dateFormat.Lenient);
		}


		[Test]
		public void Set2DigitYearStartTest()
		{
			var expected = 1970;
			var dateFormat = new DateFormat();
			var cal = new GregorianCalendar();


			dateFormat.Set2DigitYearStart(0);
			var date = dateFormat.Get2DigitYearStart();
			cal.SetTime(date);
			var startYear = cal.Year;

			Assert.AreEqual(expected, startYear);
		}


		[Test]
		public void Get2DigitYearStart()
		{

			var cal = new GregorianCalendar();
			var expected = cal.Year - 80;
			var dateFormat = new DateFormat();

			var date = dateFormat.Get2DigitYearStart();
			cal.SetTime(date);
			var startYear = cal.Year;

			Assert.AreEqual(expected, startYear);

		}


		[Test]
		public void ToPatternTest()
		{
			var pattern = "dd-MM-yyyy";
			var dateFormat = new DateFormat(pattern);

			var result = dateFormat.ToPattern();

			Assert.AreEqual(pattern, result);
		}

		[Test]
		public void GetSymbolsTest()
		{
			var dateFormat = new DateFormat();

			var result = dateFormat.GetSymbols(DateFormatSymbolType.UDAT_MONTHS);

			Assert.AreEqual(12, result.Count());
			Assert.Contains("July", result.ToList());
		}

		[Test]
		public void SetSymbolsTest()
		{
			var replacement = "SampleText";
			int index = 4;
			var dateFormat = new DateFormat();

			dateFormat.SetSymbol(DateFormatSymbolType.UDAT_MONTHS, index, replacement);
			var result = dateFormat.GetSymbols(DateFormatSymbolType.UDAT_MONTHS).ToList()[index];

			Assert.AreEqual(replacement, result);
		}

		[Test]
		public void GetAvailableLocalesTest()
		{
			var locales = DateFormat.GetAvailableLocales();

			Assert.GreaterOrEqual(locales.Count(), 600);
		}

		[Test]
		public void SetContextTest()
		{
			var expected = DisplayContext.UDISPCTX_CAPITALIZATION_FOR_UI_LIST_OR_MENU;
			var dateFormat = new DateFormat();

			dateFormat.SetContext(expected);

			var result = dateFormat.GetContext(DisplayContextType.UDISPCTX_TYPE_CAPITALIZATION);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void GetContextTest()
		{
			var expected = DisplayContext.UDISPCTX_CAPITALIZATION_NONE;
			var dateFormat = new DateFormat();

			var result = dateFormat.GetContext(DisplayContextType.UDISPCTX_TYPE_CAPITALIZATION);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void SetBooleanAttributeTest()
		{
			var expected = false;
			var dateFormat = new DateFormat();

			dateFormat.SetBooleanAttribute(DateFormatBooleanAttribute.UDAT_PARSE_ALLOW_NUMERIC, expected);
			var result = dateFormat.GetBooleanAttribute(DateFormatBooleanAttribute.UDAT_PARSE_ALLOW_NUMERIC);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void GetBooleanAttributeTest()
		{
			var expected = true;
			var dateFormat = new DateFormat();

			var result = dateFormat.GetBooleanAttribute(DateFormatBooleanAttribute.UDAT_PARSE_ALLOW_WHITESPACE);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void ApplyPatternTest()
		{

			var pattern = "dd-mm-yyyy";
			var dateFormat = new DateFormat("y");

			dateFormat.ApplyPattern(pattern);
			var result = dateFormat.ToPattern();

			Assert.AreEqual(pattern, result);
		}

	}
}
