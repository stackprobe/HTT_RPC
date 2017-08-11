using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WHTTR
{
	public class IntTools
	{
		public static int ToRange(int value, int minval, int maxval)
		{
			return Math.Min(Math.Max(value, minval), maxval);
		}

		public static int Comp(int a, int b)
		{
			if (a < b)
				return -1;

			if (b < a)
				return 1;

			return 0;
		}

		public static bool IsSame(List<int> list1, List<int> list2)
		{
			if (list1.Count != list2.Count)
				return false;

			for (int index = 0; index < list1.Count; index++)
			{
				int value1 = list1[index];
				int value2 = list2[index];

				if (value1 != value2)
					return false;
			}
			return true;
		}

		public static bool Contains(List<int> list, int target)
		{
			foreach (int value in list)
				if (value == target)
					return true;

			return false;
		}
	}
}
