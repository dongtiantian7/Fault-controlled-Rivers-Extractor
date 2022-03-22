using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 基于空间模式匹配的地理场景自动识别系统
{
    public partial class PatternMatching : Form
    {
        public PatternMatching()
        {
            InitializeComponent();
        }

        public string ModelInputPath;

        public string PatternInputPath;

        public string RiverInputPath;

        public string SavePath = string.Empty;


        private void PattermBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOpenFileDialog = new OpenFileDialog();
            pOpenFileDialog.CheckFileExists = true;//当文件不存在时发出警告
            pOpenFileDialog.Title = "打开Xml文件";
            pOpenFileDialog.Filter = "XML files (*.xml)|*.xml";

            pOpenFileDialog.InitialDirectory = pOpenFileDialog.FileName;

            pOpenFileDialog.Multiselect = false;

            DialogResult pDialogResult = pOpenFileDialog.ShowDialog();

            if (pDialogResult != DialogResult.OK) return;

            PatternInputPath = pOpenFileDialog.FileName;

            //textBox2.Text = pOpenFileDialog.FileName;
            textBoxPattern.Text = System.IO.Path.GetFileName(PatternInputPath);
        }

        private void modelBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOpenFileDialog = new OpenFileDialog();
            pOpenFileDialog.CheckFileExists = true;//当文件不存在时发出警告
            pOpenFileDialog.Title = "打开Xml文件";
            pOpenFileDialog.Filter = "XML files (*.xml)|*.xml";

            pOpenFileDialog.InitialDirectory = pOpenFileDialog.FileName;

            pOpenFileDialog.Multiselect = false;

            DialogResult pDialogResult = pOpenFileDialog.ShowDialog();

            if (pDialogResult != DialogResult.OK) return;

            ModelInputPath = pOpenFileDialog.FileName;

            //textBox2.Text = pOpenFileDialog.FileName;
            textBoxModel.Text = System.IO.Path.GetFileName(ModelInputPath);

        }

        private void riverBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOpenFileDialog = new OpenFileDialog();
            pOpenFileDialog.CheckFileExists = true;//当文件不存在时发出警告
            pOpenFileDialog.Title = "打开Shape文件";
            pOpenFileDialog.Filter = "Shape文件(*.shp)|*.shp";

            pOpenFileDialog.InitialDirectory = pOpenFileDialog.FileName;
            pOpenFileDialog.Multiselect = false;
            DialogResult pDialogResult = pOpenFileDialog.ShowDialog();

            if (pDialogResult != DialogResult.OK) return;

            RiverInputPath = pOpenFileDialog.FileName;

            textBoxRiver.Text = System.IO.Path.GetFileName(RiverInputPath);
        }

        private void bthSavePath_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = "d:\\";
            saveFileDialog1.Filter = "Shaper Files (*.shp)|*.shp|All files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxSavePath.Text = saveFileDialog1.FileName;
            }
        }


        private void OkBtn_Click(object sender, EventArgs e)
        {

            SavePath = textBoxSavePath.Text;
            this.Dispose();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

       
    }
}
