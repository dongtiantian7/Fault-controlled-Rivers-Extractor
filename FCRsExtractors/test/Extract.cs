using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;

namespace test
{
    public partial class Extract : Form
    {
        public IFeatureClass featureClass_point;
        public IFeatureClass featureClass_line;

        public string inputpath_line;
        public string inputpath_point;

        public string savepath_str;
        public string savepath_barb;
        public string savepath_right;
        public string savepath_cou;

        public Extract()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOpenFileDialog = new OpenFileDialog();
            pOpenFileDialog.CheckFileExists = true;//当文件不存在时发出警告
            pOpenFileDialog.Title = "打开Shape文件";
            pOpenFileDialog.Filter = "Shape文件(*.shp)|*.shp";

            pOpenFileDialog.InitialDirectory = pOpenFileDialog.FileName;

            pOpenFileDialog.Multiselect = false;

            DialogResult pDialogResult = pOpenFileDialog.ShowDialog();

            if (pDialogResult != DialogResult.OK) return;

            inputpath_line = pOpenFileDialog.FileName;

            //textBox2.Text = pOpenFileDialog.FileName;
            textBox1.Text = System.IO.Path.GetFileName(inputpath_line);

            int index = inputpath_line.LastIndexOf("\\");

            string maskPath = inputpath_line.Remove(index);//线的路径

            //创建工作空间
            IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactory();
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(maskPath, 0);

            string pFileName = System.IO.Path.GetFileName(inputpath_line);

            //创建要素类实例并将要素类赋值给要素图层的要素类属性
            featureClass_line = featureWorkspace.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(pFileName));

            //int num = featureClass_line.Fields.FieldCount;

            //for (int i = 0; i < num; i++)
            //{
            //    comboBox2.Items.Add(featureClass.Fields.get_Field(i).Name);
            //}


        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOpenFileDialog = new OpenFileDialog();
            pOpenFileDialog.CheckFileExists = true;//当文件不存在时发出警告
            pOpenFileDialog.Title = "打开Shape文件";
            pOpenFileDialog.Filter = "Shape文件(*.shp)|*.shp";

            pOpenFileDialog.InitialDirectory = pOpenFileDialog.FileName;

            pOpenFileDialog.Multiselect = false;

            DialogResult pDialogResult = pOpenFileDialog.ShowDialog();

            if (pDialogResult != DialogResult.OK) return;

            inputpath_point = pOpenFileDialog.FileName;

            //textBox2.Text = pOpenFileDialog.FileName;
            textBox6.Text = System.IO.Path.GetFileName(inputpath_point);

            int index = inputpath_point.LastIndexOf("\\");
            string maskPath = inputpath_point.Remove(index);//线的路径

            //创建工作空间
            IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactory();
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(maskPath, 0);

            string pFileName = System.IO.Path.GetFileName(inputpath_point);

            //创建要素类实例并将要素类赋值给要素图层的要素类属性
            featureClass_point = featureWorkspace.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(pFileName));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.InitialDirectory = "d:\\";
            saveFileDialog1.Title = "保存Shape文件";
            saveFileDialog1.Filter = "Shaper Files (*.shp)|*.shp|All files (*.*)|*.*";
            saveFileDialog1.InitialDirectory = saveFileDialog1.FileName;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = saveFileDialog1.FileName;
                savepath_str = saveFileDialog1.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.InitialDirectory = "d:\\";
            saveFileDialog1.Title = "保存Shape文件";
            saveFileDialog1.Filter = "Shaper Files (*.shp)|*.shp|All files (*.*)|*.*";
            saveFileDialog1.InitialDirectory = saveFileDialog1.FileName;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = saveFileDialog1.FileName;
                savepath_right = saveFileDialog1.FileName;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.InitialDirectory = "d:\\";
            saveFileDialog1.Title = "保存Shape文件";
            saveFileDialog1.Filter = "Shaper Files (*.shp)|*.shp|All files (*.*)|*.*";
            saveFileDialog1.InitialDirectory = saveFileDialog1.FileName;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = saveFileDialog1.FileName;
                savepath_barb = saveFileDialog1.FileName;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.InitialDirectory = "d:\\";
            saveFileDialog1.Title = "保存Shape文件";
            saveFileDialog1.Filter = "Shaper Files (*.shp)|*.shp|All files (*.*)|*.*";
            saveFileDialog1.InitialDirectory = saveFileDialog1.FileName;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = saveFileDialog1.FileName;
                savepath_cou = saveFileDialog1.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
