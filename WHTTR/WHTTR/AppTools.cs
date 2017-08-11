using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace WHTTR
{
	public class AppTools
	{
		/// <summary>
		/// HTT.exe 開始前に実行してね！！！
		/// </summary>
		public static void ServiceClear()
		{
			try
			{
				ProcessStartInfo psi = new ProcessStartInfo();

				psi.FileName = GetServiceFile();
				psi.Arguments = "/BEFORE-HTT";

				PostInitPSI(psi);

				Process.Start(psi).WaitForExit();
			}
			catch (Exception e)
			{
				SystemTools.Error(e);
			}
		}

		private static string _serviceFile;

		private static string GetServiceFile()
		{
			if (_serviceFile == null)
			{
				_serviceFile = "Service.exe";

				if (File.Exists(_serviceFile) == false)
					_serviceFile = @"..\..\..\..\Service.exe"; // dev env
			}
			return _serviceFile;
		}

		public static void PostInitPSI(ProcessStartInfo psi)
		{
			switch (Gnd.Sd.ConsoleMode)
			{
				case 0: // hide
					psi.CreateNoWindow = true;
					psi.UseShellExecute = false;
					break;

				case 1: // minimum
					psi.CreateNoWindow = false;
					psi.UseShellExecute = true;
					psi.WindowStyle = ProcessWindowStyle.Minimized;
					break;

				case 2: // normal
					break;

				default:
					throw new Exception("不明なコンソールモード：" + Gnd.Sd.ConsoleMode);
			}
		}
	}
}
