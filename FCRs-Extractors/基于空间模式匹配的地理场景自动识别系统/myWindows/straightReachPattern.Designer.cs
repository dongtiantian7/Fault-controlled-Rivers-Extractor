namespace 基于空间模式匹配的地理场景自动识别系统
{
    partial class straightReachPattern
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
            this.bthSavePath = new System.Windows.Forms.Button();
            this.textBoxSavePath = new System.Windows.Forms.TextBox();
            this.labelSave = new System.Windows.Forms.Label();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.OkBtn = new System.Windows.Forms.Button();
            this.textBoxLT = new System.Windows.Forms.TextBox();
            this.textBoxCT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bthSavePath
            // 
            this.bthSavePath.Location = new System.Drawing.Point(577, 145);
            this.bthSavePath.Name = "bthSavePath";
            this.bthSavePath.Size = new System.Drawing.Size(75, 26);
            this.bthSavePath.TabIndex = 142;
            this.bthSavePath.Text = "View";
            this.bthSavePath.UseVisualStyleBackColor = true;
            this.bthSavePath.Click += new System.EventHandler(this.bthSavePath_Click);
            // 
            // textBoxSavePath
            // 
            this.textBoxSavePath.Location = new System.Drawing.Point(141, 143);
            this.textBoxSavePath.Name = "textBoxSavePath";
            this.textBoxSavePath.Size = new System.Drawing.Size(417, 25);
            this.textBoxSavePath.TabIndex = 141;
            // 
            // labelSave
            // 
            this.labelSave.AutoSize = true;
            this.labelSave.Location = new System.Drawing.Point(61, 146);
            this.labelSave.Name = "labelSave";
            this.labelSave.Size = new System.Drawing.Size(55, 15);
            this.labelSave.TabIndex = 140;
            this.labelSave.Text = "Output";
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(510, 216);
            this.CancelBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(100, 29);
            this.CancelBtn.TabIndex = 138;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(83, 216);
            this.OkBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(100, 29);
            this.OkBtn.TabIndex = 139;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // textBoxLT
            // 
            this.textBoxLT.Location = new System.Drawing.Point(458, 50);
            this.textBoxLT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxLT.Name = "textBoxLT";
            this.textBoxLT.Size = new System.Drawing.Size(100, 25);
            this.textBoxLT.TabIndex = 146;
            this.textBoxLT.Text = "7000";
            // 
            // textBoxCT
            // 
            this.textBoxCT.Location = new System.Drawing.Point(141, 60);
            this.textBoxCT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxCT.Name = "textBoxCT";
            this.textBoxCT.Size = new System.Drawing.Size(100, 25);
            this.textBoxCT.TabIndex = 145;
            this.textBoxCT.Text = "1.05";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(412, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 15);
            this.label2.TabIndex = 144;
            this.label2.Text = "LT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 15);
            this.label1.TabIndex = 143;
            this.label1.Text = "CT";
            // 
            // straightReachPattern
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 288);
            this.Controls.Add(this.textBoxLT);
            this.Controls.Add(this.textBoxCT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bthSavePath);
            this.Controls.Add(this.textBoxSavePath);
            this.Controls.Add(this.labelSave);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OkBtn);
            this.Name = "straightReachPattern";
            this.Text = "Define Straight Reach Pattern";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bthSavePath;
        private System.Windows.Forms.TextBox textBoxSavePath;
        private System.Windows.Forms.Label labelSave;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.TextBox textBoxLT;
        private System.Windows.Forms.TextBox textBoxCT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}