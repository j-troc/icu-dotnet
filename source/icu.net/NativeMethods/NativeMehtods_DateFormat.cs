using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Icu
{
	internal static partial class NativeMethods
	{
		private class DateFormatMethodsContainer
		{
			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate Calendar.UCalendarDateFields udat_toCalendarDateFieldDelegate(
				DateFormatField field);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate DateFormat.DateFormatSafeHandle udat_openDelegate(
				DateFormatStyle timeStyle,
				DateFormatStyle dateStyle,
				[MarshalAs(UnmanagedType.LPStr)] string locale,
				[MarshalAs(UnmanagedType.LPWStr)] string tzID,
				int tzIDLength,
				[MarshalAs(UnmanagedType.LPWStr)] string pattern,
				int patternLength,
				out ErrorCode status);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate void udat_closeDelegate(
				IntPtr format);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate byte udat_getBooleanAttributeDelegate(
				DateFormat.DateFormatSafeHandle fmt,
				DateFormatBooleanAttribute attr,
				out ErrorCode status);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate void udat_setBooleanAttributeDelegate(
				DateFormat.DateFormatSafeHandle fmt,
				DateFormatBooleanAttribute attr,
				byte newValue,
				out ErrorCode status);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate DateFormat.DateFormatSafeHandle udat_cloneDelegate(
				DateFormat.DateFormatSafeHandle fmt,
				out ErrorCode status);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate int udat_formatDelegate(
				DateFormat.DateFormatSafeHandle format,
				double dateToFormat,
				IntPtr result,
				int resultLength,
				FieldPosition position,
				out ErrorCode status);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate int udat_formatCalendarDelegate(
				DateFormat.DateFormatSafeHandle format,
				Calendar.SafeCalendarHandle calendar,
				IntPtr result,
				int capacity,
				FieldPosition position,
				out ErrorCode status);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate double udat_parseDelegate(
				DateFormat.DateFormatSafeHandle format,
				StringBuilder sb,
				int textLength,
				IntPtr parsePos,
				out ErrorCode status);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate void udat_parseCalendarDelegate(
				DateFormat.DateFormatSafeHandle format,
				Calendar.SafeCalendarHandle calendar,
				StringBuilder text,
				int textLength,
				IntPtr parsePos,
				out ErrorCode status);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate byte udat_isLenientDelegate(
				DateFormat.DateFormatSafeHandle fmt);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate void udat_setLenientDelegate(
				DateFormat.DateFormatSafeHandle fmt,
				byte isLenient);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate Calendar.SafeCalendarHandle udat_getCalendarDelegate(
				DateFormat.DateFormatSafeHandle fmt);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate void udat_setCalendarDelegate(
				DateFormat.DateFormatSafeHandle fmt,
				Calendar.SafeCalendarHandle calendarToSet);
			
			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate IntPtr udat_getAvailableDelegate(
				int localeIndex);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate int udat_countAvailableDelegate();

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate double udat_get2DigitYearStartDelegate(
				DateFormat.DateFormatSafeHandle fmt,
				out ErrorCode status);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate void udat_set2DigitYearStartDelegate(
				DateFormat.DateFormatSafeHandle fmt,
				double d,
				out ErrorCode status);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate int udat_toPatternDelegate(
				DateFormat.DateFormatSafeHandle fmt,
				bool localized,
				IntPtr result,
				int resultLength,
				out ErrorCode status);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate void udat_applyPatternDelegate(
				DateFormat.DateFormatSafeHandle format,
				bool localized,
				[MarshalAs(UnmanagedType.LPWStr)] string pattern,
				int patternLength);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate int udat_getSymbolsDelegate(
				DateFormat.DateFormatSafeHandle fmt,
				DateFormatSymbolType type,
				int symbolIndex,
				IntPtr result,
				int resultLength,
				out ErrorCode status);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate int udat_countSymbolsDelegate(
				DateFormat.DateFormatSafeHandle fmt,
				DateFormatSymbolType type);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate void udat_setSymbolsDelegate(
				DateFormat.DateFormatSafeHandle format,
				DateFormatSymbolType type,
				int symbolIndex,
				[MarshalAs(UnmanagedType.LPWStr)]string value,
				int valueLength,
				out ErrorCode status);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate IntPtr udat_getLocaleByTypeDelegate(
				DateFormat.DateFormatSafeHandle fmt,
				LocDataLocaleType type,
				out ErrorCode status);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate void udat_setContextDelegate(
				DateFormat.DateFormatSafeHandle fmt,
				DisplayContext value,
				out ErrorCode status);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			internal delegate DisplayContext udat_getContextDelegate(
				DateFormat.DateFormatSafeHandle fmt,
				DisplayContextType type,
				out ErrorCode status);
			
			internal udat_toCalendarDateFieldDelegate udat_toCalendarDateField;
			internal udat_openDelegate udat_open;
			internal udat_closeDelegate udat_close;
			internal udat_getBooleanAttributeDelegate udat_getBooleanAttribute;
			internal udat_setBooleanAttributeDelegate udat_setBooleanAttribute;
			internal udat_cloneDelegate udat_clone;
			internal udat_formatDelegate udat_format;
			internal udat_formatCalendarDelegate udat_formatCalendar;
			internal udat_parseDelegate udat_parse;
			internal udat_parseCalendarDelegate udat_parseCalendar;
			internal udat_isLenientDelegate udat_isLenient;
			internal udat_setLenientDelegate udat_setLenient;
			internal udat_getCalendarDelegate udat_getCalendar;
			internal udat_setCalendarDelegate udat_setCalendar;
			internal udat_getAvailableDelegate udat_getAvailable;
			internal udat_countAvailableDelegate udat_countAvailable;
			internal udat_get2DigitYearStartDelegate udat_get2DigitYearStart;
			internal udat_set2DigitYearStartDelegate udat_set2DigitYearStart;
			internal udat_toPatternDelegate udat_toPattern;
			internal udat_applyPatternDelegate udat_applyPattern;
			internal udat_getSymbolsDelegate udat_getSymbols;
			internal udat_countSymbolsDelegate udat_countSymbols;
			internal udat_setSymbolsDelegate udat_setSymbols;
			internal udat_getLocaleByTypeDelegate udat_getLocaleByType;
			internal udat_setContextDelegate udat_setContext;
			internal udat_getContextDelegate udat_getContext;
		}

		private static DateFormatMethodsContainer _DateFormatMethods;

		private static DateFormatMethodsContainer DateFormatMethods =>
			 _DateFormatMethods ?? (_DateFormatMethods = new DateFormatMethodsContainer());

		public static Calendar.UCalendarDateFields udat_toCalendarDateField(
			DateFormatField field)
		{
			if (DateFormatMethods.udat_toCalendarDateField == null)
				DateFormatMethods.udat_toCalendarDateField = GetMethod<DateFormatMethodsContainer.udat_toCalendarDateFieldDelegate>(IcuI18NLibHandle, "udat_toCalendarDateField");
			return DateFormatMethods.udat_toCalendarDateField(field);
		}

		public static DateFormat.DateFormatSafeHandle udat_open(
			DateFormatStyle timeStyle,
			DateFormatStyle dateStyle,
			string locale,
			string tzID,
			int tzIDLength,
			string pattern,
			int patternLength,
			out ErrorCode status)
		{
			if (DateFormatMethods.udat_open == null)
				DateFormatMethods.udat_open = GetMethod<DateFormatMethodsContainer.udat_openDelegate>(IcuI18NLibHandle, "udat_open");
			return DateFormatMethods.udat_open(timeStyle, dateStyle, locale, tzID, tzIDLength, pattern, patternLength, out status);
		}

		public static void udat_close(
			IntPtr format)
		{
			if (DateFormatMethods.udat_close == null)
				DateFormatMethods.udat_close = GetMethod<DateFormatMethodsContainer.udat_closeDelegate>(IcuI18NLibHandle, "udat_close");
			DateFormatMethods.udat_close(format);
		}

		public static byte udat_getBooleanAttribute(
			DateFormat.DateFormatSafeHandle fmt,
			DateFormatBooleanAttribute attr,
			out ErrorCode status)
		{
			if (DateFormatMethods.udat_getBooleanAttribute == null)
				DateFormatMethods.udat_getBooleanAttribute = GetMethod<DateFormatMethodsContainer.udat_getBooleanAttributeDelegate>(IcuI18NLibHandle, "udat_getBooleanAttribute");
			return DateFormatMethods.udat_getBooleanAttribute(fmt, attr, out status);
		}

		public static void udat_setBooleanAttribute(
			DateFormat.DateFormatSafeHandle fmt,
			DateFormatBooleanAttribute attr,
			byte newValue,
			out ErrorCode status)
		{
			if (DateFormatMethods.udat_setBooleanAttribute == null)
				DateFormatMethods.udat_setBooleanAttribute = GetMethod<DateFormatMethodsContainer.udat_setBooleanAttributeDelegate>(IcuI18NLibHandle, "udat_setBooleanAttribute");
			DateFormatMethods.udat_setBooleanAttribute(fmt, attr, newValue, out status);
		}

		public static DateFormat.DateFormatSafeHandle udat_clone(
			DateFormat.DateFormatSafeHandle fmt,
			out ErrorCode status)
		{
			if (DateFormatMethods.udat_clone == null)
				DateFormatMethods.udat_clone = GetMethod<DateFormatMethodsContainer.udat_cloneDelegate>(IcuI18NLibHandle, "udat_clone");
			return DateFormatMethods.udat_clone(fmt, out status);
		}

		public static int udat_format(
			DateFormat.DateFormatSafeHandle format,
			double dateToFormat,
			IntPtr result,
			int resultLength,
			FieldPosition position,
			out ErrorCode status)
		{
			if (DateFormatMethods.udat_format == null)
				DateFormatMethods.udat_format = GetMethod<DateFormatMethodsContainer.udat_formatDelegate>(IcuI18NLibHandle, "udat_format");
			return DateFormatMethods.udat_format(format, dateToFormat, result, resultLength, position, out status);
		}

		public static int udat_formatCalendar(
			DateFormat.DateFormatSafeHandle format,
			Calendar.SafeCalendarHandle calendar,
			IntPtr result,
			int capacity,
			FieldPosition position,
			out ErrorCode status)
		{
			if (DateFormatMethods.udat_formatCalendar == null)
				DateFormatMethods.udat_formatCalendar = GetMethod<DateFormatMethodsContainer.udat_formatCalendarDelegate>(IcuI18NLibHandle, "udat_formatCalendar");
			return DateFormatMethods.udat_formatCalendar(format, calendar, result, capacity, position, out status);
		}

		public static double udat_parse(
			DateFormat.DateFormatSafeHandle format,
			StringBuilder text,
			int textLength,
			IntPtr parsePos,
			out ErrorCode status)
		{
			if (DateFormatMethods.udat_parse == null)
				DateFormatMethods.udat_parse = GetMethod<DateFormatMethodsContainer.udat_parseDelegate>(IcuI18NLibHandle, "udat_parse");
			
			var result = DateFormatMethods.udat_parse(format, text, textLength, parsePos, out status);

			return result;
		}

		public static void udat_parseCalendar(
			DateFormat.DateFormatSafeHandle format,
			Calendar.SafeCalendarHandle calendar,
			StringBuilder text,
			int textLength,
			IntPtr parsePos,
			out ErrorCode status)
		{
			if (DateFormatMethods.udat_parseCalendar == null)
				DateFormatMethods.udat_parseCalendar = GetMethod<DateFormatMethodsContainer.udat_parseCalendarDelegate>(IcuI18NLibHandle, "udat_parseCalendar");
			DateFormatMethods.udat_parseCalendar(format, calendar, text, textLength, parsePos, out status);
		}

		public static byte udat_isLenient(
			DateFormat.DateFormatSafeHandle fmt)
		{
			if (DateFormatMethods.udat_isLenient == null)
				DateFormatMethods.udat_isLenient = GetMethod<DateFormatMethodsContainer.udat_isLenientDelegate>(IcuI18NLibHandle, "udat_isLenient");
			return DateFormatMethods.udat_isLenient(fmt);
		}

		public static void udat_setLenient(
			DateFormat.DateFormatSafeHandle fmt,
			byte isLenient)
		{
			if (DateFormatMethods.udat_setLenient == null)
				DateFormatMethods.udat_setLenient = GetMethod<DateFormatMethodsContainer.udat_setLenientDelegate>(IcuI18NLibHandle, "udat_setLenient");
			DateFormatMethods.udat_setLenient(fmt, isLenient);
		}

		public static Calendar.SafeCalendarHandle udat_getCalendar(
			DateFormat.DateFormatSafeHandle fmt)
		{
			if (DateFormatMethods.udat_getCalendar == null)
				DateFormatMethods.udat_getCalendar = GetMethod<DateFormatMethodsContainer.udat_getCalendarDelegate>(IcuI18NLibHandle, "udat_getCalendar");
			return DateFormatMethods.udat_getCalendar(fmt);
		}

		public static void udat_setCalendar(
			DateFormat.DateFormatSafeHandle fmt,
			Calendar.SafeCalendarHandle calendarToSet)
		{
			if (DateFormatMethods.udat_setCalendar == null)
				DateFormatMethods.udat_setCalendar = GetMethod<DateFormatMethodsContainer.udat_setCalendarDelegate>(IcuI18NLibHandle, "udat_setCalendar");
			DateFormatMethods.udat_setCalendar(fmt, calendarToSet);
		}

		public static IntPtr udat_getAvailable(
			int localeIndex)
		{
			if (DateFormatMethods.udat_getAvailable == null)
				DateFormatMethods.udat_getAvailable = GetMethod<DateFormatMethodsContainer.udat_getAvailableDelegate>(IcuI18NLibHandle, "udat_getAvailable");
			return DateFormatMethods.udat_getAvailable(localeIndex);
		}

		public static int udat_countAvailable()
		{
			if (DateFormatMethods.udat_countAvailable == null)
				DateFormatMethods.udat_countAvailable = GetMethod<DateFormatMethodsContainer.udat_countAvailableDelegate>(IcuI18NLibHandle, "udat_countAvailable");
			return DateFormatMethods.udat_countAvailable();
		}

		public static double udat_get2DigitYearStart(
			DateFormat.DateFormatSafeHandle fmt,
			out ErrorCode status)
		{
			if (DateFormatMethods.udat_get2DigitYearStart == null)
				DateFormatMethods.udat_get2DigitYearStart = GetMethod<DateFormatMethodsContainer.udat_get2DigitYearStartDelegate>(IcuI18NLibHandle, "udat_get2DigitYearStart");
			return DateFormatMethods.udat_get2DigitYearStart(fmt, out status);
		}

		public static void udat_set2DigitYearStart(
			DateFormat.DateFormatSafeHandle fmt,
			double d,
			out ErrorCode status)
		{
			if (DateFormatMethods.udat_set2DigitYearStart == null)
				DateFormatMethods.udat_set2DigitYearStart = GetMethod<DateFormatMethodsContainer.udat_set2DigitYearStartDelegate>(IcuI18NLibHandle, "udat_set2DigitYearStart");
			DateFormatMethods.udat_set2DigitYearStart(fmt, d, out status);
		}

		public static int udat_toPattern(
			DateFormat.DateFormatSafeHandle fmt,
			bool localized,
			IntPtr result,
			int resultLength,
			out ErrorCode status)
		{
			if (DateFormatMethods.udat_toPattern == null)
				DateFormatMethods.udat_toPattern = GetMethod<DateFormatMethodsContainer.udat_toPatternDelegate>(IcuI18NLibHandle, "udat_toPattern");
			return DateFormatMethods.udat_toPattern(fmt, localized, result, resultLength, out status);
		}

		public static void udat_applyPattern(
			DateFormat.DateFormatSafeHandle format,
			bool localized,
			string pattern,
			int patternLength)
		{
			if (DateFormatMethods.udat_applyPattern == null)
				DateFormatMethods.udat_applyPattern = GetMethod<DateFormatMethodsContainer.udat_applyPatternDelegate>(IcuI18NLibHandle, "udat_applyPattern");
			DateFormatMethods.udat_applyPattern(format, localized, pattern, patternLength);
		}

		public static int udat_getSymbols(
			DateFormat.DateFormatSafeHandle fmt,
			DateFormatSymbolType type,
			int symbolIndex,
			IntPtr result,
			int resultLength,
			out ErrorCode status)
		{
			if (DateFormatMethods.udat_getSymbols == null)
				DateFormatMethods.udat_getSymbols = GetMethod<DateFormatMethodsContainer.udat_getSymbolsDelegate>(IcuI18NLibHandle, "udat_getSymbols");
			return DateFormatMethods.udat_getSymbols(fmt, type, symbolIndex, result, resultLength, out status);
		}

		public static int udat_countSymbols(
			DateFormat.DateFormatSafeHandle fmt,
			DateFormatSymbolType type)
		{
			if (DateFormatMethods.udat_countSymbols == null)
				DateFormatMethods.udat_countSymbols = GetMethod<DateFormatMethodsContainer.udat_countSymbolsDelegate>(IcuI18NLibHandle, "udat_countSymbols");
			return DateFormatMethods.udat_countSymbols(fmt, type);
		}

		public static void udat_setSymbols(
			DateFormat.DateFormatSafeHandle format,
			DateFormatSymbolType type,
			int symbolIndex,
			string value,
			int valueLength,
			out ErrorCode status)
		{
			if (DateFormatMethods.udat_setSymbols == null)
				DateFormatMethods.udat_setSymbols = GetMethod<DateFormatMethodsContainer.udat_setSymbolsDelegate>(IcuI18NLibHandle, "udat_setSymbols");
			DateFormatMethods.udat_setSymbols(format, type, symbolIndex, value, valueLength, out status);
		}

		public static IntPtr udat_getLocaleByType(
			DateFormat.DateFormatSafeHandle fmt,
			LocDataLocaleType type,
			out ErrorCode status)
		{
			if (DateFormatMethods.udat_getLocaleByType == null)
				DateFormatMethods.udat_getLocaleByType = GetMethod<DateFormatMethodsContainer.udat_getLocaleByTypeDelegate>(IcuI18NLibHandle, "udat_getLocaleByType");
			return DateFormatMethods.udat_getLocaleByType(fmt, type, out status);
		}

		public static void udat_setContext(
			DateFormat.DateFormatSafeHandle fmt,
			DisplayContext value,
			out ErrorCode status)
		{
			if (DateFormatMethods.udat_setContext == null)
				DateFormatMethods.udat_setContext = GetMethod<DateFormatMethodsContainer.udat_setContextDelegate>(IcuI18NLibHandle, "udat_setContext");
			DateFormatMethods.udat_setContext(fmt, value, out status);
		}

		public static DisplayContext udat_getContext(
			DateFormat.DateFormatSafeHandle fmt,
			DisplayContextType type,
			out ErrorCode status)
		{
			if (DateFormatMethods.udat_getContext == null)
				DateFormatMethods.udat_getContext = GetMethod<DateFormatMethodsContainer.udat_getContextDelegate>(IcuI18NLibHandle, "udat_getContext");
			return DateFormatMethods.udat_getContext(fmt, type, out status);
		}
		
	}
}
