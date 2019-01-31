using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
#if NETSTANDARD1_6
using Icu;
#else
using System.Globalization;
using System.Runtime.ConstrainedExecution;
#endif

namespace Icu
{
	public class DateFormat : IDisposable
	{
		internal protected sealed class DateFormatSafeHandle : SafeHandle
		{
			public DateFormatSafeHandle() :
				base(IntPtr.Zero, true)
			{ }

			///<summary>
			/// When overridden in a derived class, executes the code required to free the handle.
			///</summary>
			///<returns>
			/// true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false.
			/// In this case, it generates a ReleaseHandleFailed Managed Debugging Assistant.
			///</returns>
#if !NETSTANDARD1_6
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
#endif
			protected override bool ReleaseHandle()
			{
				if (handle != IntPtr.Zero)
					NativeMethods.udat_close(handle);
				handle = IntPtr.Zero;
				return true;
			}

			///<summary>
			///When overridden in a derived class, gets a value indicating whether the handle value is invalid.
			///</summary>
			///<returns>
			///true if the handle is valid; otherwise, false.
			///</returns>
			public override bool IsInvalid
			{
				get { return handle == IntPtr.Zero; }
			}

			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
				ReleaseHandle();
			}
		}

		private bool _disposingValue; // To detect redundant calls
		protected DateFormatSafeHandle _dateFormatHandle = default(DateFormatSafeHandle);

		private DateFormat(DateFormatStyle timeStyle, DateFormatStyle dateStyle, Locale locale, TimeZone timezone, string pattern)
		{
			_dateFormatHandle = NativeMethods.udat_open(timeStyle, dateStyle, locale?.Name ?? null,
				timezone?.ID, timezone?.ID.Length ?? -1, pattern, pattern?.Length ?? -1, out ErrorCode status);

			ExceptionFromErrorCode.ThrowIfError(status);
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		public DateFormat()
			: this(DateFormatStyle.UDAT_DEFAULT, DateFormatStyle.UDAT_DEFAULT, null, null, null)
		{
		}

		/// <summary>
		/// Construct a SimpleDateFormat using the given pattern and the default locale.
		/// The locale is used to obtain the symbols used in formatting(e.g., the names of the months), but not to provide the pattern.
		/// </summary>
		/// <param name="pattern">the pattern for the format.</param>
		public DateFormat(string pattern)
			: this(DateFormatStyle.UDAT_PATTERN, DateFormatStyle.UDAT_PATTERN, null, null, pattern)
		{
		}
		
		/// <summary>
		/// Creates DateFormat as a copy of passed instance.
		/// </summary>
		/// <param name="dateFormat">DateFormat to be cloned</param>
		public DateFormat(DateFormat dateFormat)
		{
			_dateFormatHandle = NativeMethods.udat_clone(dateFormat._dateFormatHandle, out ErrorCode status);
			ExceptionFromErrorCode.ThrowIfError(status);
		}

		/// <summary>
		/// Creates copy of current DateFormat. 
		/// </summary>
		/// <returns>deep copy</returns>
		public DateFormat Clone()
		{
			return new DateFormat(this);
		}

		/// <summary>
		/// Format a date to produce a string. 
		/// </summary>
		/// <param name="date">Date to be formatted.</param>
		/// <returns></returns>
		public string Format(double date)
		{
			return Format(date, null);
		}

		/// <summary>
		/// Formats a date into a date/time string. 
		/// </summary>
		/// <param name="date">Date to be formatted.</param>
		/// <param name="position">On input: an alignment field, if desired. On output: the offsets of the alignment field. </param>
		/// <returns>Formatted date</returns>
		public string Format(double date, FieldPosition position)
		{
			return NativeMethods.GetUnicodeString((ptr, length) =>
			{
				length = NativeMethods.udat_format(_dateFormatHandle, date, ptr, length, position, out ErrorCode err);
				return new Tuple<ErrorCode, int>(err, length);
			});
		}

		/// <summary>
		/// Formats a date into a date/time string. 
		/// </summary>
		/// <param name="calendar">Calendar to be formatted.</param>
		/// <returns>Formatted date</returns>
		public string Format(Calendar calendar)
		{
			return Format(calendar, null);
		}

		/// <summary>
		/// Formats a date into a date/time string. 
		/// 
		/// For example, given a time text "1996.07.10 AD at 15:08:56 PDT", if the given position.field is UDAT_YEAR_FIELD,
		/// the offsets fieldPosition.beginIndex and statfieldPositionus.getEndIndex will be set to 0 and 4, respectively.
		/// </summary>
		/// <param name="calendar">Calendar to be formatted.</param>
		/// <param name="position">On input: an alignment field, if desired. On output: the offsets of the alignment field. </param>
		/// <returns>Formatted date</returns>
		public string Format(Calendar calendar, FieldPosition position)
		{
			return NativeMethods.GetUnicodeString((ptr, length) =>
			{
				length = NativeMethods.udat_formatCalendar(_dateFormatHandle, calendar._CalendarHandle, ptr,
					length, position, out ErrorCode err);
				return new Tuple<ErrorCode, int>(err, length);
			});
		}

		/// <summary>
		/// Determines if a DateFormat will perform lenient parsing.
		/// </summary>
		public bool Lenient
		{
			get => NativeMethods.udat_isLenient(_dateFormatHandle) != 0 ? true : false;
			set => NativeMethods.udat_setLenient(_dateFormatHandle, value ? (byte)1 : (byte)0);
		}

		/// <summary>
		/// Get the symbols associated with an DateFormat.
		/// The symbols are what a DateFormat uses to represent locale-specific data, for example month or day names.
		/// </summary>
		/// <param name="type">The type of symbols to get. One of UDAT_ERAS, UDAT_MONTHS, UDAT_SHORT_MONTHS, UDAT_WEEKDAYS, UDAT_SHORT_WEEKDAYS, UDAT_AM_PMS, or UDAT_LOCALIZED_CHARS </param>
		/// <returns> Set of symbols of requested type.</returns>
		public IEnumerable<string> GetSymbols(DateFormatSymbolType type)
		{
			int count = NativeMethods.udat_countSymbols(_dateFormatHandle, type);

			List<string> symbols = new List<string>();

			for (int i = 0; i < count; i++)
			{
				string symbol = NativeMethods.GetUnicodeString((ptr, length) =>
				{
					length = NativeMethods.udat_getSymbols(_dateFormatHandle, type, i, ptr,
						length, out ErrorCode err);
					return new Tuple<ErrorCode, int>(err, length);
				});

				symbols.Add(symbol);
			}

			return symbols;
		}

		/// <summary>
		/// Set the symbol associated with an DateFormat.
		/// The symbols are what a DateFormat uses to represent locale-specific data, for example month or day names.
		/// </summary>
		/// <param name="type">Type of symbol to be set.</param>
		/// <param name="symbolIndex">Index of the symbol to be set.</param>
		/// <param name="value">The new value</param>
		public void SetSymbol(DateFormatSymbolType type, int symbolIndex, string value)
		{
			NativeMethods.udat_setSymbols(_dateFormatHandle, type, symbolIndex, value, value.Length, out ErrorCode status);
			ExceptionFromErrorCode.ThrowIfError(status);
		}

		/// <summary>
		/// Parse a date/time string.
		///	For example, a time text "07/10/96 4:5 PM, PDT" will be parsed into a Date that is equivalent to Date(837039928046).
		/// By default, parsing is lenient: If the input is not in the form used by this object's format method but can still be parsed as a date, then the parse succeeds.
		/// Clients may insist on strict adherence to the format by setting Lenient to false. 
		/// </summary>
		/// <param name="text">The date/time string to be parsed</param>
		/// <returns>A valid date if the input could be parsed</returns>
		public double Parse(string text)
		{
			StringBuilder sb = new StringBuilder(text);
			var date = NativeMethods.udat_parse(_dateFormatHandle, sb, sb.Length, IntPtr.Zero, out ErrorCode status);
			ExceptionFromErrorCode.ThrowIfError(status);

			return date;
		}

		/// <summary>
		/// Parse a date/time string.
		///	For example, a time text "07/10/96 4:5 PM, PDT" will be parsed into a Date that is equivalent to Date(837039928046).
		/// By default, parsing is lenient: If the input is not in the form used by this object's format method but can still be parsed as a date, then the parse succeeds.
		/// Clients may insist on strict adherence to the format by setting Lenient to false. 
		/// </summary>
		/// <param name="text">The date/time string to be parsed </param>
		/// <param name="cal"> Calendar to which the result (milliseconds and time zone) will be set.</param>
		public void Parse(string text, Calendar cal)
		{
			StringBuilder sb = new StringBuilder(text);
			NativeMethods.udat_parseCalendar(_dateFormatHandle, cal._CalendarHandle, sb, sb.Length, IntPtr.Zero, out ErrorCode status);
			ExceptionFromErrorCode.ThrowIfError(status);
		}

		/// <summary>
		/// Set the start date used to interpret two-digit year strings.
		/// When dates are parsed having 2-digit year strings, they are placed within a assumed range of 100 years starting on the two digit start date.
		/// For example, the string "24-Jan-17" may be in the year 1817, 1917, 2017, or some other year.
		/// DateFormat chooses a year so that the resultant date is on or after the two digit start date and within 100 years of the two digit start date.
		/// By default, the two digit start date is set to 80 years before the current time at which a DateFormat object is created.
		/// </summary>
		/// <param name="date">start Date used to interpret two-digit year strings. </param>
		public void Set2DigitYearStart(double date)
		{
			NativeMethods.udat_set2DigitYearStart(_dateFormatHandle, date, out ErrorCode status);
			ExceptionFromErrorCode.ThrowIfError(status);
		}

		/// <summary>
		/// Get the start date used to interpret two-digit year strings.
		/// When dates are parsed having 2-digit year strings, they are placed within a assumed range of 100 years starting on the two digit start date.
		/// For example, the string "24-Jan-17" may be in the year 1817, 1917, 2017, or some other year.
		/// DateFormat chooses a year so that the resultant date is on or after the two digit start date and within 100 years of the two digit start date.
		/// By default, the two digit start date is set to 80 years before the current time at which a DateFormat object is created.
		/// </summary>
		/// <returns>start date used to interpret two-digit year strings</returns>
		public double Get2DigitYearStart()
		{
			var d = NativeMethods.udat_get2DigitYearStart(_dateFormatHandle, out ErrorCode status);
			ExceptionFromErrorCode.ThrowIfError(status);
			return d;
		}

		/// <summary>
		/// Return a pattern string describing this date format. 
		/// </summary>
		/// <returns>The pattern</returns>
		public string ToPattern()
		{
			return NativeMethods.GetUnicodeString((ptr, length) =>
			{
				length = NativeMethods.udat_toPattern(_dateFormatHandle, false, ptr, length, out ErrorCode err);
				return new Tuple<ErrorCode, int>(err, length);
			});

		}

		/// <summary>
		/// Apply the given unlocalized pattern string to this date format.
		/// (i.e., after this call, this formatter will format dates according to the new pattern)
		/// </summary>
		/// <param name="pattern">The pattern to be applied. </param>
		public void ApplyPattern(string pattern)
		{
			NativeMethods.udat_applyPattern(_dateFormatHandle, false, pattern, pattern.Length);
		}

		/// <summary>
		/// Set the calendar to be used by this date format. 
		/// </summary>
		/// <param name="calendar">Calendar object to be set. </param>
		public void SetCalendar(Calendar calendar)
		{
			NativeMethods.udat_setCalendar(_dateFormatHandle, calendar._CalendarHandle);
		}

		/// <summary>
		/// Set a particular DisplayContext value in the formatter, such as UDISPCTX_CAPITALIZATION_FOR_STANDALONE. 
		/// </summary>
		/// <param name="value">The DisplayContext value to set. </param>
		public void SetContext(DisplayContext value)
		{
			NativeMethods.udat_setContext(_dateFormatHandle, value, out ErrorCode status);
			ExceptionFromErrorCode.ThrowIfError(status);
		}

		/// <summary>
		/// Get the formatter's DisplayContext value for the specified DisplayContextType, such as UDISPCTX_TYPE_CAPITALIZATION. 
		/// </summary>
		/// <param name="value">The DisplayContextType whose value to return </param>
		/// <returns>The DisplayContextValue for the specified type. </returns>
		public DisplayContext GetContext(DisplayContextType value)
		{
			var context = NativeMethods.udat_getContext(_dateFormatHandle, value, out ErrorCode status);
			ExceptionFromErrorCode.ThrowIfError(status);
			return context;
		}

		/// <summary>
		/// Sets an boolean attribute on this DateFormat. 
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown if this instance does not support the specified attribute.  </exception>
		/// <param name="attr">	the attribute to set </param>
		/// <param name="newValue">	new value </param>
		public void SetBooleanAttribute(DateFormatBooleanAttribute attr, bool newValue)
		{
			NativeMethods.udat_setBooleanAttribute(_dateFormatHandle, attr, newValue ? (byte)1 : (byte)0, out ErrorCode status);
			ExceptionFromErrorCode.ThrowIfError(status);
		}

		/// <summary>
		/// Returns a boolean from this DateFormat 
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown if this instance does not support the specified attribute.  </exception>
		/// <param name="attr">the attribute to set </param>
		/// <returns>the attribute value.</returns>
		public bool GetBooleanAttribute(DateFormatBooleanAttribute attr)
		{
			var value = NativeMethods.udat_getBooleanAttribute(_dateFormatHandle, attr, out ErrorCode status);
			ExceptionFromErrorCode.ThrowIfError(status);
			return value != 0 ? true : false;
		}

		/// <summary>
		/// Creates a time formatter with the given formatting style for the given locale. 
		/// </summary>
		/// <param name="timeStyle">The given formatting style. For example, SHORT for "h:mm a" in the US locale. Relative time styles are not currently supported. </param>
		/// <param name="locale">The given locale. </param>
		/// <returns></returns>
		public static DateFormat CreateTimeInstance(DateFormatStyle timeStyle, Locale locale = null)
		{
			return new DateFormat(timeStyle, DateFormatStyle.UDAT_NONE, locale, null, null);
		}

		/// <summary>
		/// Creates a date formatter with the given formatting style for the given const locale. 
		/// </summary>
		/// <param name="dateStyle">The given formatting style. For example, SHORT for "M/d/yy" in the US locale.</param>
		/// <param name="locale">The given locale. </param>
		/// <returns>A date formatter</returns>
		public static DateFormat CreateDateInstance(DateFormatStyle dateStyle, Locale locale = null)
		{
			return new DateFormat(DateFormatStyle.UDAT_NONE, dateStyle, locale, null, null);
		}

		/// <summary>
		/// Creates a date/time formatter with the given formatting styles for the given locale. 
		/// </summary>
		/// <param name="dateStyle">The given formatting style for the date portion of the result. For example, SHORT for "M/d/yy" in the US locale.</param>
		/// <param name="timeStyle">The given formatting style for the time portion of the result. For example, SHORT for "h:mm a" in the US locale. </param>
		/// <param name="locale">The given locale</param>
		/// <returns>A date/time formatter</returns>
		public static DateFormat CreateDateTimeInstance(DateFormatStyle dateStyle, DateFormatStyle timeStyle, Locale locale = null)
		{
			return new DateFormat(timeStyle, dateStyle, locale, null, null);
		}

		/// <summary>
		/// Gets the set of locales for which DateFormats are installed.
		/// </summary>
		/// <returns>the set of locales for which DateFormats are installed</returns>
		public static IEnumerable<Locale> GetAvailableLocales()
		{
			int available = NativeMethods.udat_countAvailable();

			List<Locale> locales = new List<Locale>();

			for (int i = 0; i < available; i++)
			{
				var locName = Marshal.PtrToStringAnsi(NativeMethods.udat_getAvailable(i));
				locales.Add(new Locale(locName));
			}

			return locales;
		}

		#region IDisposable support

		/// <summary>
		/// Dispose of managed/unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases the resources used by Calendar.
		/// </summary>
		/// <param name="disposing">true to release managed and unmanaged
		/// resources; false to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposingValue)
			{
				if (disposing)
				{
					// Dispose managed state (managed objects), if any.
				}

				_dateFormatHandle.Dispose();
				_disposingValue = true;
			}
		}
		~DateFormat()
		{
			Dispose(false);
		}

		#endregion
	}
}
