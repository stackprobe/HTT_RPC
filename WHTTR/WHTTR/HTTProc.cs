using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace WHTTR
{
	public class HTTProc
	{
		private Process Proc;

		public HTTProc()
		{
			AppTools.ServiceClear(); // HTT.exe 開始前に実行する！！！

			try
			{
				EndRq(); // 念のため！

				ProcessStartInfo psi = new ProcessStartInfo();

				psi.FileName = GetHttFile();
				psi.Arguments = "/U " + Gnd.HTT_APP_UUID + " /P " + Gnd.Sd.PortNo + " /S " + GetServiceFile();

				AppTools.PostInitPSI(psi);

				this.Proc = Process.Start(psi);
			}
			catch (Exception e)
			{
				SystemTools.Error(e);
			}
		}

		public bool IsRunning()
		{
			if (this.Proc != null && this.Proc.HasExited)
				this.Proc = null;

			return this.Proc != null;
		}

		public void Destroy_BusyDlg()
		{
			try
			{
				EndRq();

				using (BusyWin f = new BusyWin(delegate()
				{
					return this.Proc != null && this.Proc.HasExited == false;
				}
				))
				{
					f.ShowDialog();
				}

				this.Proc = null;
			}
			catch (Exception e)
			{
				SystemTools.Error(e);
			}
		}

		private static void EndRq()
		{
			ProcessStartInfo psi = new ProcessStartInfo();

			psi.FileName = GetHttFile();
			psi.Arguments = "/U " + Gnd.HTT_APP_UUID + " /T";

			AppTools.PostInitPSI(psi);

			Process.Start(psi).WaitForExit();
		}

		private static string _httFile;
		private static string _serviceFile;

		private static string GetHttFile()
		{
			if (_httFile == null)
			{
				_httFile = "HTT.exe";

				if (File.Exists(_httFile) == false)
					_httFile = @"C:\Dev\Main\HTT\HTT\Release\HTT.exe"; // dev env
			}
			return _httFile;
		}

		private static string GetServiceFile()
		{
			if (_serviceFile == null)
			{
				_serviceFile = "Service.dat";

				if (File.Exists(_serviceFile) == false)
					_serviceFile = @"..\..\..\..\Service.dat"; // dev env
			}
			return _serviceFile;
		}
	}
}
