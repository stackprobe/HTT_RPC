namespace WHTTR
{
	partial class ServiceSettingWin
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceSettingWin));
			this.label1 = new System.Windows.Forms.Label();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.BtnOk = new System.Windows.Forms.Button();
			this.ContentLengthMax = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.WaitResponseMillis = new System.Windows.Forms.TextBox();
			this.ErrorProv = new System.Windows.Forms.ErrorProvider(this.components);
			this.label3 = new System.Windows.Forms.Label();
			this.WaitResponseTimeoutSec = new System.Windows.Forms.TextBox();
			this.BtnDefault = new System.Windows.Forms.Button();
			this.KillWinAPIToolsZombies = new System.Windows.Forms.CheckBox();
			this.MainTab = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.ContentLengthMaxUnit = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.Btn拡張のDefault = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.ErrorProv)).BeginInit();
			this.MainTab.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(50, 100);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(230, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "リクエストメッセージの最大サイズ：";
			// 
			// BtnCancel
			// 
			this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnCancel.Location = new System.Drawing.Point(521, 295);
			this.BtnCancel.Margin = new System.Windows.Forms.Padding(2);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(97, 35);
			this.BtnCancel.TabIndex = 2;
			this.BtnCancel.Text = "キャンセル";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// BtnOk
			// 
			this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOk.Location = new System.Drawing.Point(420, 295);
			this.BtnOk.Margin = new System.Windows.Forms.Padding(2);
			this.BtnOk.Name = "BtnOk";
			this.BtnOk.Size = new System.Drawing.Size(97, 35);
			this.BtnOk.TabIndex = 1;
			this.BtnOk.Text = "OK";
			this.BtnOk.UseVisualStyleBackColor = true;
			this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
			// 
			// ContentLengthMax
			// 
			this.ContentLengthMax.Location = new System.Drawing.Point(287, 97);
			this.ContentLengthMax.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ContentLengthMax.MaxLength = 19;
			this.ContentLengthMax.Name = "ContentLengthMax";
			this.ContentLengthMax.Size = new System.Drawing.Size(190, 27);
			this.ContentLengthMax.TabIndex = 1;
			this.ContentLengthMax.Text = "1234567890123456789";
			this.ContentLengthMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(50, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(234, 20);
			this.label2.TabIndex = 0;
			this.label2.Text = "Wait Response Cycle [Millisecond] :";
			// 
			// WaitResponseMillis
			// 
			this.WaitResponseMillis.Location = new System.Drawing.Point(291, 47);
			this.WaitResponseMillis.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.WaitResponseMillis.MaxLength = 10;
			this.WaitResponseMillis.Name = "WaitResponseMillis";
			this.WaitResponseMillis.Size = new System.Drawing.Size(190, 27);
			this.WaitResponseMillis.TabIndex = 1;
			this.WaitResponseMillis.Text = "1234567890";
			this.WaitResponseMillis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// ErrorProv
			// 
			this.ErrorProv.ContainerControl = this;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(50, 100);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(231, 20);
			this.label3.TabIndex = 2;
			this.label3.Text = "Wait Response Timeout [Second] :";
			// 
			// WaitResponseTimeoutSec
			// 
			this.WaitResponseTimeoutSec.Location = new System.Drawing.Point(291, 97);
			this.WaitResponseTimeoutSec.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.WaitResponseTimeoutSec.MaxLength = 10;
			this.WaitResponseTimeoutSec.Name = "WaitResponseTimeoutSec";
			this.WaitResponseTimeoutSec.Size = new System.Drawing.Size(190, 27);
			this.WaitResponseTimeoutSec.TabIndex = 3;
			this.WaitResponseTimeoutSec.Text = "1234567890";
			this.WaitResponseTimeoutSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// BtnDefault
			// 
			this.BtnDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BtnDefault.Location = new System.Drawing.Point(5, 205);
			this.BtnDefault.Margin = new System.Windows.Forms.Padding(2);
			this.BtnDefault.Name = "BtnDefault";
			this.BtnDefault.Size = new System.Drawing.Size(97, 35);
			this.BtnDefault.TabIndex = 3;
			this.BtnDefault.Text = "デフォルト";
			this.BtnDefault.UseVisualStyleBackColor = true;
			this.BtnDefault.Click += new System.EventHandler(this.BtnDefault_Click);
			// 
			// KillWinAPIToolsZombies
			// 
			this.KillWinAPIToolsZombies.AutoSize = true;
			this.KillWinAPIToolsZombies.Location = new System.Drawing.Point(291, 150);
			this.KillWinAPIToolsZombies.Name = "KillWinAPIToolsZombies";
			this.KillWinAPIToolsZombies.Size = new System.Drawing.Size(213, 24);
			this.KillWinAPIToolsZombies.TabIndex = 4;
			this.KillWinAPIToolsZombies.Text = "Kill WinAPITools.exe Zombies";
			this.KillWinAPIToolsZombies.UseVisualStyleBackColor = true;
			// 
			// MainTab
			// 
			this.MainTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainTab.Controls.Add(this.tabPage1);
			this.MainTab.Controls.Add(this.tabPage2);
			this.MainTab.Location = new System.Drawing.Point(12, 12);
			this.MainTab.Name = "MainTab";
			this.MainTab.SelectedIndex = 0;
			this.MainTab.Size = new System.Drawing.Size(610, 278);
			this.MainTab.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.ContentLengthMaxUnit);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.BtnDefault);
			this.tabPage1.Controls.Add(this.ContentLengthMax);
			this.tabPage1.Location = new System.Drawing.Point(4, 29);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(602, 245);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "基本";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// ContentLengthMaxUnit
			// 
			this.ContentLengthMaxUnit.AutoSize = true;
			this.ContentLengthMaxUnit.Location = new System.Drawing.Point(484, 100);
			this.ContentLengthMaxUnit.Name = "ContentLengthMaxUnit";
			this.ContentLengthMaxUnit.Size = new System.Drawing.Size(48, 20);
			this.ContentLengthMaxUnit.TabIndex = 2;
			this.ContentLengthMaxUnit.Text = "バイト";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.Btn拡張のDefault);
			this.tabPage2.Controls.Add(this.label2);
			this.tabPage2.Controls.Add(this.KillWinAPIToolsZombies);
			this.tabPage2.Controls.Add(this.WaitResponseMillis);
			this.tabPage2.Controls.Add(this.WaitResponseTimeoutSec);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Location = new System.Drawing.Point(4, 29);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(602, 245);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "拡張";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// Btn拡張のDefault
			// 
			this.Btn拡張のDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Btn拡張のDefault.Location = new System.Drawing.Point(5, 205);
			this.Btn拡張のDefault.Margin = new System.Windows.Forms.Padding(2);
			this.Btn拡張のDefault.Name = "Btn拡張のDefault";
			this.Btn拡張のDefault.Size = new System.Drawing.Size(97, 35);
			this.Btn拡張のDefault.TabIndex = 5;
			this.Btn拡張のDefault.Text = "Default";
			this.Btn拡張のDefault.UseVisualStyleBackColor = true;
			this.Btn拡張のDefault.Click += new System.EventHandler(this.Btn拡張のDefault_Click);
			// 
			// ServiceSettingWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(634, 341);
			this.Controls.Add(this.MainTab);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnOk);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ServiceSettingWin";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HTT_RPC / 詳細設定";
			this.TopMost = true;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ServiceSettingWin_FormClosed);
			this.Load += new System.EventHandler(this.ServiceSettingWin_Load);
			this.Shown += new System.EventHandler(this.ServiceSettingWin_Shown);
			((System.ComponentModel.ISupportInitialize)(this.ErrorProv)).EndInit();
			this.MainTab.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.Button BtnOk;
		private System.Windows.Forms.TextBox ContentLengthMax;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox WaitResponseMillis;
		private System.Windows.Forms.ErrorProvider ErrorProv;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox WaitResponseTimeoutSec;
		private System.Windows.Forms.Button BtnDefault;
		private System.Windows.Forms.CheckBox KillWinAPIToolsZombies;
		private System.Windows.Forms.TabControl MainTab;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Label ContentLengthMaxUnit;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button Btn拡張のDefault;
	}
}
