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
    public partial class Fault_analysis : Form
    {
        public Fault_analysis()
        {
            InitializeComponent();
        }

        public  List<double> f1 = new List<double>();
        public  List<double> f2 = new List<double>();
        public  List<double> f3 = new List<double>();

        public List<List<IPoint>> Sumlist = new List<List<IPoint>>();

        public IFeatureClass featureClass;

        public string inputpath1,inputpath2,inputpath3,inputpath4;

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

            inputpath1 = pOpenFileDialog.FileName;

            //textBox2.Text = pOpenFileDialog.FileName;
            textBox1.Text = System.IO.Path.GetFileName(inputpath1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = "c:\\";
            saveFileDialog1.Filter = "Shaper Files (*.shp)|*.shp|All files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = saveFileDialog1.FileName;
                savepath = saveFileDialog1.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            OpenFileDialog pOpenFileDialog = new OpenFileDialog();
            pOpenFileDialog.CheckFileExists = true;//当文件不存在时发出警告
            pOpenFileDialog.Title = "打开Shape文件";
            pOpenFileDialog.Filter = "Shape文件(*.shp)|*.shp";

            pOpenFileDialog.InitialDirectory = pOpenFileDialog.FileName;

            pOpenFileDialog.Multiselect = false;

            DialogResult pDialogResult = pOpenFileDialog.ShowDialog();

            if (pDialogResult != DialogResult.OK) return;

            inputpath2 = pOpenFileDialog.FileName;

            textBox3.Text = System.IO.Path.GetFileName(inputpath2);

         
        }

        private void button6_Click(object sender, EventArgs e)
        {

            OpenFileDialog pOpenFileDialog = new OpenFileDialog();
            pOpenFileDialog.CheckFileExists = true;//当文件不存在时发出警告
            pOpenFileDialog.Title = "打开Shape文件";
            pOpenFileDialog.Filter = "Shape文件(*.shp)|*.shp";

            pOpenFileDialog.InitialDirectory = pOpenFileDialog.FileName;

            pOpenFileDialog.Multiselect = false;

            DialogResult pDialogResult = pOpenFileDialog.ShowDialog();

            if (pDialogResult != DialogResult.OK) return;

            inputpath3 = pOpenFileDialog.FileName;

            textBox4.Text = System.IO.Path.GetFileName(inputpath3);

           
        }

        private void button7_Click(object sender, EventArgs e)
        {

            OpenFileDialog pOpenFileDialog = new OpenFileDialog();
            pOpenFileDialog.CheckFileExists = true;//当文件不存在时发出警告
            pOpenFileDialog.Title = "打开Shape文件";
            pOpenFileDialog.Filter = "Shape文件(*.shp)|*.shp";

            pOpenFileDialog.InitialDirectory = pOpenFileDialog.FileName;

            pOpenFileDialog.Multiselect = false;

            DialogResult pDialogResult = pOpenFileDialog.ShowDialog();

            if (pDialogResult != DialogResult.OK) return;

            inputpath4 = pOpenFileDialog.FileName;

            textBox5.Text = System.IO.Path.GetFileName(inputpath4);

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //readlist(inputpath1);
            //readlist(inputpath2);
            //readlist(inputpath3);
            //readlist(inputpath4);

            //readliststr(inputpath1);
            //readliststr(inputpath2);
            //readliststr(inputpath3);
            //readliststr(inputpath4);

            readliststr1(inputpath1);
            readliststr1(inputpath2);
            readliststr1(inputpath3);
            readliststr1(inputpath4);

            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Dispose();
        }

        public void readliststr(String inputpath)
        {
            int index = inputpath.LastIndexOf("\\");

            string maskPath = inputpath.Remove(index);//线的路径

            //创建工作空间
            IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactory();
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(maskPath, 0);

            string pFileName = System.IO.Path.GetFileName(inputpath);

            //创建要素类实例并将要素类赋值给要素图层的要素类属性
            featureClass = featureWorkspace.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(pFileName));

            IFeatureCursor featureCursor = featureClass.Search(null, false);
            IFeature feature = featureCursor.NextFeature();

            while (feature != null)
            {
                if (featureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {

                    IPolyline polyline = (IPolyline)feature.Shape;

                    //得到line的点集合
                    IPointCollection PointCol = polyline as IPointCollection;

                    double ang_fa = double.Parse(feature.get_Value(2).ToString());

                    if (ang_fa < 90) 
                    {
                        double dis_x = 200 * Math.Cos(ang_fa);

                        double dis_y = 200 * Math.Sin(ang_fa);

                        List<IPoint> line_cur1 = new List<IPoint>();
                        IPoint ps1 = new PointClass();
                        ps1.X = PointCol.get_Point(0).X + dis_x;
                        ps1.Y = PointCol.get_Point(0).Y + dis_y;
                        line_cur1.Add(ps1);
                        IPoint pe1 = new PointClass();
                        pe1.X = PointCol.get_Point(PointCol.PointCount - 1).X + dis_x;
                        pe1.Y = PointCol.get_Point(PointCol.PointCount - 1).Y + dis_y;
                        line_cur1.Add(pe1);
                        Sumlist.Add(line_cur1);


                        List<IPoint> line_cur2 = new List<IPoint>();
                        IPoint ps2 = new PointClass();
                        ps2.X = PointCol.get_Point(0).X - dis_x;
                        ps2.Y = PointCol.get_Point(0).Y - dis_y; ;
                        line_cur2.Add(ps2);
                        IPoint pe2 = new PointClass();
                        pe2.X = PointCol.get_Point(PointCol.PointCount - 1).X - dis_x;
                        pe2.Y = PointCol.get_Point(PointCol.PointCount - 1).Y - dis_y;
                        line_cur2.Add(pe2);
                        Sumlist.Add(line_cur2);
                    }
                    else if (ang_fa < 180)
                    {
                        double dis_x = -170 * Math.Cos(ang_fa);

                        double dis_y = 170 * Math.Sin(ang_fa);

                        List<IPoint> line_cur1 = new List<IPoint>();
                        IPoint ps1 = new PointClass();
                        ps1.X = PointCol.get_Point(0).X + dis_x;
                        ps1.Y = PointCol.get_Point(0).Y - dis_y;
                        line_cur1.Add(ps1);
                        IPoint pe1 = new PointClass();
                        pe1.X = PointCol.get_Point(PointCol.PointCount - 1).X + dis_x;
                        pe1.Y = PointCol.get_Point(PointCol.PointCount - 1).Y - dis_y;
                        line_cur1.Add(pe1);
                        Sumlist.Add(line_cur1);


                        List<IPoint> line_cur2 = new List<IPoint>();
                        IPoint ps2 = new PointClass();
                        ps2.X = PointCol.get_Point(0).X - dis_x;
                        ps2.Y = PointCol.get_Point(0).Y + dis_y; ;
                        line_cur2.Add(ps2);
                        IPoint pe2 = new PointClass();
                        pe2.X = PointCol.get_Point(PointCol.PointCount - 1).X - dis_x;
                        pe2.Y = PointCol.get_Point(PointCol.PointCount - 1).Y + dis_y;
                        line_cur2.Add(pe2);
                        Sumlist.Add(line_cur2);
                    }
                    else if (ang_fa < 270)
                    {
                        double dis_x = -170 * Math.Sin(ang_fa);

                        double dis_y = -170 * Math.Cos(ang_fa);

                        List<IPoint> line_cur1 = new List<IPoint>();
                        IPoint ps1 = new PointClass();
                        ps1.X = PointCol.get_Point(PointCol.PointCount - 1).X - dis_x;
                        ps1.Y = PointCol.get_Point(PointCol.PointCount - 1).Y ;
                        line_cur1.Add(ps1);
                        IPoint pe1 = new PointClass();
                        pe1.X = PointCol.get_Point(0).X - dis_x;
                        pe1.Y = PointCol.get_Point(0).Y;
                        line_cur1.Add(pe1);
                        Sumlist.Add(line_cur1);


                        List<IPoint> line_cur2 = new List<IPoint>();
                        IPoint ps2 = new PointClass();
                        ps2.X = PointCol.get_Point(PointCol.PointCount - 1).X + dis_x;
                        ps2.Y = PointCol.get_Point(PointCol.PointCount - 1).Y;
                        line_cur2.Add(ps2);
                        IPoint pe2 = new PointClass();
                        pe2.X = PointCol.get_Point(0).X + dis_x;
                        pe2.Y = PointCol.get_Point(0).Y;
                        line_cur2.Add(pe2);
                        Sumlist.Add(line_cur2);
                    }
                    else
                    {
                        double dis_x = 170 * Math.Cos(ang_fa);

                        double dis_y = -170 * Math.Sin(ang_fa);

                        List<IPoint> line_cur1 = new List<IPoint>();
                        IPoint ps1 = new PointClass();
                        ps1.X = PointCol.get_Point(0).X + dis_x;
                        ps1.Y = PointCol.get_Point(0).Y - dis_y;
                        line_cur1.Add(ps1);
                        IPoint pe1 = new PointClass();
                        pe1.X = PointCol.get_Point(PointCol.PointCount - 1).X + dis_x;
                        pe1.Y = PointCol.get_Point(PointCol.PointCount - 1).Y - dis_y;
                        line_cur1.Add(pe1);
                        Sumlist.Add(line_cur1);


                        List<IPoint> line_cur2 = new List<IPoint>();
                        IPoint ps2 = new PointClass();
                        ps2.X = PointCol.get_Point(0).X- dis_x;
                        ps2.Y = PointCol.get_Point(0).Y + dis_y; ;
                        line_cur2.Add(ps2);
                        IPoint pe2 = new PointClass();
                        pe2.X = PointCol.get_Point(PointCol.PointCount - 1).X- dis_x;
                        pe2.Y = PointCol.get_Point(PointCol.PointCount - 1).Y + dis_y;
                        line_cur2.Add(pe2);
                        Sumlist.Add(line_cur2);
                    }
                    
                }//if

                f1.Add(double.Parse(feature.get_Value(2).ToString()));
                f2.Add(double.Parse(feature.get_Value(4).ToString()));
                f3.Add(double.Parse(feature.get_Value(3).ToString()));

                f1.Add(double.Parse(feature.get_Value(2).ToString()));
                f2.Add(double.Parse(feature.get_Value(4).ToString()));
                f3.Add(double.Parse(feature.get_Value(3).ToString()));

                feature = featureCursor.NextFeature();
            }//while
        }

        public void readliststr1(String inputpath)
        {
            int index = inputpath.LastIndexOf("\\");

            string maskPath = inputpath.Remove(index);//线的路径

            //创建工作空间
            IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactory();
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(maskPath, 0);

            string pFileName = System.IO.Path.GetFileName(inputpath);

            //创建要素类实例并将要素类赋值给要素图层的要素类属性
            featureClass = featureWorkspace.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(pFileName));

            IFeatureCursor featureCursor = featureClass.Search(null, false);
            IFeature feature = featureCursor.NextFeature();

            while (feature != null)
            {
                if (featureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    IPolyline polyline = (IPolyline)feature.Shape;

                    //得到line的点集合
                    IPointCollection PointCol = polyline as IPointCollection;

                    double ang_fa = double.Parse(feature.get_Value(2).ToString());

                    if (((ang_fa > 45) && (ang_fa <= 135)) || ((ang_fa > 225) && (ang_fa <= 315)))
                    {
                        List<IPoint> line_cur1 = new List<IPoint>();
                        IPoint ps1 = new PointClass();
                        ps1.X = PointCol.get_Point(0).X;
                        ps1.Y = PointCol.get_Point(0).Y - 170;
                        line_cur1.Add(ps1);
                        IPoint pe1 = new PointClass();
                        pe1.X = PointCol.get_Point(PointCol.PointCount - 1).X;
                        pe1.Y = PointCol.get_Point(PointCol.PointCount - 1).Y - 170;
                        line_cur1.Add(pe1);
                        Sumlist.Add(line_cur1);


                        List<IPoint> line_cur2 = new List<IPoint>();
                        IPoint ps2 = new PointClass();
                        ps2.X = PointCol.get_Point(0).X;
                        ps2.Y = PointCol.get_Point(0).Y + 170;
                        line_cur2.Add(ps2);
                        IPoint pe2 = new PointClass();
                        pe2.X = PointCol.get_Point(PointCol.PointCount - 1).X;
                        pe2.Y = PointCol.get_Point(PointCol.PointCount - 1).Y + 170;
                        line_cur2.Add(pe2);
                        Sumlist.Add(line_cur2);
                    }
                    else
                    {
                        List<IPoint> line_cur1 = new List<IPoint>();
                        IPoint ps1 = new PointClass();
                        ps1.X = PointCol.get_Point(0).X - 170;
                        ps1.Y = PointCol.get_Point(0).Y;
                        line_cur1.Add(ps1);
                        IPoint pe1 = new PointClass();
                        pe1.X = PointCol.get_Point(PointCol.PointCount - 1).X - 170;
                        pe1.Y = PointCol.get_Point(PointCol.PointCount - 1).Y;
                        line_cur1.Add(pe1);
                        Sumlist.Add(line_cur1);


                        List<IPoint> line_cur2 = new List<IPoint>();
                        IPoint ps2 = new PointClass();
                        ps2.X = PointCol.get_Point(0).X + 170;
                        ps2.Y = PointCol.get_Point(0).Y;
                        line_cur2.Add(ps2);
                        IPoint pe2 = new PointClass();
                        pe2.X = PointCol.get_Point(PointCol.PointCount - 1).X + 170;
                        pe2.Y = PointCol.get_Point(PointCol.PointCount - 1).Y;
                        line_cur2.Add(pe2);
                        Sumlist.Add(line_cur2);
                    }                   

                }//if

                f1.Add(double.Parse(feature.get_Value(2).ToString()));
                f2.Add(double.Parse(feature.get_Value(4).ToString()));
                f3.Add(double.Parse(feature.get_Value(3).ToString()));

                f1.Add(double.Parse(feature.get_Value(2).ToString()));
                f2.Add(double.Parse(feature.get_Value(4).ToString()));
                f3.Add(double.Parse(feature.get_Value(3).ToString()));
                feature = featureCursor.NextFeature();
            }//while
        }

        public void readlist(String inputpath)
        {
            int index = inputpath.LastIndexOf("\\");

            string maskPath = inputpath.Remove(index);//线的路径

            //创建工作空间
            IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactory();
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(maskPath, 0);

            string pFileName = System.IO.Path.GetFileName(inputpath);

            //创建要素类实例并将要素类赋值给要素图层的要素类属性
            featureClass = featureWorkspace.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(pFileName));

            IFeatureCursor featureCursor = featureClass.Search(null, false);
            IFeature feature = featureCursor.NextFeature();

            while (feature != null)
            {
                if (featureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    IPolyline polyline = (IPolyline)feature.Shape;

                    //得到line的点集合
                    IPointCollection PointCol = polyline as IPointCollection;

                    List<IPoint> line_cur = new List<IPoint>();

                    for (int i = 0; i < PointCol.PointCount; i++)
                    {
                        line_cur.Add(PointCol.get_Point(i));
                    }

                    Sumlist.Add(line_cur);
                }//if

                f1.Add(double.Parse(feature.get_Value(2).ToString()));
                f2.Add(double.Parse(feature.get_Value(4).ToString()));
                f3.Add(double.Parse(feature.get_Value(3).ToString()));
                feature = featureCursor.NextFeature();
            }//while
        }
    }
}
