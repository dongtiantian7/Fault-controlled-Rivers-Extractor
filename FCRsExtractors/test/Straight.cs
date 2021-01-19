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
    public partial class Straight : Form
    {
        public Straight()
        {
            InitializeComponent();
        }

        public double ST, LT;

        public IFeatureClass featureClass;

        public string inputpath;

        public string savepath;

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


        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = saveFileDialog1.FileName;
            saveFileDialog1.Filter = "Shaper Files (*.shp)|*.shp|All files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = saveFileDialog1.FileName;
                savepath = saveFileDialog1.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ST = double.Parse(textBox3.Text);
            LT = double.Parse(textBox4.Text);

            int index = inputpath.LastIndexOf("\\");

            string maskPath = inputpath.Remove(index);//线的路径

            //创建工作空间
            IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactory();
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(maskPath, 0);

            string pFileName = System.IO.Path.GetFileName(inputpath);

            //创建要素类实例并将要素类赋值给要素图层的要素类属性
            featureClass = featureWorkspace.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(pFileName));

            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        //private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
        //        e.Handled = true;
        //    //小数点的处理。
        //    if ((int)e.KeyChar == 46)                           //小数点
        //    {
        //        if (textBox1.Text.Length <= 0)
        //            e.Handled = true;   //小数点不能在第一位
        //        else
        //        {
        //            float f;
        //            float oldf;
        //            bool b1 = false, b2 = false;
        //            b1 = float.TryParse(textBox1.Text, out oldf);
        //            b2 = float.TryParse(textBox1.Text + e.KeyChar.ToString(), out f);
        //            if (b2 == false)
        //            {
        //                if (b1 == true)
        //                    e.Handled = true;
        //                else
        //                    e.Handled = false;
        //            }
        //        }
        //    }
        //}

 

        
        
    }
}
