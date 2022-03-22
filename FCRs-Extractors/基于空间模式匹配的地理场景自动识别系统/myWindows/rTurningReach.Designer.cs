namespace 基于空间模式匹配的地理场景自动识别系统
{
    partial class rTurningReach
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxLT = new System.Windows.Forms.TextBox();
            this.textBoxMinAngle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bthSavePath = new System.Windows.Forms.Button();
            this.textBoxSavePath = new System.Windows.Forms.TextBox();
            this.labelSave = new System.Windows.Forms.Label();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.OkBtn = new System.Windows.Forms.Button();
            this.textBoxMaxAngle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxLT
            // 
            this.textBoxLT.Location = new System.Drawing.Point(483, 43);
            this.textBoxLT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxLT.Name = "textBoxLT";
            this.textBoxLT.Size = new System.Drawing.Size(100, 25);
            this.textBoxLT.TabIndex = 155;
            this.textBoxLT.Text = "2000";
            this.textBoxLT.TextChanged += new System.EventHandler(this.textBoxLT_TextChanged);
            // 
            // textBoxMinAngle
            // 
            this.textBoxMinAngle.Location = new System.Drawing.Point(221, 44);
            this.textBoxMinAngle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxMinAngle.Name = "textBoxMinAngle";
            this.textBoxMinAngle.Size = new System.Drawing.Size(100, 25);
            this.textBoxMinAngle.TabIndex = 154;
            this.textBoxMinAngle.Text = "70";
            this.textBoxMinAngle.TextChanged += new System.EventHandler(this.textBoxMinAngle_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(432, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 15);
            this.label2.TabIndex = 153;
            this.label2.Text = "LT";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 152;
            this.label1.Text = "MinAngle";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // bthSavePath
            // 
            this.bthSavePath.Location = new System.Drawing.Point(602, 153);
            this.bthSavePath.Name = "bthSavePath";
            this.bthSavePath.Size = new System.Drawing.Size(75, 26);
            this.bthSavePath.TabIndex = 151;
            this.bthSavePath.Text = "View";
            this.bthSavePath.UseVisualStyleBackColor = true;
            this.bthSavePath.Click += new System.EventHandler(this.bthSavePath_Click);
            // 
            // textBoxSavePath
            // 
            this.textBoxSavePath.Location = new System.Drawing.Point(166, 151);
            this.textBoxSavePath.Name = "textBoxSavePath";
            this.textBoxSavePath.Size = new System.Drawing.Size(417, 25);
            this.textBoxSavePath.TabIndex = 150;
            this.textBoxSavePath.TextChanged += new System.EventHandler(this.textBoxSavePath_TextChanged);
            // 
            // labelSave
            // 
            this.labelSave.AutoSize = true;
            this.labelSave.Location = new System.Drawing.Point(86, 154);
            this.labelSave.Name = "labelSave";
            this.labelSave.Size = new System.Drawing.Size(55, 15);
            this.labelSave.TabIndex = 149;
            this.labelSave.Text = "Output";
            this.labelSave.Click += new System.EventHandler(this.labelSave_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(535, 224);
            this.CancelBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(100, 29);
            this.CancelBtn.TabIndex = 147;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(108, 224);
            this.OkBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(100, 29);
            this.OkBtn.TabIndex = 148;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // textBoxMaxAngle
            // 
            this.textBoxMaxAngle.Location = new System.Drawing.Point(221, 89);
            this.textBoxMaxAngle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxMaxAngle.Name = "textBoxMaxAngle";
            this.textBoxMaxAngle.Size = new System.Drawing.Size(100, 25);
            this.textBoxMaxAngle.TabIndex = 157;
            this.textBoxMaxAngle.Text = "110";
            this.textBoxMaxAngle.TextChanged += new System.EventHandler(this.textBoxMaxAngle_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(87, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 156;
            this.label3.Text = "MaxAngle";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // rTurningReach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 304);
            this.Controls.Add(this.textBoxMaxAngle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxLT);
            this.Controls.Add(this.textBoxMinAngle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bthSavePath);
            this.Controls.Add(this.textBoxSavePath);
            this.Controls.Add(this.labelSave);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OkBtn);
            this.Name = "rTurningReach";
            this.Text = "Define Right-angle Turning Reach Pattern";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLT;
        private System.Windows.Forms.TextBox textBoxMinAngle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bthSavePath;
        private System.Windows.Forms.TextBox textBoxSavePath;
        private System.Windows.Forms.Label labelSave;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.TextBox textBoxMaxAngle;
        private System.Windows.Forms.Label label3;
    }
}