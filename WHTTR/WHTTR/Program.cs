﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WHTTR
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			BootTools.OnBoot();

			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			SystemEvents.SessionEnding += new SessionEndingEventHandler(SessionEnding);

			Gnd.EndProcEvent = new EventWaitHandle(false, EventResetMode.AutoReset, "{b07abec3-0907-4ba5-80b2-49dee440e481}");

			foreach (string arg in SystemTools.GetArgq())
			{
				if (StringTools.EqualsIgnoreCase(arg, "/T"))
				{
					Gnd.EndProcEvent.Set();
					return;
				}
			}
			Mutex procMutex = new Mutex(false, "{22db5921-e899-4040-8706-d15abd436f76}");

			if (procMutex.WaitOne(0) && GlobalProcMtx.Create("{948f0921-4801-403f-8127-302172ae0318}", APP_TITLE))
			{
				CheckSelfDir();
				CheckCopiedExe();

				File.Delete(SystemTools.LOG_FILE);

				SystemTools.AntiWindowsDefenderSmartScreen();

				// HTT.exe ゾンビ対策は HTTProc.ctor でやってる。

				// orig >

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainWin());

				// < orig

				Gnd.Sd.DoSave();

				GlobalProcMtx.Release();
				procMutex.ReleaseMutex();
			}
			procMutex.Close();
		}

		public const string APP_TITLE = "HTT_RPC";
		public const string ERROR_DLG_TITLE = APP_TITLE + " / Error";

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			try
			{
				MessageBox.Show(
					"[Application_ThreadException]\n" + e.Exception,
					ERROR_DLG_TITLE,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
			catch
			{ }

			Environment.Exit(1);
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				MessageBox.Show(
					"[CurrentDomain_UnhandledException]\n" + e.ExceptionObject,
					ERROR_DLG_TITLE,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
			catch
			{ }

			Environment.Exit(2);
		}

		private static void SessionEnding(object sender, SessionEndingEventArgs e)
		{
			Environment.Exit(3);
		}

		private static void CheckSelfDir()
		{
			string dir = BootTools.SelfDir;
			Encoding SJIS = Encoding.GetEncoding(932);

			if (dir != SJIS.GetString(SJIS.GetBytes(dir)))
			{
				MessageBox.Show(
					"Shift_JIS に変換出来ない文字を含むパスからは実行できません。",
					ERROR_DLG_TITLE,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(4);
			}
			if (dir.StartsWith("\\\\"))
			{
				MessageBox.Show(
					"ネットワークフォルダからは実行できません。",
					ERROR_DLG_TITLE,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(5);
			}
		}

		private static void CheckCopiedExe()
		{
			if (File.Exists("Icon_11.dat")) // リリースに含まれるファイル
				return;

			if (Directory.Exists(@"..\Debug")) // ? devenv
				return;

			MessageBox.Show(
				"WHY AM I ALONE ?",
				"",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
				);

			Environment.Exit(6);
		}
	}
}
