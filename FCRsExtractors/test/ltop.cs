using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Carto;

namespace test
{
    public partial class ltop : Form
    {
        public ltop()
        {
            InitializeComponent();
        }

        public double ST, LT;

        public IFeatureClass featureClass;

        public string inputpath;

        public string path_tin;

        public string path_voronoi;

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

             

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = "d:\\";
            saveFileDialog1.Filter = "Shaper Files (*.shp)|*.shp|All files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = saveFileDialog1.FileName;
                path_tin = saveFileDialog1.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = "d:\\";
            saveFileDialog1.Filter = "Shaper Files (*.shp)|*.shp|All files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = saveFileDialog1.FileName;
                path_voronoi = saveFileDialog1.FileName;
            }
        }
    }
}
