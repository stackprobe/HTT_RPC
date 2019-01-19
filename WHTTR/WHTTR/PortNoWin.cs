using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WHTTR
{
	public partial class PortNoWin : Form
	{
		public PortNoWin()
		{
			InitializeComponent();
		}

		private void PortNoWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void PortNoWin_Shown(object sender, EventArgs e)
		{
			this.PortNo.Text = "" + Gnd.Sd.PortNo;
			this.PortNo.SelectAll();

			SystemTools.PostShown(this);
		}

		private void PortNoWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			try
			{
				int newPortNo = int.Parse(this.PortNo.Text);

				if (newPortNo < 1 || 65535 < newPortNo)
					throw new Exception("1 ～ 65535 の値を入力して下さい。");

				Gnd.Sd.PortNo = newPortNo;
				this.Close();
			}
			catch (Exception ex)
			{
				this.ErrorProv.SetError(this.PortNo, ex.Message);
			}
		}

		private void PortNo_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13)
			{
				e.Handled = true;
				this.BtnOk.Focus();
			}
		}
	}
}
