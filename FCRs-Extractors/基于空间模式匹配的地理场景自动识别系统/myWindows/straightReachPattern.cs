﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 基于空间模式匹配的地理场景自动识别系统
{
    public partial class straightReachPattern : Form
    {
        public straightReachPattern()
        {
            InitializeComponent();
        }

        public double CT;
        public double LT;
        public string SavePath = string.Empty;

        private void bthSavePath_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = "d:\\";
            saveFileDialog1.Filter = "XML files (*.xml)|*.xml";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxSavePath.Text = saveFileDialog1.FileName;
            }
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            CT = double.Parse(textBoxCT.Text);
            LT = double.Parse(textBoxLT.Text);
            SavePath = textBoxSavePath.Text;

            this.Dispose();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
