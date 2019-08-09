using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WHTTR
{
	public static class StringTools
	{
		public static readonly Encoding ENCODING_SJIS = Encoding.GetEncoding(932);

		public static bool EqualsIgnoreCase(string a, string b)
		{
			return a.ToUpper() == b.ToUpper();
		}

		public static readonly string ASCII = GetAsciiRange(0x21, 0x7e);
		//public static readonly string DIGIT = GetAsciiRange(0x30, 0x39);
		//public static readonly string ALPHA = GetAsciiRange(0x41, 0x5a);
		//public static readonly string alpha = GetAsciiRange(0x61, 0x7a);

		public static string GetAsciiRange(int minCode, int maxCode)
		{
			StringBuilder buff = new StringBuilder();

			for (int code = minCode; code <= maxCode; code++)
				buff.Append(Encoding.ASCII.GetString(new byte[] { (byte)code }));

			return buff.ToString();
		}

		public static string ToContainsOnly(string str, string validChrs)
		{
			StringBuilder buff = new StringBuilder();

			foreach (char chr in str)
				if (validChrs.Contains(chr))
					buff.Append(chr);

			return buff.ToString();
		}

		public static bool ContainsIgnoreCase(string str, string target)
		{
			for (int index = 0; index + target.Length <= str.Length; index++)
				if (EqualsIgnoreCase(str.Substring(index, target.Length), target))
					return true;

			return false;
		}
	}
}
