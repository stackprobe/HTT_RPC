using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace WHTTR
{
	public static class HttServerTools
	{
		public const string COMMON_ID = "{7da01163-efa3-4941-a5a6-be0800720d8e}"; // shared_uuid@g
		public static string W_MUTEX_ID { get { return COMMON_ID + "_w"; } }
		public static string MUTEX_ID { get { return COMMON_ID + "_m"; } }
		public static string HTT_ID { get { return COMMON_ID + "_h"; } }
		public static string HTT_SERVICE_ID { get { return COMMON_ID + "_hs"; } }

		public static Mutex GetMutex_W()
		{
			return new Mutex(false, W_MUTEX_ID);
		}

		// Service.exe も MUTEX_ID を掴んで生存チェックするため、事前に W_MUTEX_ID で排他すること。

		public static Mutex GetMutex()
		{
			return new Mutex(false, MUTEX_ID);
		}

		public static bool IsRunning()
		{
			try
			{
				using (Mutex w_mtx = GetMutex_W())
				using (new MutexSection(w_mtx))
				using (Mutex mtx = GetMutex())
				{
					if (mtx.WaitOne(0))
					{
						mtx.ReleaseMutex();
						return false;
					}
				}
				return true;
			}
			catch
			{ }

			return false;
		}

		private const string WIN_API_TOOLS_FILE_ID = "{b46c0dfc-6df3-45e3-9b78-38e3b4f2cd9b}"; // shared_uuid@g
		private static readonly string WIN_API_TOOLS_FILE_ID_SU = Su(WIN_API_TOOLS_FILE_ID);

		private static bool IsWinAPITools(Process proc)
		{
			try
			{
				if (StringTools.ContainsIgnoreCase(proc.ProcessName, WIN_API_TOOLS_FILE_ID_SU))
					return true;
			}
			catch
			{ }

			try
			{
				if (StringTools.ContainsIgnoreCase(proc.MainModule.FileName, WIN_API_TOOLS_FILE_ID))
					return true;
			}
			catch
			{ }

			return false;
		}

		private static string Su(string src)
		{
			return src
				.Replace("{", "")
				.Replace("-", "")
				.Replace("}", "");
		}

		private static List<Process> ZombieProcs = new List<Process>();

		public static void CheckWinAPIToolsZombies()
		{
			if (Gnd.Sd.KillWinAPIToolsZombies == false)
			{
				ZombieProcs.Clear();
				return;
			}
			try
			{
				SystemTools.WriteLog("CWZ_START");

				using (Mutex w_mtx = GetMutex_W())
				using (new MutexSection(w_mtx))
				using (Mutex mtx = GetMutex())
				{
					if (mtx.WaitOne(0))
					{
						SystemTools.WriteLog("MTX Locked -> HTT-Server NOT Running");

						try
						{
							SystemTools.WriteLog("ZPC_1 = " + ZombieProcs.Count);

							if (ZombieProcs.Count == 0)
							{
								foreach (Process proc in Process.GetProcesses())
								{
									try
									{
										if (IsWinAPITools(proc))
										{
											SystemTools.WriteLog("Found Proc, Id = " + proc.Id);

											ZombieProcs.Add(proc);
										}
									}
									catch (Exception e)
									{
										SystemTools.WriteLog("ZPA_" + e);
									}
								}
							}
							else
							{
								foreach (Process proc in ZombieProcs)
								{
									try
									{
										if (proc.HasExited == false)
										{
											{
												List<string> lines = new List<string>();

												lines.Add("このプロセスを強制終了します。");
												lines.Add("プロセスID=" + ToString_Id(proc));
												lines.Add("イメージ名=" + ToString_ProcessName(proc));
												lines.Add("実行ファイル名=" + ToString_MainModule_FileName(proc));

												SystemTools.WriteLog(lines);
											}

											proc.Kill();
										}
										else
										{
											SystemTools.WriteLog("Proc Exited, Id = " + proc.Id);
										}
									}
									catch (Exception e)
									{
										SystemTools.WriteLog("ZPK_" + e);
									}
								}
								ZombieProcs.Clear();
							}
							SystemTools.WriteLog("ZPC_2 = " + ZombieProcs.Count);
						}
						finally
						{
							mtx.ReleaseMutex();
						}
					}
					else
					{
						SystemTools.WriteLog("MTX Lock Failed -> HTT-Server Running");

						ZombieProcs.Clear();
					}
				}
				SystemTools.WriteLog("CWZ_ENDED");
			}
			catch (Exception e)
			{
				SystemTools.WriteLog("CWZ_" + e);
			}
		}

		private static string ToString_Id(Process proc)
		{
			try
			{
				return "" + proc.Id;
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}

		private static string ToString_ProcessName(Process proc)
		{
			try
			{
				return "" + proc.ProcessName;
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}

		private static string ToString_MainModule_FileName(Process proc)
		{
			try
			{
				return "" + proc.MainModule.FileName;
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}
	}
}
