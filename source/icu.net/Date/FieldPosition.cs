using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Icu
{
	[StructLayout(LayoutKind.Sequential)]
	public class FieldPosition
	{
		public int field;
		public int beginIndex;
		public int endIndex;
	}
}
