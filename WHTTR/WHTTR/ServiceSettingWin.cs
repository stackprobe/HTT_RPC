using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WHTTR
{
	public partial class ServiceSettingWin : Form
	{
		public ServiceSettingWin()
		{
			InitializeComponent();
		}

		private void ServiceSettingWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void ServiceSettingWin_Shown(object sender, EventArgs e)
		{
			this.DoLoad();
			this.ContentLengthMax.SelectAll();
		}

		private void ServiceSettingWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		// ---- データ入出力 ----

		private void DoLoad()
		{
			this.ContentLengthMax.Text = "";
			this.WaitResponseMillis.Text = "";

			try
			{
				string text = File.ReadAllText(GetServiceConfFile(), Encoding.ASCII);
				text = StringTools.ToContainsOnly(text, StringTools.ASCII);
				string[] tokens = text.Split(':');
				int c = 0;

				this.ContentLengthMax.Text = tokens[c++];
				this.WaitResponseMillis.Text = tokens[c++];
				this.WaitResponseTimeoutSec.Text = tokens[c++];
			}
			catch
			{ }

			this.KillWinAPIToolsZombies.Checked = Gnd.Sd.KillWinAPIToolsZombies;
		}

		private bool DoSave() // ret: ? 成功
		{
			this.ErrorProv.Clear();

			string text = "";

			try
			{
				long value = long.Parse(this.ContentLengthMax.Text);

				if (value < 0 || 1000000000000000000 < value)
					throw new Exception("0 ～ 10^18 の値を入力して下さい。");

				text += value;
			}
			catch (Exception e)
			{
				this.ErrorProv.SetError(this.ContentLengthMax, e.Message);
				return false;
			}

			text += ":";

			try
			{
				int value = int.Parse(this.WaitResponseMillis.Text);

				if (value < 0 || 1000000000 < value)
					throw new Exception("0 ～ 10^9 の値を入力して下さい。");

				text += value;
			}
			catch (Exception e)
			{
				this.ErrorProv.SetError(this.WaitResponseMillis, e.Message);
				return false;
			}

			text += ":";

			try
			{
				int value = int.Parse(this.WaitResponseTimeoutSec.Text);

				if (value < 0 || 1000000000 < value)
					throw new Exception("0 ～ 10^9 の値を入力して下さい。");

				text += value;
			}
			catch (Exception e)
			{
				this.ErrorProv.SetError(this.WaitResponseTimeoutSec, e.Message);
				return false;
			}

			Gnd.Sd.KillWinAPIToolsZombies = this.KillWinAPIToolsZombies.Checked;

			try
			{
				File.WriteAllText(GetServiceConfFile(), text, Encoding.ASCII);
			}
			catch
			{ }

			return true;
		}

		private static string _serviceConfFile;

		private static string GetServiceConfFile()
		{
			if (_serviceConfFile == null)
			{
				_serviceConfFile = "Service.conf";

				if (File.Exists(_serviceConfFile) == false)
					_serviceConfFile = @"..\..\..\..\Service.conf"; // dev env
			}
			return _serviceConfFile;
		}

		// ----

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			if (this.DoSave())
				this.Close();
		}

		private void BtnDefault_Click(object sender, EventArgs e)
		{
			this.ContentLengthMax.Text = "" + 20000000;
			this.WaitResponseMillis.Text = "" + 300;
			this.WaitResponseTimeoutSec.Text = "" + 60;
			this.KillWinAPIToolsZombies.Checked = false;
		}
	}
}
