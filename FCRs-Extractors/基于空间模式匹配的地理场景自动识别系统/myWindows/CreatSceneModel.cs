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

namespace 基于空间模式匹配的地理场景自动识别系统
{
    public partial class ARG : Form
    {
        public ARG()
        {
            InitializeComponent();
        }

        public IFeatureClass featureClass;

        public string inputpath;

        /// <summary>
        /// 模型保存路径
        /// </summary>
        public string SavePath = string.Empty;


        public int select_index1;
        public int select_index2;

        public double LT;
        public double CT;

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

            inputpath = pOpenFileDialog.FileName;

            //textBox2.Text = pOpenFileDialog.FileName;
            textBox1.Text = System.IO.Path.GetFileName(inputpath);

            int index = inputpath.LastIndexOf("\\");

            string maskPath = inputpath.Remove(index);//线的路径

            //创建工作空间
            IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactory();
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(maskPath, 0);

            string pFileName = System.IO.Path.GetFileName(inputpath);

            //创建要素类实例并将要素类赋值给要素图层的要素类属性
            featureClass = featureWorkspace.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(pFileName));

            int num = featureClass.Fields.FieldCount;

            for (int i = 0; i < num; i++)
            {
                comboBox1.Items.Add(featureClass.Fields.get_Field(i).Name);
                comboBox2.Items.Add(featureClass.Fields.get_Field(i).Name);
            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            select_index1 = this.comboBox1.SelectedIndex;
            select_index2 = this.comboBox2.SelectedIndex;
            CT = double.Parse(textBox3.Text);
            LT = double.Parse(textBox4.Text);
            SavePath = textBoxSavePath.Text;

            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void bthSavePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog saveFileDialog1 = new FolderBrowserDialog();
            saveFileDialog1.Description = "选择文件保存路径";
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.InitialDirectory = saveFileDialog1.FileName; ;

            //saveFileDialog1.Title = "选择文件保存路径";
            ////saveFileDialog1.Filter = "TIN(*.tin)|*.tin|All files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxSavePath.Text = saveFileDialog1.SelectedPath;
            }
        }
    }
}
