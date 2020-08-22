using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace WHTTR
{
	public static class SystemTools
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

		// sync > @ AntiWindowsDefenderSmartScreen

		public static void AntiWindowsDefenderSmartScreen()
		{
			WriteLog("awdss_1");

			if (Is初回起動())
			{
				WriteLog("awdss_2");

				foreach (string exeFile in Directory.GetFiles(BootTools.SelfDir, "*.exe", SearchOption.TopDirectoryOnly))
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
					catch (Exception e)
					{
						WriteLog(e);
					}
				}
				WriteLog("awdss_3");
			}
			WriteLog("awdss_4");
		}

		// < sync

		public static bool Is初回起動()
		{
			return Gnd.Sd.Is初回起動();
		}

		public static void WriteLog(object message)
		{
			WriteLog("" + message);
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

		// sync > @ PostShown

		public static void PostShown_GetAllControl(Form f, Action<Control> reaction)
		{
			Queue<Control.ControlCollection> controlTable = new Queue<Control.ControlCollection>();

			controlTable.Enqueue(f.Controls);

			while (1 <= controlTable.Count)
			{
				foreach (Control control in controlTable.Dequeue())
				{
					GroupBox gb = control as GroupBox;

					if (gb != null)
					{
						controlTable.Enqueue(gb.Controls);
					}
					TabControl tc = control as TabControl;

					if (tc != null)
					{
						foreach (TabPage tp in tc.TabPages)
						{
							controlTable.Enqueue(tp.Controls);
						}
					}
					SplitContainer sc = control as SplitContainer;

					if (sc != null)
					{
						controlTable.Enqueue(sc.Panel1.Controls);
						controlTable.Enqueue(sc.Panel2.Controls);
					}
					Panel p = control as Panel;

					if (p != null)
					{
						controlTable.Enqueue(p.Controls);
					}
					reaction(control);
				}
			}
		}

		public static void PostShown(Form f)
		{
			PostShown_GetAllControl(f, control =>
			{
				Control c = new Control[]
				{
					control as TextBox,
					control as NumericUpDown,
				}
				.FirstOrDefault(v => v != null);

				if (c != null)
				{
					if (c.ContextMenuStrip == null)
					{
						ContextMenuStrip menu = new ContextMenuStrip();

						{
							ToolStripMenuItem item = new ToolStripMenuItem();

#if false
							item.Text = "内容をクリップボードにコピー";
							item.Click += (sdr, ev) =>
							{
								try
								{
									Clipboard.SetText(c.Text ?? "");
								}
								catch
								{ }
							};
#else
							item.Text = "項目なし";
							item.Enabled = false;
#endif

							menu.Items.Add(item);
						}

						c.ContextMenuStrip = menu;
					}
				}
			});
		}

		// < sync
	}
}
