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

using System.IO;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.AnalysisTools;         //添加引用
using ESRI.ArcGIS.Geoprocessor;

namespace test
{
    public partial class Form1 : Form
    {
        IFeatureClass featureClass;

        ILayer m_Layer;

        public static List<double> elevation = new List<double>();

        public static List<List<IPoint>> Linelist = new List<List<IPoint>>();
        
        public static List<double> yuan_id = new List<double>();

        public static List<double> length_all = new List<double>();


        public static List<double> length = new List<double>();

        public static List<List<IPoint>> straight_line = new List<List<IPoint>>();

        public static List<double> curb_S = new List<double>();


        public static List<List<IPoint>> Barb_riverline_list = new List<List<IPoint>>();

        public static List<String> from_id = new List<String>();

        public static List<String> to_id = new List<String>();

        public static List<double> intersection = new List<double>();


        public static List<List<IPoint>> Rigntangle_list = new List<List<IPoint>>();

        public static List<double> turn_angle = new List<double>();


        public static List<List<IPoint>> Counterpart_list = new List<List<IPoint>>();

        public static List<double> Coun_curb_S = new List<double>();

        //public static string path_r = "0";


        public Form1()
        {
            InitializeComponent();
        }    

        private void Form1_Load(object sender, EventArgs e)
        {
            axTOCControl1.SetBuddyControl(axMapControl1);

        }


        public string linetoshp(List<List<IPoint>> line, string name, string fn1, string fn2, string fn3, List<double> temp1, List<double> temp2, List<double> temp3)
        {
            //判断生成的文件是否已存在，如果存在，则删掉已存在的文件；
            string inSHPpath = name;
            //string inSHPpath = "D:\\" + name;
            string shpDirName = System.IO.Path.GetDirectoryName(inSHPpath);
            string shpName1 = System.IO.Path.GetFileNameWithoutExtension(inSHPpath);
            string shpFullName = shpName1 + ".shp";
            string prjName = shpName1 + ".prj";
            string dbfName = shpName1 + ".dbf";
            string shxName = shpName1 + ".shx";
            string sbnName = shpName1 + ".sbn";
            string xmlName = shpName1 + ".shp.xml";
            string sbxName = shpName1 + ".sbx";
            if (System.IO.File.Exists(shpDirName + "\\" + shpFullName))
                System.IO.File.Delete(shpDirName + "\\" + shpFullName);
            if (System.IO.File.Exists(shpDirName + "\\" + prjName))
                System.IO.File.Delete(shpDirName + "\\" + prjName);
            if (System.IO.File.Exists(shpDirName + "\\" + dbfName))
                System.IO.File.Delete(shpDirName + "\\" + dbfName);
            if (System.IO.File.Exists(shpDirName + "\\" + shxName))
                System.IO.File.Delete(shpDirName + "\\" + shxName);
            if (System.IO.File.Exists(shpDirName + "\\" + sbnName))
                System.IO.File.Delete(shpDirName + "\\" + sbnName);
            if (System.IO.File.Exists(shpDirName + "\\" + xmlName))
                System.IO.File.Delete(shpDirName + "\\" + xmlName);
            if (System.IO.File.Exists(shpDirName + "\\" + sbxName))
                System.IO.File.Delete(shpDirName + "\\" + sbxName);

            //开始生成shp；
            string shpName = System.IO.Path.GetFileNameWithoutExtension(inSHPpath);   //获取生成的矢量

            //打开生成shapefile的工作空间；
            IWorkspaceFactory pWSF = new ShapefileWorkspaceFactory();
            IFeatureWorkspace pFWS = pWSF.OpenFromFile(shpDirName, 0) as IFeatureWorkspace;

            //开始添加属性字段；
            IFields fields = new FieldsClass();
            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;

            //添加字段“OID”；
            IField oidField = new FieldClass();
            IFieldEdit oidFieldEdit = (IFieldEdit)oidField;
            oidFieldEdit.Name_2 = "OID";
            oidFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            fieldsEdit.AddField(oidField);

            //设置生成图的空间坐标参考系统；
            IGeometryDef geometryDef = new GeometryDefClass();
            IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
            geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
            //geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            //geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;

            ISpatialReference spatialReference = new UnknownCoordinateSystemClass();
            //ISpatialReferenceFactory pSpatialRefFac = new SpatialReferenceEnvironmentClass();
            //IProjectedCoordinateSystem spatialReference = pSpatialRefFac.CreateProjectedCoordinateSystem((int)esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_39); 
            //IProjectedCoordinateSystem spatialReference = pSpatialRefFac.CreateProjectedCoordinateSystem((int)esriSRProjCS4Type.esriSRProjCS_WGS1984_Antarctic_Polar_Stereographic); 
            ISpatialReferenceResolution spatialReferenceResolution = (ISpatialReferenceResolution)spatialReference;
            spatialReferenceResolution.ConstructFromHorizon();
            ISpatialReferenceTolerance spatialReferenceTolerance = (ISpatialReferenceTolerance)spatialReference;
            spatialReferenceTolerance.SetDefaultXYTolerance();
            geometryDefEdit.SpatialReference_2 = spatialReference;

            //添加字段“Shape”;
            IField geometryField = new FieldClass();
            IFieldEdit geometryFieldEdit = (IFieldEdit)geometryField;
            geometryFieldEdit.Name_2 = "Shape";
            geometryFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            geometryFieldEdit.GeometryDef_2 = geometryDef;
            fieldsEdit.AddField(geometryField);

            IField nameField = new FieldClass();
            IFieldEdit nameFieldEdit = (IFieldEdit)nameField;

            //添加字段1；
            nameField = new FieldClass();
            nameFieldEdit = (IFieldEdit)nameField;
            //nameFieldEdit.Name_2 = "经度X";
            nameFieldEdit.Name_2 = fn1;
            nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            nameFieldEdit.Length_2 = 20;
            fieldsEdit.AddField(nameField);

            //添加字段2；
            nameField = new FieldClass();
            nameFieldEdit = (IFieldEdit)nameField;
            //nameFieldEdit.Name_2 = "纬度Y";
            nameFieldEdit.Name_2 = fn2;
            //nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            nameFieldEdit.Length_2 = 20;
            fieldsEdit.AddField(nameField);

            //添加字段3；
            nameField = new FieldClass();
            nameFieldEdit = (IFieldEdit)nameField;
            nameFieldEdit.Name_2 = fn3;
            //nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            nameFieldEdit.Length_2 = 20;
            fieldsEdit.AddField(nameField);

            IFieldChecker fieldChecker = new FieldCheckerClass();
            IEnumFieldError enumFieldError = null;
            IFields validatedFields = null;
            fieldChecker.ValidateWorkspace = (IWorkspace)pFWS;
            fieldChecker.Validate(fields, out enumFieldError, out validatedFields);

            //在工作空间中生成FeatureClass;
            IFeatureClass pNewFeaCls = pFWS.CreateFeatureClass(shpName, validatedFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");

            for (int i = 0; i < line.Count(); i++)
            {
                //当前线中的折点个数
                int pointcount = line[i].Count();

                IPolyline pl = new PolylineClass();
                IPointCollection ptc = pl as IPointCollection;

                object missing = Type.Missing;

                for (int j = 0; j < pointcount; j++)
                {
                    IPoint cpoint = line[i][j];

                    ptc.AddPoint(cpoint, missing, missing);
                }

                (pl as ITopologicalOperator).Simplify();

                //IPointArray ss = new PointArrayClass();

                //IMap pmap = axMapControl1.Map; ;
                //IActiveView pactive = pmap as IActiveView;
                //IMarkerElement pmarke = new MarkerElementClass();
                //IElement pele = pmarke as IElement;
                //pele.Geometry = p1;

                //IGraphicsContainer pgra;
                //pgra = pmap as IGraphicsContainer;
                //pgra.AddElement(pmarke as IElement, 0);
                //pactive.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                //ss.Add(p1);

                IFeature feature = pNewFeaCls.CreateFeature();
                feature.Shape = pl;
                feature.set_Value(2, temp1[i]);
                feature.set_Value(3, temp2[i]);
                feature.set_Value(4, temp3[i]);
                feature.Store();
            }//for

            return inSHPpath;
        }

        //把点序列按照河流序号存储为点图层
        public string ponittoshp(List<List<IPoint>> river, string name)
        {
            //判断生成的文件是否已存在，如果存在，则删掉已存在的文件；
            string inSHPpath = name;
            //string inSHPpath = "D:\\" + "riverpoint";//默认把点图层存储到
            string shpDirName = System.IO.Path.GetDirectoryName(inSHPpath);
            string shpName1 = System.IO.Path.GetFileNameWithoutExtension(inSHPpath);
            string shpFullName = shpName1 + ".shp";
            string prjName = shpName1 + ".prj";
            string dbfName = shpName1 + ".dbf";
            string shxName = shpName1 + ".shx";
            string sbnName = shpName1 + ".sbn";
            string xmlName = shpName1 + ".shp.xml";
            string sbxName = shpName1 + ".sbx";
            if (System.IO.File.Exists(shpDirName + "\\" + shpFullName))
                System.IO.File.Delete(shpDirName + "\\" + shpFullName);
            if (System.IO.File.Exists(shpDirName + "\\" + prjName))
                System.IO.File.Delete(shpDirName + "\\" + prjName);
            if (System.IO.File.Exists(shpDirName + "\\" + dbfName))
                System.IO.File.Delete(shpDirName + "\\" + dbfName);
            if (System.IO.File.Exists(shpDirName + "\\" + shxName))
                System.IO.File.Delete(shpDirName + "\\" + shxName);
            if (System.IO.File.Exists(shpDirName + "\\" + sbnName))
                System.IO.File.Delete(shpDirName + "\\" + sbnName);
            if (System.IO.File.Exists(shpDirName + "\\" + xmlName))
                System.IO.File.Delete(shpDirName + "\\" + xmlName);
            if (System.IO.File.Exists(shpDirName + "\\" + sbxName))
                System.IO.File.Delete(shpDirName + "\\" + sbxName);

            //开始生成shp；
            string shpName = System.IO.Path.GetFileNameWithoutExtension(inSHPpath);   //获取生成的矢量

            //打开生成shapefile的工作空间；
            IWorkspaceFactory pWSF = new ShapefileWorkspaceFactory();
            IFeatureWorkspace pFWS = pWSF.OpenFromFile(shpDirName, 0) as IFeatureWorkspace;

            //开始添加属性字段；
            IFields fields = new FieldsClass();
            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;

            //添加字段“OID”；
            IField oidField = new FieldClass();
            IFieldEdit oidFieldEdit = (IFieldEdit)oidField;
            oidFieldEdit.Name_2 = "OID";
            oidFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            fieldsEdit.AddField(oidField);

            //设置生成图的空间坐标参考系统；
            IGeometryDef geometryDef = new GeometryDefClass();
            IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
            //geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
            geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            //geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;

            //ISpatialReference spatialReference = new UnknownCoordinateSystemClass();     
            ISpatialReferenceFactory pSpatialRefFac = new SpatialReferenceEnvironmentClass();
            IProjectedCoordinateSystem spatialReference = pSpatialRefFac.CreateProjectedCoordinateSystem((int)esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_39);

            ISpatialReferenceResolution spatialReferenceResolution = (ISpatialReferenceResolution)spatialReference;
            spatialReferenceResolution.ConstructFromHorizon();
            ISpatialReferenceTolerance spatialReferenceTolerance = (ISpatialReferenceTolerance)spatialReference;
            spatialReferenceTolerance.SetDefaultXYTolerance();
            geometryDefEdit.SpatialReference_2 = spatialReference;

            //添加字段“Shape”;
            IField geometryField = new FieldClass();
            IFieldEdit geometryFieldEdit = (IFieldEdit)geometryField;
            geometryFieldEdit.Name_2 = "Shape";
            geometryFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            geometryFieldEdit.GeometryDef_2 = geometryDef;
            fieldsEdit.AddField(geometryField);


            //添加字段1记录点所在的河流序号；
            IField nameField = new FieldClass();
            IFieldEdit nameFieldEdit = (IFieldEdit)nameField;
            nameFieldEdit.Name_2 = "所属河流";
            nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
            nameFieldEdit.Length_2 = 20;
            fieldsEdit.AddField(nameField);

            IFieldChecker fieldChecker = new FieldCheckerClass();
            IEnumFieldError enumFieldError = null;
            IFields validatedFields = null;
            fieldChecker.ValidateWorkspace = (IWorkspace)pFWS;
            fieldChecker.Validate(fields, out enumFieldError, out validatedFields);

            //在工作空间中生成FeatureClass;
            IFeatureClass pNewFeaCls = pFWS.CreateFeatureClass(shpName, validatedFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");

            //记录点的所属河流
            for (int i = 0; i < river.Count; i++)
            {
                for (int j = 0; j < river[i].Count; j++)
                {
                    IPoint pl = new PointClass();
                    pl.X = river[i][j].X;
                    pl.Y = river[i][j].Y;

                    IFeature feature = pNewFeaCls.CreateFeature();
                    feature.Shape = pl;
                    feature.set_Value(2, i);

                    feature.Store();
                }

            }

            return inSHPpath;
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


        private void 添加数据ToolStripMenuItem_Click(object sender, EventArgs e)
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

            //axMapControl1.ClearLayers();
            //axMapControl1.AddLayer(pFlayer);
            //axMapControl1.Refresh();

            //得到feature
            IFeatureCursor featureCursor = featureClass.Search(null, false);
            IFeature feature = featureCursor.NextFeature();

            //while (feature != null)
            //{
            //    if (featureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
            //    {
            //        //from_id.Add(feature.get_Value(feature.Fields.FindField("FROM_NODE")).ToString());

            //        //to_id.Add(feature.get_Value(feature.Fields.FindField("TO_NODE")).ToString());

            //        //length_all.Add(double.Parse(feature.get_Value(feature.Fields.FindField("Shape_Leng")).ToString()));

            //        IPolyline polyline = (IPolyline)feature.Shape;

            //        //得到line的点集合
            //        IPointCollection PointCol = polyline as IPointCollection;

            //        List<IPoint> line_cur = new List<IPoint>();

            //        for (int i = 0; i < PointCol.PointCount; i++)
            //        {
            //            line_cur.Add(PointCol.get_Point(i));
            //        }

            //        Linelist.Add(line_cur);
            //    }//if

            //    feature = featureCursor.NextFeature();

            //}//while

        }

        private void 提取直线河段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double LT1, ST1;
            Straight straight1 = new Straight();
            straight1.ShowDialog();

            Linelist.Clear();
            featureClass = straight1.featureClass;
            LT1 = straight1.LT;
            ST1 = straight1.ST;

            IFeatureCursor featureCursor = featureClass.Search(null, false);
            IFeature feature = featureCursor.NextFeature();

            while (feature != null)
            {
                if (featureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    IPolyline polyline = (IPolyline)feature.Shape;

                    //IPolygon polyg = (IPolygon)feature.Shape;

                    //得到line的点集合
                    IPointCollection PointCol = polyline as IPointCollection;

                    List<IPoint> line_cur = new List<IPoint>();

                    for (int i = 0; i < PointCol.PointCount; i++)
                    {
                        line_cur.Add(PointCol.get_Point(i));
                    }

                    Linelist.Add(line_cur);
                }//if
                feature = featureCursor.NextFeature();
            }//while

            //straight_line = get_straight.getStraightLine(Linelist, LT1, ST1);

            util.linetoshp(straight_line, straight1.savepath, "原id", "弯曲系数", "长度", yuan_id, curb_S, length);

            view_list(straight1.savepath);

        }

        private void 提取倒钩河ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double ST1, LT1;
            Barbriver barbriver1 = new Barbriver();
            barbriver1.ShowDialog();

            Linelist.Clear();
            from_id.Clear();
            to_id.Clear();
            featureClass = barbriver1.featureClass;
            ST1 = barbriver1.ST;
            LT1 = barbriver1.LT;

            IFeatureCursor featureCursor = featureClass.Search(null, false);
            IFeature feature = featureCursor.NextFeature();

            //int line_count = 0;

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

                    Linelist.Add(line_cur);
                }//if

                from_id.Add(feature.get_Value(barbriver1.select_index1).ToString());

                to_id.Add(feature.get_Value(barbriver1.select_index).ToString());

                feature = featureCursor.NextFeature();

            }//while

            //Barb_riverline_list = Get_Barbriver.getBarbriver(Linelist, from_id, to_id, ST1, LT1);

            util.linetoshp(Barb_riverline_list, barbriver1.savepath, "原id", "交汇角", "长度", yuan_id, intersection, length);

            view_list(barbriver1.savepath);
        }

        private void 提取直角转弯ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //double ST1, LT1, MD1, AT1;

            //Rightangle rightangle1 = new Rightangle();
            //rightangle1.ShowDialog();

            //Linelist.Clear();
            //featureClass = rightangle1.featureClass;
            //LT1 = rightangle1.LT;
            //ST1 = rightangle1.ST;
            //MD1 = rightangle1.MD;
            //AT1 = rightangle1.AT;

            //IFeatureCursor featureCursor = featureClass.Search(null, false);
            //IFeature feature = featureCursor.NextFeature();

            //while (feature != null)
            //{
            //    if (featureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
            //    {
            //        IPolyline polyline = (IPolyline)feature.Shape;

         

            //        //得到line的点集合
            //        IPointCollection PointCol = polyline as IPointCollection;

            //        List<IPoint> line_cur = new List<IPoint>();

            //        for (int i = 0; i < PointCol.PointCount; i++)
            //        {
            //            line_cur.Add(PointCol.get_Point(i));
            //        }

            //        Linelist.Add(line_cur);
            //    }//if
            //    feature = featureCursor.NextFeature();
            //}//while

            //Rigntangle_list = Get_Rightangle.getRightangle(Linelist, ST1, LT1, MD1, AT1);

            //util.linetoshp(Rigntangle_list, rightangle1.savepath, "原id", "转折角", "长度", yuan_id, turn_angle, length);

            //view_list(rightangle1.savepath);
        }

        private void 提取对口河ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //double ST1, LT1;
            //Counterpart counterpart1 = new Counterpart();
            //counterpart1.ShowDialog();

            //Linelist.Clear();
            //from_id.Clear();
            //to_id.Clear();

            //featureClass = counterpart1.featureClass;
            //ST1 = counterpart1.ST;
            //LT1 = counterpart1.LT;

            //IFeatureCursor featureCursor = featureClass.Search(null, false);
            //IFeature feature = featureCursor.NextFeature();

            //while (feature != null)
            //{
            //    if (featureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
            //    {  
            //        IPolyline polyline = (IPolyline)feature.Shape;

            //        //得到line的点集合
            //        IPointCollection PointCol = polyline as IPointCollection;

            //        List<IPoint> line_cur = new List<IPoint>();

            //        for (int i = 0; i < PointCol.PointCount; i++)
            //        {
            //            line_cur.Add(PointCol.get_Point(i));
            //        }

            //        Linelist.Add(line_cur);
            //    }//if

            //    //from_id.Add(feature.get_Value(counterpart1.select_index1).ToString());

            //    to_id.Add(feature.get_Value(counterpart1.select_index).ToString());

            //    feature = featureCursor.NextFeature();

            //}//while

            //Counterpart_list = Get_Counterpart.getCounterpart(Linelist, to_id, ST1, LT1);

            //util.linetoshp(Counterpart_list, counterpart1.savepath, "原id", "弯曲度", "长度", yuan_id, Coun_curb_S, length);

            //view_list(counterpart1.savepath);
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            //if (e.button == 2)
            //{
            //    contextMenuStrip1.Show(axTOCControl1, e.x, e.y);
            //}
        }

        private void axTOCControl1_OnMouseDown_1(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (axMapControl1.LayerCount > 0)
            {
                esriTOCControlItem pItem = new esriTOCControlItem();

                m_Layer = new FeatureLayerClass();

                IBasicMap pBasicMap = new MapClass();

                object pOther = new object();

                object pIndex = new object();

                axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pBasicMap, ref m_Layer, ref pOther, ref pIndex);
            }


            if (e.button == 2)
            {
                contextMenuStrip1.Show(axTOCControl1, e.x, e.y);
            }

        }

        private void 打开属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            属性表 attributeTable = new 属性表();
            attributeTable.CreateAttributeTable(m_Layer);
            attributeTable.ShowDialog();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (axMapControl1.LayerCount > 0)
            {

                if (m_Layer != null)
                {
                    axMapControl1.Map.DeleteLayer(m_Layer);
                }
            }
        }

        //计算流向
        public List<int> calcu_flow(List<double> ele, List<List<IPoint>> Llist)
        {
            List<int> toid = new List<int>();

            int num = Llist.Count();

            for (int i = 0; i < num; i++)
            {
                int tempf = -10;

                double min = ele[i];

                int min_id = i; 

                for (int j = 0; j < num; j++)
                {
                    if (i == j) continue;

                    tempf = util.adjacent(Llist[i], Llist[j]);

                    if ((tempf > 0) && (ele[j] < min))
                    {
                        min = ele[j]; min_id = j;   
                    }
                }

                if (min_id == i)
                    toid.Add(0);
                else
                    toid.Add(min_id);
            }

            return toid;
        }

        private void 数据预处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //获得服务对象名称
            //IAGSServerObjectName pServerObjectName = GetMapServer("http://services.arcgisonline.com/ArcGIS/services", "ESRI_Imagery_World_2D", false);
            //IAGSServerObjectName pServerObjectName = GetMapServer("http://localhost:6080/arcgis/services", "MyMapService", false);  


            ////连接server服务器
            //IAGSServerConnectionFactory pAGSServerConnFactory = new AGSServerConnectionFactoryClass();
            //IAGSServerConnection pAGSServerConn = new AGSServerConnectionClass();

            //IPropertySet pPropSet = new PropertySetClass();

            //pPropSet.SetProperty("url", "http://localhost:6080/arcgis/services");

            //pAGSServerConn = pAGSServerConnFactory.Open(pPropSet, 0);

            //if (pAGSServerConn == null) return;

            ////获得所有server服务
            //IAGSEnumServerObjectName pAGSEnumServerObjName = pAGSServerConn.ServerObjectNames;

            //if (pAGSEnumServerObjName == null) return;

            //pAGSEnumServerObjName.Reset();

            ////具体某个server服务
            //IAGSServerObjectName pAGSServerObjName = new AGSServerObjectNameClass();

            //pAGSServerObjName = pAGSEnumServerObjName.Next();

            //IMapServer pMapServer = null;

            //while (pAGSServerObjName.Name != null)
            //{
            //    //查找制定名称的server服务AllVector
            //    //if (pAGSServerObjName.Name == "AllVector")
            //    if (pAGSServerObjName.Name == "FeaService")
            //    {
            //        IName pName = pAGSServerObjName as IName;

            //        pMapServer = pName.Open() as IMapServer;

            //        break;
            //    }

            //    pAGSEnumServerObjName.Next();
            //}

            //if (pMapServer == null) return;

            ////获取该server服务的所有服务信息
            //IMapServerInfo pMapServerInfo = new MapServerInfoClass();
            //pMapServerInfo = pMapServer.GetServerInfo(pMapServer.DefaultMapName);

            ////获取该server服务下的所有图层信息
            //IMapLayerInfos pMapLayerInfos = new MapLayerInfosClass();
            //pMapLayerInfos = pMapServerInfo.MapLayerInfos;

            //IMapLayerInfo pMapLayerInfo = new MapLayerInfoClass();


            //for (int i = 0; i < pMapLayerInfos.Count; i++)
            //{
            //    //定位到具体的一个图层
            //    pMapLayerInfo = pMapLayerInfos.get_Element(i);

            //    //判断当前是否为数据图层
            //    if (pMapLayerInfo.Fields == null) continue;


            //    //后面是查找具体的某个图层的数据

            //    //判断图层中是否存在位站码的字段
            //    int fieldCount = pMapLayerInfo.Fields.FieldCount;    //获取字段个数


            //    //Identity identity = new ESRI.ArcGIS.ADF.Identity("yourusername", "yourpassword", "yourdomain");


            //    ESRI.ArcGIS.Server.GISServerConnectionClass gisconnection = new ESRI.ArcGIS.Server.GISServerConnectionClass();
            //    gisconnection.Connect("SKY-20180428CRO");
            //    ESRI.ArcGIS.Server.IServerObjectManager SOM = gisconnection.ServerObjectManager;

            //    IServerContext pServerContext = SOM.CreateServerContext("FeaService", "MapServer");

            //    IMapDescription pMD = pMapServerInfo.DefaultMapDescription;

            //    ISpatialFilter pSpatialFilter = pServerContext.CreateObject("esriGeodatabase.SpatialFilter") as ISpatialFilter;
            //    pSpatialFilter.Geometry = pMD.MapArea.Extent;
            //    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

            //    IRecordSet pRecordSet = pMapServer.QueryFeatureData(pMD.Name, 0, (IQueryFilter)pSpatialFilter);
            //    //存储查询的位站码,站名记录
            //    IFeatureCursor pFeatCursor = pRecordSet.get_Cursor(false) as IFeatureCursor;
            //    IFeature feature = pFeatCursor.NextFeature();

            //    int i1i2 = fieldCount;
               
            //}





            //IAGSServerObjectName pServerObjectName = GetMapServer("http://localhost:6080/arcgis/services", "Temp/ft", false);
            //IName pName = (IName)pServerObjectName;

            ////访问地图服务
            //IAGSServerObject pServerObject = (IAGSServerObject)pName.Open();
            //IMapServer pMapServer = (IMapServer)pServerObject;


            //ESRI.ArcGIS.Carto.IMapServerLayer pMapServerLayer = new ESRI.ArcGIS.Carto.MapServerLayerClass();
            ////连接地图服务
            //pMapServerLayer.ServerConnect(pServerObjectName, pMapServer.DefaultMapName);

            ////添加数据图层
            //axMapControl1.AddLayer(pMapServerLayer as ILayer);

            //axMapControl1.Refresh();

            //ESRI.ArcGIS.Server.GISServerConnectionClass gisconnection = new ESRI.ArcGIS.Server.GISServerConnectionClass();
            //gisconnection.Connect("localhost");
            //ESRI.ArcGIS.Server.IServerObjectManager som = gisconnection.ServerObjectManager;
            //string servertype = "MapServer";
            ////string serverobjectname = "china";
            //ESRI.ArcGIS.Server.IServerContext servercontext = som.CreateServerContext(pServerObjectName.ToString(), servertype);
            //IMapServer ms = (IMapServer)servercontext.ServerObject;
            //IMapServerObjects pMapServerObjs = pMapServer as IMapServerObjects;

            //IMap pMap = pMapServerObjs.get_Map(pMapServer.DefaultMapName);

            //IFeatureLayer pFlayer = pMap.get_Layer(0) as IFeatureLayer;

            ////IFeatureLayer pFlayer = axMapControl1.get_Layer(1) as IFeatureLayer;
            //featureClass = pFlayer.FeatureClass;
            //IFeatureCursor featureCursor = featureClass.Search(null, false);
            //IFeature feature = featureCursor.NextFeature();

            //while (feature != null)
            //{
            //    if (featureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
            //    {
            //        IPolyline polyline = (IPolyline)feature.Shape;

            //        //得到line的点集合
            //        IPointCollection PointCol = polyline as IPointCollection;

            //        List<IPoint> line_cur = new List<IPoint>();

            //        for (int i = 0; i < PointCol.PointCount; i++)
            //        {
            //            line_cur.Add(PointCol.get_Point(i));
            //        }

            //        Linelist.Add(line_cur);
            //    }//if
            //    feature = featureCursor.NextFeature();
            //}//while
           
        }

        //public IAGSServerObjectName GetMapServer(string pHostOrUrl, string pServiceName, bool pIsLAN)
        //{
        //    //设置连接属性
        //    IPropertySet pPropertySet = new PropertySetClass();
        //    if (pIsLAN)
        //        pPropertySet.SetProperty("machine", pHostOrUrl);
        //    else
        //        pPropertySet.SetProperty("url", pHostOrUrl);

        //    //打开连接
        //    IAGSServerConnectionFactory pFactory = new AGSServerConnectionFactory();
        //    //Type factoryType = Type.GetTypeFromProgID(
        //    //    "esriGISClient.AGSServerConnectionFactory");
        //    //IAGSServerConnectionFactory agsFactory = (IAGSServerConnectionFactory)
        //    //    Activator.CreateInstance(factoryType);
        //    IAGSServerConnection pConnection = pFactory.Open(pPropertySet, 0);

        //    //Get the image server.
        //    IAGSEnumServerObjectName pServerObjectNames = pConnection.ServerObjectNames;
        //    pServerObjectNames.Reset();
        //    IAGSServerObjectName ServerObjectName = pServerObjectNames.Next();
        //    while (ServerObjectName != null)
        //    {
        //        if ((ServerObjectName.Name.ToLower() == pServiceName.ToLower()) &&
        //            (ServerObjectName.Type == "MapServer"))
        //            //if ((ServerObjectName.Name.ToLower() == pServiceName.ToLower()) &&
        //            //   (ServerObjectName.Type == "FeatureServer"))
        //        {

        //            break;
        //        }
        //        ServerObjectName = pServerObjectNames.Next();
        //    }

        //    //返回对象
        //    return ServerObjectName;
        //}

        //private void RetrieveDataFromServer()
        //{
        //    //连接server服务器

        //    IAGSServerConnectionFactory pAGSServerConnFactory = new AGSServerConnectionFactoryClass();
        //    IAGSServerConnection pAGSServerConn = new AGSServerConnectionClass();

        //    IPropertySet pPropSet = new PropertySetClass();

        //    pPropSet.SetProperty("url", "http://localhost:6080/arcgis/services");

        //    pAGSServerConn = pAGSServerConnFactory.Open(pPropSet, 0);

        //    if (pAGSServerConn == null) return;

        //    //获得所有server服务
        //    IAGSEnumServerObjectName pAGSEnumServerObjName = pAGSServerConn.ServerObjectNames;

        //    if (pAGSEnumServerObjName == null) return;

        //    pAGSEnumServerObjName.Reset();

        //    //具体某个server服务
        //    IAGSServerObjectName pAGSServerObjName = new AGSServerObjectNameClass();

        //    pAGSServerObjName = pAGSEnumServerObjName.Next();

        //    IMapServer pMapServer = null;

        //    while (pAGSServerObjName.Name != null)
        //    {
        //        //查找制定名称的server服务AllVector
        //        //if (pAGSServerObjName.Name == "AllVector")
        //        if (pAGSServerObjName.Name == "Temp/ft")
        //        {
        //            IName pName = pAGSServerObjName as IName;

        //            pMapServer = pName.Open() as IMapServer;

        //            break;
        //        }

        //        pAGSEnumServerObjName.Next();

        //    }

        //    if (pMapServer == null) return;

        //    //获取该server服务的所有服务信息
        //    IMapServerInfo pMapServerInfo = new MapServerInfoClass();

        //    pMapServerInfo = pMapServer.GetServerInfo(pMapServer.DefaultMapName);

        //    //获取该server服务下的所有图层信息
        //    IMapLayerInfos pMapLayerInfos = new MapLayerInfosClass();

        //    pMapLayerInfos = pMapServerInfo.MapLayerInfos;

        //    IMapLayerInfo pMapLayerInfo = new MapLayerInfoClass();

        //    for (int i = 0; i < pMapLayerInfos.Count; i++)
        //    {
        //        //定位到具体的一个图层
        //        pMapLayerInfo = pMapLayerInfos.get_Element(i);

        //        //判断当前是否为数据图层
        //        if (pMapLayerInfo.Fields == null) continue;


        //        //后面是查找具体的某个图层的数据

        //        //判断图层中是否存在位站码的字段
        //        int fieldCount = pMapLayerInfo.Fields.FieldCount;

        //        int pIndex = pMapLayerInfo.Fields.FindField("STCD8");

        //        if (pIndex >= 0 || pMapLayerInfo.Name == "墒情站")
        //        {
        //            //查询图层，输入STCD8和Shape字段结果
        //            IQueryFilter filter = new QueryFilterClass();

        //            if (pMapLayerInfo.Name == "墒情站")
        //            {

        //                filter.SubFields = "ENNMCD" + "," + "Shape" + "," + "ENNM";

        //                filter.WhereClause = "ENNMCD" + " <> ''";
        //            }
        //            else
        //            {
        //                filter.SubFields = "STCD8" + "," + "Shape" + "," + "ENNM";

        //                filter.WhereClause = "STCD8" + " <> ''";

        //            }

        //            IRecordSet pRecordSet = pMapServer.QueryFeatureData(pMapServer.DefaultMapName, i, filter);

        //            //存储查询的位站码,站名记录
        //            IFeatureCursor pFeatCursor = pRecordSet.get_Cursor(false) as IFeatureCursor;

        //            //IFeature pFeature = pFeatCursor.NextFeature();


        //            IFeature feature = pFeatCursor.NextFeature();

        //            while (feature != null)
        //            {
                     

        //                if (featureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
        //                {
        //                    IPolyline polyline = (IPolyline)feature.Shape;

        //                    //得到line的点集合
        //                    IPointCollection PointCol = polyline as IPointCollection;

        //                    List<IPoint> line_cur = new List<IPoint>();

        //                    for (int j = 0; j < PointCol.PointCount; j++)
        //                    {
        //                        line_cur.Add(PointCol.get_Point(j));
        //                    }

        //                    Linelist.Add(line_cur);
        //                }//if

        //                //from_id.Add(feature.get_Value(counterpart1.select_index1).ToString());

        //                //to_id.Add(feature.get_Value(counterpart1.select_index).ToString());

        //                feature = pFeatCursor.NextFeature();

        //            }//while



        //            //pSpatialRef = pFeature.Shape.SpatialReference;

        //            //while (pFeature != null)
        //            //{
        //            //    int FieldIDIndex;

        //            //    if (pMapLayerInfo.Name == NAME_SERVER_LAYER)
        //            //        FieldIDIndex = pFeature.Fields.FindField("ENNMCD");
        //            //    else
        //            //        FieldIDIndex = pFeature.Fields.FindField("STCD8");

        //            //    int FieldNameIndex = pFeature.Fields.FindField("ENNM");

        //            //    //添加位站码数据及其站名数据
        //            //    string m_strStationID = pFeature.get_Value(FieldIDIndex).ToString();

        //            //    string m_strStationName = pFeature.get_Value(FieldNameIndex).ToString();

        //            //    if (ContainStringValue(m_StationIDList, m_strStationID) == false)
        //            //    {
        //            //        m_StationIDList.Add(m_strStationID); //8位站码

        //            //        m_StationNameList.Add(m_strStationName); //对应站名

        //            //        m_StationPointList.Add(pFeature.Shape);//站码对应点

        //            //        string tp = m_strStationID + " " + m_strStationName;

        //            //        StationComboBox.Items.Add(tp);
        //            //    }

        //            //    pFeature = pFeatCursor.NextFeature();

        //            //}

        //        }


        //    }
        //}
    
        private void 断层解析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fault_analysis faultanalysis1 = new Fault_analysis();
            faultanalysis1.ShowDialog();

            util.linetoshp(faultanalysis1.Sumlist, faultanalysis1.savepath, "断层走向", "长度", "参数", faultanalysis1.f1, faultanalysis1.f2, faultanalysis1.f3);

            view_list(faultanalysis1.savepath);

        }

        private void 河流线要素转点要素ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ltop lp1 = new ltop();
            lp1.ShowDialog();
         
            Linelist.Clear();

            featureClass = lp1.featureClass;   

            //ponittoshp(Linelist, lp1.savepath);
            //view_list(lp1.savepath);

            //ITin pTin = Get_synturning.CreateTin(featureClass, lp1.path_tin);

            //IFeatureClass pFClass;
            //pFClass = CreateFeaClass(lp1.path_voronoi);

            //Get_synturning.CreateVr(pFClass, pTin);

            //IFeatureLayer flay = new FeatureLayerClass();
            //flay.Name = "泰森多边形";
            //flay.FeatureClass = pFClass;
            //axMapControl1.Map.AddLayer(flay as ILayer);
            //axMapControl1.Refresh();

            Get_synturning.GpCreateVr(lp1.path_voronoi, lp1.path_voronoi);
        }

        private void 获取ARG图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ARG arg1 = new ARG();
            arg1.ShowDialog();

            List<IPoint> arg_point1 = new List<IPoint>();

            Entity.Linelist.Clear();
            Entity.from_id.Clear();
            Entity.to_id.Clear();

            featureClass = arg1.featureClass;

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
                    Entity.Linelist.Add(line_cur);
                }//if

                Entity.from_id.Add(feature.get_Value(arg1.select_index1).ToString());
                Entity.to_id.Add(feature.get_Value(arg1.select_index2).ToString());

                feature = featureCursor.NextFeature();
            }//while

            get_ARG getARG = new get_ARG(Entity.LT, Entity.ST, Entity.Linelist, Entity.from_id, Entity.to_id);

            getARG.CreatARG();

            for (int i = 0; i < getARG.ListArgPoint.Count; i++)
            {
                arg_point1.Add(getARG.ListArgPoint[i].PointL);
                Entity.sstr_curb.Add(getARG.ListArgPoint[i].CurbS);
                Entity.bpoint_id.Add(getARG.ListArgPoint[i].BPointId);
                Entity.sstr_length.Add(getARG.ListArgPoint[i].Length);
            }

            //util.ponittoshp(arg_point1, arg1.savepath_point, "所属河流", "河段起点", "长度", Entity.priver_id, Entity.bpoint_id, Entity.sstr_length);
            util.ponittoshp(arg_point1, arg1.savepath_point, "弯曲度", "河段起点", "长度", Entity.sstr_curb, Entity.bpoint_id, Entity.sstr_length);

            for (int i = 0; i < getARG.ListArgLine.Count; i++)
            {
                Entity.arg_line.Add(getARG.ListArgLine[i].LineL);
                Entity.friver_id.Add(getARG.ListArgLine[i].RID);
                Entity.triver_id.Add(getARG.ListArgLine[i].MidLength);
                Entity.lturn_angle.Add(getARG.ListArgLine[i].MidAngle);
            }
            
            util.linetoshp(Entity.arg_line, arg1.savepath_line, "起点所属河流", "终点所属河流", "转角", Entity.friver_id, Entity.triver_id, Entity.lturn_angle);

            view_list(arg1.savepath_line);
            view_list(arg1.savepath_point);

            GraphStruct dataGraph = getARG.Graph;
            //获取一个模式图
            PatternDefine pDefine = new PatternDefine("D:/1122调试模式匹配/Xml/");
            pDefine.GenerateAll();

            //GraphStruct barPGraph = pDefine.PreadXmlFile("BarPattern.xml");
            //GraphStruct couPGraph = pDefine.PreadXmlFile("CouPattern1.xml");
            GraphStruct rigPGraph = pDefine.PreadXmlFile("RightTurnPattern.xml");    
            //GraphStruct strPGraph = pDefine.PreadXmlFile("StrPattern.xml");
            //GraphStruct capPGraph = pDefine.PreadXmlFile("CapturePattern.xml");

            //模式匹配
            Matching mat = new Matching(dataGraph, rigPGraph);
            mat.StruMatchGraph();

            //将模式图还原为河段对象
            List<SpecRiver> resultRivers = util.Graph2River(mat.subGraphs);

            //存储被识别出的河段对象
            Entity.AttributeName.Add("原河流");
            Entity.AttributeName.Add("长度");

            //util.River2Shp("D:\\1202测试实验结果\\bar0105\\bar.shp", resultRivers, Entity.AttributeName);
            //util.River2Shp("D:\\1202测试实验结果\\con1226\\cou.shp", resultRivers, Entity.AttributeName);
            util.River2Shp("D:\\1202测试实验结果\\rig0106\\rig1.shp", resultRivers, Entity.AttributeName);
            //util.River2Shp("E:\\20寒假远程办公\\专利\\河流袭夺数据\\result\\cap.shp", resultRivers, Entity.AttributeName);
            //util.River2Shp("D:\\0127测试实验结果\\result\\bar\\bar.shp", resultRivers, Entity.AttributeName);

            //view_list("D:\\1202测试实验结果\\result\\bar\\bar.shp");
            //view_list("D:\\1202测试实验结果\\result\\cou\\cou.shp");
            //view_list("D:\\1202测试实验结果\\result\\rig\\rig.shp");
            //view_list("D:\\河流袭夺\\沂蒙山result\\cap.shp");
            //view_list("D:\\1202测试实验结果\\专利\\bar.shp");

            int jd = 0;
        }

        private void 基于ARG识别河流ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PatternDefine pDefine = new PatternDefine("D:/1122调试模式匹配/Xml/");

            ////生成所有模式的xml文件
            //pDefine.GenerateAll();

            this.Dispose();

            //Extract extract1 = new Extract();
            //extract1.ShowDialog();

            //util.clear_all();

            //featureClass = extract1.featureClass_point;

            //IFeatureCursor featureCursor = featureClass.Search(null, false);
            //IFeature feature = featureCursor.NextFeature();

            //while (feature != null)
            //{
            //    if (featureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
            //    {
            //        IPolyline polyline = (IPolyline)feature.Shape;

            //        //得到line的点集合
            //        IPointCollection PointCol = polyline as IPointCollection;

            //        List<IPoint> line_cur = new List<IPoint>();

            //        for (int i = 0; i < PointCol.PointCount; i++)
            //        {
            //            line_cur.Add(PointCol.get_Point(i));
            //        }
            //        Entity.Linelist.Add(line_cur);
            //    }//if
            //    //int id = featureClass.FindField("InDgree");

            //    ////xx = featureClass.GetFeature(i).Value[id];
                
            //    //Entity.to_id.Add(feature.get_Value(id).ToString());

            //    feature = featureCursor.NextFeature();
            //}//while


            //extract_river.extract(Entity.Linelist, Entity.arg_point, Entity.arg_line, Entity.friver_id, Entity.triver_id, Entity.lturn_angle, 20, Entity.LT, Entity.ST);

            //util.linetoshp(Entity.straight_line, extract1.savepath_str, "直线近似度", "河段长度", "所属河流", Entity.curb_S, Entity.strlength, Entity.str_yuan_id);
            //util.linetoshp(Entity.Rigntangle_list, extract1.savepath_right, "转角", "河段长度", "所属河流", Entity.turn_angle, Entity.tur_length, Entity.tur_yuan_id);
            //util.linetoshp(Entity.Barb_riverline_list, extract1.savepath_barb, "汇入角", "河流长度", "所属河流", Entity.inter_angle, Entity.bar_length, Entity.bar_yuan_id);
            //util.linetoshp(Entity.Counterpart_list, extract1.savepath_cou, "交汇角", "河流长度", "所属河流", Entity.Coun_turn_angle, Entity.cou_length, Entity.cou_yuan_id);

            //view_list(extract1.savepath_str);
            //view_list(extract1.savepath_right);
            //view_list(extract1.savepath_barb);
            //view_list(extract1.savepath_cou);
        }

        private void 提取同步转弯ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        ////将IFeatureClass转化为shp文件
        //public void ExportFeature(IFeatureClass  pInFeatureClass, string pPath)
        //{
        //    //判断生成的文件是否已存在，如果存在，则删掉已存在的文件；
        //    string inSHPpath = pPath;
        //    string shpDirName = System.IO.Path.GetDirectoryName(inSHPpath);
        //    string shpName1 = System.IO.Path.GetFileNameWithoutExtension(inSHPpath);
        //    string shpFullName = shpName1 + ".shp";
        //    string prjName = shpName1 + ".prj";
        //    string dbfName = shpName1 + ".dbf";
        //    string shxName = shpName1 + ".shx";
        //    string sbnName = shpName1 + ".sbn";
        //    string xmlName = shpName1 + ".shp.xml";
        //    string sbxName = shpName1 + ".sbx";
        //    if (System.IO.File.Exists(shpDirName + "\\" + shpFullName))
        //        System.IO.File.Delete(shpDirName + "\\" + shpFullName);
        //    if (System.IO.File.Exists(shpDirName + "\\" + prjName))
        //        System.IO.File.Delete(shpDirName + "\\" + prjName);
        //    if (System.IO.File.Exists(shpDirName + "\\" + dbfName))
        //        System.IO.File.Delete(shpDirName + "\\" + dbfName);
        //    if (System.IO.File.Exists(shpDirName + "\\" + shxName))
        //        System.IO.File.Delete(shpDirName + "\\" + shxName);
        //    if (System.IO.File.Exists(shpDirName + "\\" + sbnName))
        //        System.IO.File.Delete(shpDirName + "\\" + sbnName);
        //    if (System.IO.File.Exists(shpDirName + "\\" + xmlName))
        //        System.IO.File.Delete(shpDirName + "\\" + xmlName);
        //    if (System.IO.File.Exists(shpDirName + "\\" + sbxName))
        //        System.IO.File.Delete(shpDirName + "\\" + sbxName);

        //    IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
        //    string parentPath = pPath.Substring(0, pPath.LastIndexOf('\\'));
        //    string fileName = pPath.Substring(pPath.LastIndexOf('\\') + 1, pPath.Length - pPath.LastIndexOf('\\') - 1);
        //    IWorkspaceName pWorkspaceName = pWorkspaceFactory.Create(parentPath, fileName, null, 0);
        //    // Cast for IName        
        //    IName name = (IName)pWorkspaceName;
        //    //Open a reference to the access workspace through the name object        
        //    IWorkspace pOutWorkspace = (IWorkspace)name.Open();


        //    IDataset pInDataset = pInFeatureClass as IDataset;
        //    IFeatureClassName pInFCName = pInDataset.FullName as IFeatureClassName;
        //    IWorkspace pInWorkspace = pInDataset.Workspace;
        //    IDataset pOutDataset = pOutWorkspace as IDataset;
        //    IWorkspaceName pOutWorkspaceName = pOutDataset.FullName as IWorkspaceName;
        //    IFeatureClassName pOutFCName = new FeatureClassNameClass();
        //    IDatasetName pDatasetName = pOutFCName as IDatasetName;
        //    pDatasetName.WorkspaceName = pOutWorkspaceName;
        //    pDatasetName.Name = pInFeatureClass.AliasName;
        //    IFieldChecker pFieldChecker = new FieldCheckerClass();
        //    pFieldChecker.InputWorkspace = pInWorkspace;
        //    pFieldChecker.ValidateWorkspace = pOutWorkspace;
        //    IFields pFields = pInFeatureClass.Fields;
        //    IFields pOutFields;
        //    IEnumFieldError pEnumFieldError;
        //    pFieldChecker.Validate(pFields, out pEnumFieldError, out pOutFields);
        //    IFeatureDataConverter pFeatureDataConverter = new FeatureDataConverterClass();
        //    pFeatureDataConverter.ConvertFeatureClass(pInFCName, null, null, pOutFCName, null, pOutFields, "", 100, 0);

        //}

        //public void CreatVoronoi(IFeatureClass pointFeaCls, IFeatureClass voronoiFeaCls, IPolygon clippingPolygon)
        //{
        //    if (pointFeaCls.ShapeType == esriGeometryType.esriGeometryPoint)
        //    {
        //        if (pointFeaCls.FeatureCount(null) > 2)
        //        {
                    
        //            IEnvelope pEnv = clippingPolygon.Envelope;// (IEnvelope)fc.getExtent();

        //            //pEnv.setSpatialReferenceByRef(clippingPolygon.SpatialReference);

        //            ITinEdit pTinEdit = new TinClass();

        //            pTinEdit.InitNew(pEnv);

        //            pTinEdit.AddFromFeatureClass(pointFeaCls, null, pointFeaCls.Fields.get_Field(0), null, esriTinSurfaceType.esriTinMassPoint, null);
        //            //void AddFromFeatureClass(IFeatureClass, IQueryFilter , IField , IField , esriTinSurfaceType Type, ref object pbUseShapeZ = Type.Missing);

        //            pTinEdit.Refresh();

        //            ITinNodeCollection pTinNodeCollection = (ITinNodeCollection)pTinEdit;

        //            pTinNodeCollection.ConvertToVoronoiRegions(voronoiFeaCls, null, clippingPolygon, "", "");

        //        }
        //    }
        //}


        //public ITin CreateTin(IFeatureClass pFeatureClass, IField pField, string pPath)
        //{
        //    IGeoDataset pGeoDataset = pFeatureClass as IGeoDataset;
        //    ITinEdit pTinEdit = new TinClass();
        //    pTinEdit.InitNew(pGeoDataset.Extent);
        //    object pObj = Type.Missing;

        //    pTinEdit.AddFromFeatureClass(pFeatureClass, null, pField, null, esriTinSurfaceType.esriTinMassPoint, ref pObj);

        //    pTinEdit.SaveAs(pPath, ref pObj);
        //    pTinEdit.Refresh();

        //    return pTinEdit as ITin;
        //}

        ////创建泰森多边形
        //void CreateVr(IFeatureClass pFeatureClass,ITin pTin)
        //{
        //    ITinNodeCollection pTinColl = pTin as ITinNodeCollection;

        //    pTinColl.ConvertToVoronoiRegions(pFeatureClass,null,null,"","");
        //}

    }
}
