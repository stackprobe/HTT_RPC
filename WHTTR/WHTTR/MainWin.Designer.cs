namespace WHTTR
{
	partial class MainWin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
			this.TaskTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.TaskTrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ポート番号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.詳細設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.再起動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.TaskTrayMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// TaskTrayIcon
			// 
			this.TaskTrayIcon.ContextMenuStrip = this.TaskTrayMenu;
			this.TaskTrayIcon.Text = "HTT_RPC";
			// 
			// TaskTrayMenu
			// 
			this.TaskTrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設定ToolStripMenuItem,
            this.再起動ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.終了ToolStripMenuItem});
			this.TaskTrayMenu.Name = "TaskTrayMenu";
			this.TaskTrayMenu.Size = new System.Drawing.Size(113, 76);
			// 
			// 設定ToolStripMenuItem
			// 
			this.設定ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ポート番号ToolStripMenuItem,
            this.詳細設定ToolStripMenuItem});
			this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
			this.設定ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.設定ToolStripMenuItem.Text = "設定";
			// 
			// ポート番号ToolStripMenuItem
			// 
			this.ポート番号ToolStripMenuItem.Name = "ポート番号ToolStripMenuItem";
			this.ポート番号ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.ポート番号ToolStripMenuItem.Text = "ポート番号";
			this.ポート番号ToolStripMenuItem.Click += new System.EventHandler(this.ポート番号ToolStripMenuItem_Click);
			// 
			// 詳細設定ToolStripMenuItem
			// 
			this.詳細設定ToolStripMenuItem.Name = "詳細設定ToolStripMenuItem";
			this.詳細設定ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.詳細設定ToolStripMenuItem.Text = "詳細設定";
			this.詳細設定ToolStripMenuItem.Click += new System.EventHandler(this.詳細設定ToolStripMenuItem_Click);
			// 
			// 再起動ToolStripMenuItem
			// 
			this.再起動ToolStripMenuItem.Name = "再起動ToolStripMenuItem";
			this.再起動ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.再起動ToolStripMenuItem.Text = "再起動";
			this.再起動ToolStripMenuItem.Click += new System.EventHandler(this.再起動ToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(109, 6);
			// 
			// 終了ToolStripMenuItem
			// 
			this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
			this.終了ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.終了ToolStripMenuItem.Text = "終了";
			this.終了ToolStripMenuItem.Click += new System.EventHandler(this.終了ToolStripMenuItem_Click);
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Interval = 2000;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(-400, -400);
			this.Name = "MainWin";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "HTT_RPC_FourRoses";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.TaskTrayMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon TaskTrayIcon;
		private System.Windows.Forms.ContextMenuStrip TaskTrayMenu;
		private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ポート番号ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 再起動ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem 詳細設定ToolStripMenuItem;
	}
}

