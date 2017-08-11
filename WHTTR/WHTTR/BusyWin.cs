using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;

namespace WHTTR
{
	public partial class BusyWin : Form
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

		public delegate bool Interlude_d(); // ret: ? ビジー状態を続ける。
		private Interlude_d D_Interlude;

		public BusyWin(Interlude_d d_interlude)
		{
			this.D_Interlude = d_interlude;

			InitializeComponent();
		}

		private void BusyWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void BusyWin_Shown(object sender, EventArgs e)
		{
			this.MT_Enabled = true;
		}

		private void BusyWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MT_Enabled = false;
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
				if (this.MT_Count < 5)
					return;

				if (this.D_Interlude() == false)
				{
					this.Close();
					return;
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
	}
}
