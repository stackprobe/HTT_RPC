using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Security.Permissions;

namespace WHTTR
{
	public partial class MainWin : Form
	{
		#region ALT_F4 抑止

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
				return;

			base.WndProc(ref m);
		}

		#endregion

		public MainWin()
		{
			InitializeComponent();

			Gnd.Sd.DoLoad();
			Gnd.HTTProc = new HTTProc();
			Gnd.RunIcon = new Icon(GetOffIconFile("11"));
			Gnd.OffIcon_00 = new Icon(GetOffIconFile("00"));
			Gnd.OffIcon_01 = new Icon(GetOffIconFile("01"));
			Gnd.OffIcon_10 = new Icon(GetOffIconFile("10"));
		}

		private static string GetOffIconFile(string suffix)
		{
			string file = "Icon_" + suffix + ".dat";

			if (File.Exists(file) == false)
				file = @"..\..\httr_16_" + suffix + ".ico"; // dev env

			return file;
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.Visible = false;
			this.TaskTrayIcon.Icon = Gnd.RunIcon;
			this.TaskTrayIcon.Visible = true;
			this.MT_Enabled = true;
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MT_Enabled = false;
			this.TaskTrayIcon.Visible = false;

			Gnd.HTTProc.Destroy_BusyDlg();
			Gnd.HTTProc = null;
		}

		private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private bool MT_Enabled;
		private bool MT_Busy;
		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MT_Enabled == false || this.MT_Busy)
				return;

			this.MT_Busy = true;

			try
			{
				if (Gnd.EndProcEvent.WaitOne(0))
				{
					this.Close();
					return;
				}

				{
					Icon icon;

					switch ((Gnd.HTTProc.IsRunning() ? 1 : 0) + (HttServerTools.IsRunning() ? 2 : 0))
					{
						case 0: icon = Gnd.OffIcon_00; break;
						case 1: icon = Gnd.OffIcon_01; break;
						case 2: icon = Gnd.OffIcon_10; break;
						case 3: icon = Gnd.RunIcon; break;

						default:
							throw null;
					}
					if (this.TaskTrayIcon.Icon != icon)
					{
						this.TaskTrayIcon.Icon = icon;

						{
							string text;

							if (Gnd.HTTProc.IsRunning() == false)
								text = "HTT_RPC / HTT is not running";
							else if (HttServerTools.IsRunning())
								text = "HTT_RPC / HttServer is running";
							else
								text = "HTT_RPC / HttServer is not running";

							this.TaskTrayIcon.Text = text;
						}
					}
				}

				if (this.MT_Count % 3 == 0)
				{
					HttServerTools.CheckWinAPIToolsZombies();
				}
				if (this.MT_Count % 100 == 0)
				{
					GC.Collect();
				}
			}
			catch (Exception ex)
			{
				this.MT_Enabled = false;
				throw ex;
			}
			finally
			{
				this.MT_Count++;
				this.MT_Busy = false;
			}
		}

		private void ポート番号ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MT_Enabled = false;
			this.TaskTrayIcon.Visible = false;

			Gnd.HTTProc.Destroy_BusyDlg();
			Gnd.HTTProc = null;

			using (PortNoWin f = new PortNoWin())
			{
				f.ShowDialog();
			}

			Gnd.Sd.DoSave();
			Gnd.HTTProc = new HTTProc();

			this.TaskTrayIcon.Visible = true;
			this.MT_Enabled = true;
		}

		private void 再起動ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MT_Enabled = false;
			this.TaskTrayIcon.Visible = false;

			Gnd.HTTProc.Destroy_BusyDlg();
			Gnd.HTTProc = new HTTProc();

			this.TaskTrayIcon.Visible = true;
			this.MT_Enabled = true;
		}

		private void 詳細設定ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MT_Enabled = false;
			this.TaskTrayIcon.Visible = false;

			Gnd.HTTProc.Destroy_BusyDlg();
			Gnd.HTTProc = null;

			using (ServiceSettingWin f = new ServiceSettingWin())
			{
				f.ShowDialog();
			}

			Gnd.Sd.DoSave();
			Gnd.HTTProc = new HTTProc();

			this.TaskTrayIcon.Visible = true;
			this.MT_Enabled = true;
		}
	}
}
