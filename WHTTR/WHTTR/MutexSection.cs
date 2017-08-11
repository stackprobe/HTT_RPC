using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WHTTR
{
	public class MutexSection : IDisposable
	{
		private Mutex _m;

		public MutexSection(Mutex m)
		{
			_m = m;
			_m.WaitOne();
		}

		public void Dispose()
		{
			if (_m != null)
			{
				_m.ReleaseMutex();
				_m = null;
			}
		}
	}
}
