namespace 基于空间模式匹配的地理场景自动识别系统
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开Shp文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creatArgModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模式定义ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.straightReachToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightangleTurningReachToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barbRiverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contraapertureRiverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模式匹配ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 553);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(860, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.creatArgModelToolStripMenuItem,
            this.模式定义ToolStripMenuItem,
            this.模式匹配ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(860, 28);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开Shp文件ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.文件ToolStripMenuItem.Text = "File";
            // 
            // 打开Shp文件ToolStripMenuItem
            // 
            this.打开Shp文件ToolStripMenuItem.Name = "打开Shp文件ToolStripMenuItem";
            this.打开Shp文件ToolStripMenuItem.Size = new System.Drawing.Size(186, 24);
            this.打开Shp文件ToolStripMenuItem.Text = "Open Shp Files";
            this.打开Shp文件ToolStripMenuItem.Click += new System.EventHandler(this.打开Shp文件ToolStripMenuItem_Click);
            // 
            // creatArgModelToolStripMenuItem
            // 
            this.creatArgModelToolStripMenuItem.Name = "creatArgModelToolStripMenuItem";
            this.creatArgModelToolStripMenuItem.Size = new System.Drawing.Size(140, 24);
            this.creatArgModelToolStripMenuItem.Text = "Scene Modeling";
            this.creatArgModelToolStripMenuItem.Click += new System.EventHandler(this.creatArgModelToolStripMenuItem_Click);
            // 
            // 模式定义ToolStripMenuItem
            // 
            this.模式定义ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.straightReachToolStripMenuItem,
            this.rightangleTurningReachToolStripMenuItem,
            this.barbRiverToolStripMenuItem,
            this.contraapertureRiverToolStripMenuItem});
            this.模式定义ToolStripMenuItem.Name = "模式定义ToolStripMenuItem";
            this.模式定义ToolStripMenuItem.Size = new System.Drawing.Size(125, 24);
            this.模式定义ToolStripMenuItem.Text = "Define Pattern";
            // 
            // straightReachToolStripMenuItem
            // 
            this.straightReachToolStripMenuItem.Name = "straightReachToolStripMenuItem";
            this.straightReachToolStripMenuItem.Size = new System.Drawing.Size(271, 24);
            this.straightReachToolStripMenuItem.Text = "Straight Reach";
            this.straightReachToolStripMenuItem.Click += new System.EventHandler(this.straightReachToolStripMenuItem_Click);
            // 
            // rightangleTurningReachToolStripMenuItem
            // 
            this.rightangleTurningReachToolStripMenuItem.Name = "rightangleTurningReachToolStripMenuItem";
            this.rightangleTurningReachToolStripMenuItem.Size = new System.Drawing.Size(271, 24);
            this.rightangleTurningReachToolStripMenuItem.Text = "Right-angle Turning Reach";
            this.rightangleTurningReachToolStripMenuItem.Click += new System.EventHandler(this.rightangleTurningReachToolStripMenuItem_Click);
            // 
            // barbRiverToolStripMenuItem
            // 
            this.barbRiverToolStripMenuItem.Name = "barbRiverToolStripMenuItem";
            this.barbRiverToolStripMenuItem.Size = new System.Drawing.Size(271, 24);
            this.barbRiverToolStripMenuItem.Text = "Barb River";
            this.barbRiverToolStripMenuItem.Click += new System.EventHandler(this.barbRiverToolStripMenuItem_Click);
            // 
            // contraapertureRiverToolStripMenuItem
            // 
            this.contraapertureRiverToolStripMenuItem.Name = "contraapertureRiverToolStripMenuItem";
            this.contraapertureRiverToolStripMenuItem.Size = new System.Drawing.Size(271, 24);
            this.contraapertureRiverToolStripMenuItem.Text = "Contra-aperture River";
            this.contraapertureRiverToolStripMenuItem.Click += new System.EventHandler(this.contraapertureRiverToolStripMenuItem_Click);
            // 
            // 模式匹配ToolStripMenuItem
            // 
            this.模式匹配ToolStripMenuItem.Name = "模式匹配ToolStripMenuItem";
            this.模式匹配ToolStripMenuItem.Size = new System.Drawing.Size(147, 24);
            this.模式匹配ToolStripMenuItem.Text = "Pattern Matching";
            this.模式匹配ToolStripMenuItem.Click += new System.EventHandler(this.模式匹配ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.退出ToolStripMenuItem.Text = "Exit";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(233, 383);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 6;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Location = new System.Drawing.Point(215, 65);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(633, 485);
            this.axMapControl1.TabIndex = 2;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Location = new System.Drawing.Point(12, 65);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(197, 485);
            this.axTOCControl1.TabIndex = 1;
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Location = new System.Drawing.Point(12, 31);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(836, 28);
            this.axToolbarControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(860, 575);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.axToolbarControl1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Automatic Detection of Fault-controlled Rivers Using Spatial Pattern Matching";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开Shp文件ToolStripMenuItem;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 模式匹配ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 模式定义ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creatArgModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem straightReachToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightangleTurningReachToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barbRiverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contraapertureRiverToolStripMenuItem;
    }
}

