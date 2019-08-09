using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace WHTTR
{
	public static class Gnd
	{
		public const string HTT_APP_UUID = "{6a04a791-bf4b-4bc3-91c5-459321f5b5fb}";

		public static EventWaitHandle EndProcEvent;
		public static SaveData Sd = new SaveData();
		public static HTTProc HTTProc;
		public static Icon RunIcon;
		public static Icon OffIcon_00;
		public static Icon OffIcon_01;
		public static Icon OffIcon_10;
	}
}
