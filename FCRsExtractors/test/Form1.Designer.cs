namespace test
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
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据预处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.提取直线河段ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.提取直角转弯ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.提取对口河ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.提取倒钩河ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.断层解析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.提取同步转弯ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.河流线要素转点要素ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取ARG图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.基于ARG识别河流ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开属性表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axLicenseControl2 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(60, 165);
            this.axLicenseControl1.Margin = new System.Windows.Forms.Padding(4);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.提取直线河段ToolStripMenuItem,
            this.提取直角转弯ToolStripMenuItem,
            this.提取对口河ToolStripMenuItem,
            this.提取倒钩河ToolStripMenuItem,
            this.断层解析ToolStripMenuItem,
            this.提取同步转弯ToolStripMenuItem,
            this.获取ARG图ToolStripMenuItem,
            this.基于ARG识别河流ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1524, 28);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加数据ToolStripMenuItem,
            this.数据预处理ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 添加数据ToolStripMenuItem
            // 
            this.添加数据ToolStripMenuItem.Name = "添加数据ToolStripMenuItem";
            this.添加数据ToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.添加数据ToolStripMenuItem.Text = "打开Shp文件";
            this.添加数据ToolStripMenuItem.Click += new System.EventHandler(this.添加数据ToolStripMenuItem_Click);
            // 
            // 数据预处理ToolStripMenuItem
            // 
            this.数据预处理ToolStripMenuItem.Name = "数据预处理ToolStripMenuItem";
            this.数据预处理ToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.数据预处理ToolStripMenuItem.Text = "数据预处理";
            this.数据预处理ToolStripMenuItem.Click += new System.EventHandler(this.数据预处理ToolStripMenuItem_Click);
            // 
            // 提取直线河段ToolStripMenuItem
            // 
            this.提取直线河段ToolStripMenuItem.Name = "提取直线河段ToolStripMenuItem";
            this.提取直线河段ToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.提取直线河段ToolStripMenuItem.Text = "提取直线河段";
            this.提取直线河段ToolStripMenuItem.Click += new System.EventHandler(this.提取直线河段ToolStripMenuItem_Click);
            // 
            // 提取直角转弯ToolStripMenuItem
            // 
            this.提取直角转弯ToolStripMenuItem.Name = "提取直角转弯ToolStripMenuItem";
            this.提取直角转弯ToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.提取直角转弯ToolStripMenuItem.Text = "提取直角转弯";
            this.提取直角转弯ToolStripMenuItem.Click += new System.EventHandler(this.提取直角转弯ToolStripMenuItem_Click);
            // 
            // 提取对口河ToolStripMenuItem
            // 
            this.提取对口河ToolStripMenuItem.Name = "提取对口河ToolStripMenuItem";
            this.提取对口河ToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.提取对口河ToolStripMenuItem.Text = "提取对口河";
            this.提取对口河ToolStripMenuItem.Click += new System.EventHandler(this.提取对口河ToolStripMenuItem_Click);
            // 
            // 提取倒钩河ToolStripMenuItem
            // 
            this.提取倒钩河ToolStripMenuItem.Name = "提取倒钩河ToolStripMenuItem";
            this.提取倒钩河ToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.提取倒钩河ToolStripMenuItem.Text = "提取倒钩河";
            this.提取倒钩河ToolStripMenuItem.Click += new System.EventHandler(this.提取倒钩河ToolStripMenuItem_Click);
            // 
            // 断层解析ToolStripMenuItem
            // 
            this.断层解析ToolStripMenuItem.Name = "断层解析ToolStripMenuItem";
            this.断层解析ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.断层解析ToolStripMenuItem.Text = "断层解析";
            this.断层解析ToolStripMenuItem.Click += new System.EventHandler(this.断层解析ToolStripMenuItem_Click);
            // 
            // 提取同步转弯ToolStripMenuItem
            // 
            this.提取同步转弯ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.河流线要素转点要素ToolStripMenuItem});
            this.提取同步转弯ToolStripMenuItem.Name = "提取同步转弯ToolStripMenuItem";
            this.提取同步转弯ToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.提取同步转弯ToolStripMenuItem.Text = "提取同步转弯";
            this.提取同步转弯ToolStripMenuItem.Click += new System.EventHandler(this.提取同步转弯ToolStripMenuItem_Click);
            // 
            // 河流线要素转点要素ToolStripMenuItem
            // 
            this.河流线要素转点要素ToolStripMenuItem.Name = "河流线要素转点要素ToolStripMenuItem";
            this.河流线要素转点要素ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.河流线要素转点要素ToolStripMenuItem.Text = "河流线转点";
            this.河流线要素转点要素ToolStripMenuItem.Click += new System.EventHandler(this.河流线要素转点要素ToolStripMenuItem_Click);
            // 
            // 获取ARG图ToolStripMenuItem
            // 
            this.获取ARG图ToolStripMenuItem.Name = "获取ARG图ToolStripMenuItem";
            this.获取ARG图ToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.获取ARG图ToolStripMenuItem.Text = "Extract FCRs";
            this.获取ARG图ToolStripMenuItem.Click += new System.EventHandler(this.获取ARG图ToolStripMenuItem_Click);
            // 
            // 基于ARG识别河流ToolStripMenuItem
            // 
            this.基于ARG识别河流ToolStripMenuItem.Name = "基于ARG识别河流ToolStripMenuItem";
            this.基于ARG识别河流ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.基于ARG识别河流ToolStripMenuItem.Text = "退出";
            this.基于ARG识别河流ToolStripMenuItem.Click += new System.EventHandler(this.基于ARG识别河流ToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开属性表ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(154, 52);
            // 
            // 打开属性表ToolStripMenuItem
            // 
            this.打开属性表ToolStripMenuItem.Name = "打开属性表ToolStripMenuItem";
            this.打开属性表ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.打开属性表ToolStripMenuItem.Text = "打开属性表";
            this.打开属性表ToolStripMenuItem.Click += new System.EventHandler(this.打开属性表ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // axLicenseControl2
            // 
            this.axLicenseControl2.Enabled = true;
            this.axLicenseControl2.Location = new System.Drawing.Point(583, 191);
            this.axLicenseControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.axLicenseControl2.Name = "axLicenseControl2";
            this.axLicenseControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl2.OcxState")));
            this.axLicenseControl2.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl2.TabIndex = 0;
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 28);
            this.axToolbarControl1.Margin = new System.Windows.Forms.Padding(4);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(1509, 28);
            this.axToolbarControl1.TabIndex = 7;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.axTOCControl1.Location = new System.Drawing.Point(0, 64);
            this.axTOCControl1.Margin = new System.Windows.Forms.Padding(4);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(231, 850);
            this.axTOCControl1.TabIndex = 6;
            this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown_1);
            // 
            // axMapControl1
            // 
            this.axMapControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.axMapControl1.Location = new System.Drawing.Point(185, 64);
            this.axMapControl1.Margin = new System.Windows.Forms.Padding(4);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(1290, 850);
            this.axMapControl1.TabIndex = 5;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1524, 828);
            this.Controls.Add(this.axLicenseControl2);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "FCRsExtractors";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 提取直线河段ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 提取直角转弯ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 提取对口河ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 提取倒钩河ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加数据ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打开属性表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据预处理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 断层解析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 提取同步转弯ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 河流线要素转点要素ToolStripMenuItem;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl2;
        private System.Windows.Forms.ToolStripMenuItem 获取ARG图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 基于ARG识别河流ToolStripMenuItem;
    }
}

