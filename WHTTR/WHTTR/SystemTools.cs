﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace WHTTR
{
	public class SystemTools
	{
		public static Queue<string> GetArgq()
		{
			string[] args = Environment.GetCommandLineArgs();
			Queue<string> argq = new Queue<string>();

			for (int index = 1; index < args.Length; index++)
				argq.Enqueue(args[index]);

			return argq;
		}

		public static void Error(Exception e)
		{
			try
			{
				MessageBox.Show(
					"" + e,
					Program.ERROR_DLG_TITLE,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
			catch
			{ }
		}

		public static void AntiWindowsDefenderSmartScreen()
		{
			WriteLog("awdss_1");

			if (Gnd.Sd.Is初回起動())
			{
				WriteLog("awdss_2");

				foreach (string exeFile in Directory.GetFiles(BootTools.SelfDir, "*.exe", SearchOption.AllDirectories))
				{
					try
					{
						WriteLog("awdss_exeFile: " + exeFile);

						if (exeFile.ToLower() == BootTools.SelfFile.ToLower())
						{
							WriteLog("awdss_self_noop");
						}
						else
						{
							byte[] exeData = File.ReadAllBytes(exeFile);
							File.Delete(exeFile);
							File.WriteAllBytes(exeFile, exeData);
						}
						WriteLog("awdss_OK");
					}
					catch
					{ }
				}
				WriteLog("awdss_3");
			}
			WriteLog("awdss_4");
		}

		public static void WriteLog(string line)
		{
			WriteLog(new string[] { line });
		}

		public static void WriteLog(IEnumerable<string> lines)
		{
			try
			{
				if (File.Exists(LOG_FILE) && 200000L < new FileInfo(LOG_FILE).Length) // ? 200 KB <
					File.Delete(LOG_FILE);

				List<string> buff = new List<string>();

				foreach (string line in lines)
				{
					buff.Add("[" + DateTime.Now + "]" + line);
				}
				File.AppendAllLines(LOG_FILE, buff, StringTools.ENCODING_SJIS);
			}
			catch
			{ }
		}

		public static string LOG_FILE
		{
			get
			{
				return Path.Combine(
					BootTools.SelfDir,
					Path.GetFileNameWithoutExtension(BootTools.SelfFile) + ".log"
					);
				//return BootTools.SelfFile + ".log"; // old
			}
		}
	}
}
