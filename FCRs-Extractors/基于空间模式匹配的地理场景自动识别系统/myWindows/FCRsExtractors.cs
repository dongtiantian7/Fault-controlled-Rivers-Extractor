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
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Carto;


namespace 基于空间模式匹配的地理场景自动识别系统
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 打开Shp文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOpenFileDialog = new OpenFileDialog();
            pOpenFileDialog.CheckFileExists = true;//当文件不存在时发出警告
            pOpenFileDialog.Title = "打开Shape文件";
            pOpenFileDialog.Filter = "Shape文件(*.shp)|*.shp";

            pOpenFileDialog.InitialDirectory = pOpenFileDialog.FileName;

            pOpenFileDialog.Multiselect = false;

            DialogResult pDialogResult = pOpenFileDialog.ShowDialog();

            if (pDialogResult != DialogResult.OK) return;

            string pPath = pOpenFileDialog.FileName;

            //如果未选择任何文件，则直接返回程序，不再进行下一步操作
            if (pOpenFileDialog.FileName == "")
                return;

            int index = pPath.LastIndexOf("\\");

            string maskPath = pPath.Remove(index);//线的路径

            //创建工作空间
            IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactory();
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(maskPath, 0);

            string pFileName = System.IO.Path.GetFileName(pPath);

            //创建要素类实例并将要素类赋值给要素图层的要素类属性
            IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(pFileName));

            //显示shp文件
            IFeatureLayer pFlayer = new FeatureLayerClass();

            pFlayer.FeatureClass = featureWorkspace.OpenFeatureClass(pFileName);
            pFlayer.Name = featureClass.AliasName;
            ILayer pLayer = pFlayer as ILayer;

            IMap pmap = axMapControl1.Map;
            pmap.AddLayer(pLayer);
            axMapControl1.ActiveView.Refresh();
        }
      

        public void view_list(string name)
        {
            string path_r = name;

            int index_r = path_r.LastIndexOf("\\");

            string maskPath_r = path_r.Remove(index_r);//线的路径

            //创建工作空间
            IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactory();
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(maskPath_r, 0);

            string pFileName = System.IO.Path.GetFileName(path_r);

            //创建要素类实例并将要素类赋值给要素图层的要素类属性
            IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(pFileName));

            //显示shp文件
            IFeatureLayer pFlayer = new FeatureLayerClass();

            pFlayer.FeatureClass = featureWorkspace.OpenFeatureClass(pFileName);
            pFlayer.Name = featureClass.AliasName;
            ILayer pLayer = pFlayer as ILayer;

            IMap pmap = axMapControl1.Map;
            pmap.AddLayer(pLayer);
            axMapControl1.ActiveView.Refresh();

        }
       
       
        //数据模型构建
        private void creatArgModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ARG arg1 = new ARG();
            arg1.ShowDialog();
             
            if (arg1.SavePath == "")
                return;

            List<IPoint> arg_point1 = new List<IPoint>();
            string savepath_node = arg1.SavePath + "\\ArgNode.shp";
            string savepath_line = arg1.SavePath + "\\ArgLine.shp";
            string savepath_graph = arg1.SavePath + "\\DataGraph.xml";

            List<List<IPoint>> riverList = new List<List<IPoint>>();
            List<String> FromIdList = new List<String>();
            List<String> ToIdList = new List<String>();

            IFeatureClass featureClass = arg1.featureClass;
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
                    riverList.Add(line_cur);
                }//if

                FromIdList.Add(feature.get_Value(arg1.select_index1).ToString());
                ToIdList.Add(feature.get_Value(arg1.select_index2).ToString());

                feature = featureCursor.NextFeature();
            }//while


            SceneModeling getARG = new SceneModeling(arg1.LT, arg1.CT, riverList, FromIdList, ToIdList);
            getARG.CreatARG();


            List<double> nodetypeList = new List<double>();
            List<double> sstr_length = new List<double>();
            List<double> sstr_curb = new List<double>();
            for (int i = 0; i < getARG.ListArgPoint.Count; i++)
            {
                arg_point1.Add(getARG.ListArgPoint[i].PointL);
                sstr_curb.Add(getARG.ListArgPoint[i].Curb);
                sstr_length.Add(getARG.ListArgPoint[i].Length);
                nodetypeList.Add(getARG.ListArgPoint[i].NodeType);
            }
            util.Ponit2Shp(arg_point1, savepath_node, "弯曲度", "节点类型", "长度", sstr_curb, nodetypeList, sstr_length);

            List<double> edgetypeList = new List<double>();
            List<List<IPoint>> arg_line = new List<List<IPoint>>();
            List<double> midlengthList = new List<double>();
            List<double> lturn_angle = new List<double>();
            for (int i = 0; i < getARG.ListArgLine.Count; i++)
            {
                arg_line.Add(getARG.ListArgLine[i].LineL);
                midlengthList.Add(getARG.ListArgLine[i].MidLength);
                lturn_angle.Add(getARG.ListArgLine[i].MidAngle);
                edgetypeList.Add(getARG.ListArgLine[i].LineType);
            }
            util.Line2Shp(arg_line, savepath_line, "过渡长度", "边类型", "转角", midlengthList, edgetypeList, lturn_angle);

            view_list(savepath_line);
            view_list(savepath_node);

            GraphStruct dataGraph = getARG.Graph;

            XmlUtil.DgenerateXmlFile(savepath_graph, dataGraph );

            MessageBox.Show("数据模型构建已完成!");
        } 


        //直线河段 模式定义
        private void straightReachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            straightReachPattern sr = new straightReachPattern();
            sr.ShowDialog();

            if (sr.SavePath == "")
                return;

            //构建模式图
            GraphStruct strPattern = PatternDefine.CreatStr( sr.CT, sr.LT);

            //生成xml文件
            XmlUtil.PgenerateXmlFile(sr.SavePath,strPattern);

            MessageBox.Show("模式定义已完成!");
        }

        //直角转弯 模式定义
        private void rightangleTurningReachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rTurningReach ptr = new rTurningReach();
            ptr.ShowDialog();

            if (ptr.SavePath == "")
                return;

            //构建模式图
            GraphStruct ptrPattern = PatternDefine.CreatRightTurn(ptr.MinAngle, ptr.MaxAngle, ptr.LT);

            //生成xml文件
            XmlUtil.PgenerateXmlFile(ptr.SavePath, ptrPattern);

            MessageBox.Show("模式定义已完成!");
        }

        //倒钩河 模式定义
        private void barbRiverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            barbRiverPattern br = new barbRiverPattern();
            br.ShowDialog();

            if (br.SavePath == "")
                return;

            //构建模式图
            GraphStruct brPattern = PatternDefine.CreatBarb(br.MinAngle, br.LT);

            //生成xml文件
            XmlUtil.PgenerateXmlFile(br.SavePath, brPattern);
            MessageBox.Show("模式定义已完成!");
        }

        //对口河 模式定义
        private void contraapertureRiverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contra_apertureRiver cr = new contra_apertureRiver();
            cr.ShowDialog();

            if (cr.SavePath == "")
                return;

            //构建模式图
            GraphStruct crPattern = PatternDefine.CreatCou1(cr.MinAngle, cr.MaxAngle, cr.LT);

            //生成xml文件
            XmlUtil.PgenerateXmlFile(cr.SavePath, crPattern);
            MessageBox.Show("模式定义已完成!");
        }

        //模式匹配
        private void 模式匹配ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PatternMatching pPatternMatching = new PatternMatching();
            pPatternMatching.ShowDialog();

            if (pPatternMatching.SavePath == "")
                return;

            //获取一个数据模型
            GraphStruct dataGraph = XmlUtil.DreadXmlFile(pPatternMatching.ModelInputPath); ;

            //获取一个模式图
            GraphStruct patternGraph = XmlUtil.PreadXmlFile(pPatternMatching.PatternInputPath);

            //模式匹配
            Matching mat = new Matching(dataGraph, patternGraph);
            mat.StruMatchGraph();

            //读取原始河流线数据
            List<List<IPoint>> riverlist = util.ReadLineShp(pPatternMatching.RiverInputPath);

            //将模式图还原为河段对象
            List<SpecRiver> resultRivers = util.Graph2River(mat.subGraphs, riverlist);

            //存储被识别出的河段对象
            List<string> AttributeName = new List<String>();
            AttributeName.Add("原河流");
            AttributeName.Add("长度");

            util.River2Shp(pPatternMatching.SavePath, resultRivers, AttributeName);

            view_list(pPatternMatching.SavePath);

            MessageBox.Show("模式匹配已完成!");
        }


        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        const int WM_SYSCOMMAND = 0X112;//274
        const int SC_MAXIMIZE = 0XF030;//61488
        const int SC_MINIMIZE = 0XF020;//61472
        const int SC_RESTORE = 0XF120; //61728
        const int SC_CLOSE = 0XF060;//61536
        const int SC_RESIZE_Horizontal = 0XF002;//61442
        const int SC_RESIZE_Vertical = 0XF006;//61446
        const int SC_RESIZE_Both = 0XF008;//61448

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                switch (m.WParam.ToInt32())
                {
                    case SC_MAXIMIZE:
                    case SC_RESTORE:
                    case SC_RESIZE_Horizontal:
                    case SC_RESIZE_Vertical:
                    case SC_RESIZE_Both:
                        if (WindowState == FormWindowState.Minimized)
                        {
                            base.WndProc(ref m);
                        }
                        else
                        {
                            Size beforeResizeSize = this.Size;
                            base.WndProc(ref m);
                            //窗口resize之后的大小
                            Size afterResizeSize = this.Size;
                            //获得变化比例
                            float percentWidth = (float)afterResizeSize.Width / beforeResizeSize.Width;
                            float percentHeight = (float)afterResizeSize.Height / beforeResizeSize.Height;
                            foreach (Control control in this.Controls)
                            {
                                //按比例改变控件大小
                                control.Width = (int)(control.Width * percentWidth);
                                control.Height = (int)(control.Height * percentHeight);
                                //为了不使控件之间覆盖 位置也要按比例变化
                                control.Left = (int)(control.Left * percentWidth);
                                control.Top = (int)(control.Top * percentHeight);
                                //改变控件字体大小
                                control.Font = new Font(control.Font.Name, control.Font.Size * Math.Min(percentHeight, percentHeight), control.Font.Style, control.Font.Unit);
                            }
                        }
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }

    }
}
