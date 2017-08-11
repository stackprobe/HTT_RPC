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
			((System.ComponentModel.ISupportInitialize)(this.ErrorProv)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(251, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Request Content-Length Max [Byte] :";
			// 
			// BtnCancel
			// 
			this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnCancel.Location = new System.Drawing.Point(387, 233);
			this.BtnCancel.Margin = new System.Windows.Forms.Padding(2);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(97, 35);
			this.BtnCancel.TabIndex = 8;
			this.BtnCancel.Text = "キャンセル";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// BtnOk
			// 
			this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOk.Location = new System.Drawing.Point(286, 233);
			this.BtnOk.Margin = new System.Windows.Forms.Padding(2);
			this.BtnOk.Name = "BtnOk";
			this.BtnOk.Size = new System.Drawing.Size(97, 35);
			this.BtnOk.TabIndex = 7;
			this.BtnOk.Text = "OK";
			this.BtnOk.UseVisualStyleBackColor = true;
			this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
			// 
			// ContentLengthMax
			// 
			this.ContentLengthMax.Location = new System.Drawing.Point(270, 29);
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
			this.label2.Location = new System.Drawing.Point(12, 83);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(234, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "Wait Response Cycle [Millisecond] :";
			// 
			// WaitResponseMillis
			// 
			this.WaitResponseMillis.Location = new System.Drawing.Point(270, 80);
			this.WaitResponseMillis.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.WaitResponseMillis.MaxLength = 10;
			this.WaitResponseMillis.Name = "WaitResponseMillis";
			this.WaitResponseMillis.Size = new System.Drawing.Size(190, 27);
			this.WaitResponseMillis.TabIndex = 3;
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
			this.label3.Location = new System.Drawing.Point(12, 134);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(231, 20);
			this.label3.TabIndex = 4;
			this.label3.Text = "Wait Response Timeout [Second] :";
			// 
			// WaitResponseTimeoutSec
			// 
			this.WaitResponseTimeoutSec.Location = new System.Drawing.Point(270, 131);
			this.WaitResponseTimeoutSec.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.WaitResponseTimeoutSec.MaxLength = 10;
			this.WaitResponseTimeoutSec.Name = "WaitResponseTimeoutSec";
			this.WaitResponseTimeoutSec.Size = new System.Drawing.Size(190, 27);
			this.WaitResponseTimeoutSec.TabIndex = 5;
			this.WaitResponseTimeoutSec.Text = "1234567890";
			this.WaitResponseTimeoutSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// BtnDefault
			// 
			this.BtnDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BtnDefault.Location = new System.Drawing.Point(11, 233);
			this.BtnDefault.Margin = new System.Windows.Forms.Padding(2);
			this.BtnDefault.Name = "BtnDefault";
			this.BtnDefault.Size = new System.Drawing.Size(97, 35);
			this.BtnDefault.TabIndex = 9;
			this.BtnDefault.Text = "デフォルト";
			this.BtnDefault.UseVisualStyleBackColor = true;
			this.BtnDefault.Click += new System.EventHandler(this.BtnDefault_Click);
			// 
			// KillWinAPIToolsZombies
			// 
			this.KillWinAPIToolsZombies.AutoSize = true;
			this.KillWinAPIToolsZombies.Location = new System.Drawing.Point(247, 185);
			this.KillWinAPIToolsZombies.Name = "KillWinAPIToolsZombies";
			this.KillWinAPIToolsZombies.Size = new System.Drawing.Size(213, 24);
			this.KillWinAPIToolsZombies.TabIndex = 6;
			this.KillWinAPIToolsZombies.Text = "Kill WinAPITools.exe Zombies";
			this.KillWinAPIToolsZombies.UseVisualStyleBackColor = true;
			// 
			// ServiceSettingWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(495, 279);
			this.Controls.Add(this.KillWinAPIToolsZombies);
			this.Controls.Add(this.BtnDefault);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.WaitResponseTimeoutSec);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.WaitResponseMillis);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnOk);
			this.Controls.Add(this.ContentLengthMax);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ServiceSettingWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HTT_RPC";
			this.TopMost = true;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ServiceSettingWin_FormClosed);
			this.Load += new System.EventHandler(this.ServiceSettingWin_Load);
			this.Shown += new System.EventHandler(this.ServiceSettingWin_Shown);
			((System.ComponentModel.ISupportInitialize)(this.ErrorProv)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

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
	}
}
