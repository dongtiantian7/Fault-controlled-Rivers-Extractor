using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;

namespace test
{
    class util
    {
        //相邻关系分类
        public static int adjacent(List<IPoint> l1, List<IPoint> l2)
        {
            int n = 1;

            double mindis = util.getDistanceOfTwoPoints(l1[0], l2[0]);

            int count1 = l1.Count(), count2 = l2.Count();

            //double dis1 = util.getDistanceOfTwoPoints(l1[0], l2[0]);
            double dis2 = util.getDistanceOfTwoPoints(l1[0], l2[count2 - 1]);
            double dis3 = util.getDistanceOfTwoPoints(l1[count1 - 1], l2[0]);
            double dis4 = util.getDistanceOfTwoPoints(l1[count1 - 1], l2[count2 - 1]);

            //if (mindis < util.getDistanceOfTwoPoints(l1[0], l2[0]))
            //{
            //    mindis = util.getDistanceOfTwoPoints(l1[0], l2[0]);
            //    n = 1;
            //}

            if (mindis > dis2)
            {
                mindis = dis2;
                n = 2;
            }
            if (mindis > dis3)
            {
                mindis = dis3;
                n = 3;
            }
            if (mindis > dis4)
            {
                mindis = dis4; 
                n = 4;
            }
            return n;
        }

        ///// <summary>
        ///// 生成从id序号的点开始的直线河段，
        ///// </summary>
        ///// <param name="Line"></param>
        ///// <param name="id">直线河段的起点</param>
        ///// <param name="ST"></param>
        ///// <param name="dir">提取直线河段的方向</param>
        ///// <returns>返回直线河段的终点</returns>
        //public static int minstra(List<IPoint> Line, int bid, double ST, int dir)
        //{
        //    int did = bid, i, maxid = bid;
        //    double S = 0, sum_dis = 0, maxlen = 0;

        //    if (dir > 0)
        //    {
        //        for (i = bid; i < Line.Count() - 1; i++)
        //        {
        //            sum_dis += util.getDistanceOfTwoPoints(Line[i], Line[i + 1]);

        //            S = sum_dis / util.getDistanceOfTwoPoints(Line[bid], Line[i + 1]);

        //            if (S < ST)
        //            {
        //                if (sum_dis > maxlen)
        //                { 
        //                    maxlen = sum_dis;
        //                    maxid = i + 1;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (i = bid; i > 0; i--)
        //        {
        //            sum_dis += util.getDistanceOfTwoPoints(Line[i], Line[i - 1]);

        //            S = sum_dis / util.getDistanceOfTwoPoints(Line[bid], Line[i - 1]);

        //            if (S < ST)
        //            {
        //                if (sum_dis > maxlen)
        //                {
        //                    maxlen = sum_dis;
        //                    maxid = i - 1;
        //                }
        //            }
        //        }//for
        //    }

        //    return maxid;
        //}

        //得到指定河流中指定河流段的长度
        public static double sumdis_line(List<IPoint> Line, int bid, int eid)
        {
            double sum = 0;

            if (bid > eid)
            {
                int t = bid;
                bid = eid;
                eid = t;
            }

            for (int i = bid; i < eid; i++)
                sum += getDistanceOfTwoPoints(Line[i], Line[i + 1]);

            return sum;
        }

        

        //计算两向量 的夹角
        public static double getangle(IPoint A, IPoint B)
        {
            double L1_n = Math.Sqrt(Math.Pow(A.X, 2) + Math.Pow(A.Y, 2));

            double L2_n = Math.Sqrt(Math.Pow(B.X, 2) + Math.Pow(B.Y, 2));

            double ang = Math.Acos((A.X * B.X + A.Y * B.Y) / (L1_n * L2_n)) / Math.PI * 180;

            return ang;
        }

        //计算两点间的距离
        public static double getDistanceOfTwoPoints(ESRI.ArcGIS.Geometry.IPoint point1, ESRI.ArcGIS.Geometry.IPoint point2)
        {
            return Math.Sqrt((point1.X - point2.X) * (point1.X - point2.X) + (point1.Y - point2.Y) * (point1.Y - point2.Y));
        }

        public static string linetoshp(List<List<IPoint>> line, string name, string fn1, string fn2, string fn3, List<double> temp1, List<double> temp2, List<double> temp3)
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

        //把点序列存储为点图层
        public static string ponittoshp(List<IPoint> Listpoint, string name, string fn1, string fn2, string fn3, List<double> temp1, List<double> temp2, List<double> temp3)
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

            ISpatialReference spatialReference = new UnknownCoordinateSystemClass();     
            //ISpatialReferenceFactory pSpatialRefFac = new SpatialReferenceEnvironmentClass();
            //IProjectedCoordinateSystem spatialReference = pSpatialRefFac.CreateProjectedCoordinateSystem((int)esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_39);

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


            ////添加字段1记录点所在的河流序号；
            //IField nameField = new FieldClass();
            //IFieldEdit nameFieldEdit = (IFieldEdit)nameField;
            //nameFieldEdit.Name_2 = "所属河流";
            //nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
            //nameFieldEdit.Length_2 = 20;
            //fieldsEdit.AddField(nameField);

            IField nameField = new FieldClass();
            IFieldEdit nameFieldEdit = (IFieldEdit)nameField;

            //添加字段1；
            nameField = new FieldClass();
            nameFieldEdit = (IFieldEdit)nameField;
            nameFieldEdit.Name_2 = fn1;
            //nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
            nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            nameFieldEdit.Length_2 = 20;
            fieldsEdit.AddField(nameField);

            //添加字段2；
            nameField = new FieldClass();
            nameFieldEdit = (IFieldEdit)nameField;
            nameFieldEdit.Name_2 = fn2;
            nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            nameFieldEdit.Length_2 = 20;
            fieldsEdit.AddField(nameField);

            //添加字段3；
            nameField = new FieldClass();
            nameFieldEdit = (IFieldEdit)nameField;
            nameFieldEdit.Name_2 = fn3;
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

            //记录点的所属河流
            for (int i = 0; i < Listpoint.Count; i++)
            {
                IPoint pl = new PointClass();
                pl.X = Listpoint[i].X;
                pl.Y = Listpoint[i].Y;

                IFeature feature = pNewFeaCls.CreateFeature();
                feature.Shape = pl;
                feature.set_Value(2, temp1[i]);
                feature.set_Value(3, temp2[i]);
                feature.set_Value(4, temp3[i]);

                feature.Store();
            }

            return inSHPpath;
        }

        /// <summary>
        /// 将河流对象存储为线图层
        /// </summary>
        /// <param name="filePath">线图层的绝对路径</param>
        /// <param name="Rivers">河流数据</param>
        /// <param name="AttributeName">河流图层的属性名称</param>
        public static void River2Shp(string filePath, List<SpecRiver> Rivers, List<string> AttributeName)
        {
            //判断生成的文件是否已存在，如果存在，则删掉已存在的文件；
            string inSHPpath = filePath;
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


            //设置生成图的空间坐标参考系统；
            IGeometryDef geometryDef = new GeometryDefClass();
            IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
            geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;

            ISpatialReference spatialReference = new UnknownCoordinateSystemClass();
            ISpatialReferenceResolution spatialReferenceResolution = (ISpatialReferenceResolution)spatialReference;
            spatialReferenceResolution.ConstructFromHorizon();
            ISpatialReferenceTolerance spatialReferenceTolerance = (ISpatialReferenceTolerance)spatialReference;
            spatialReferenceTolerance.SetDefaultXYTolerance();
            geometryDefEdit.SpatialReference_2 = spatialReference;

            //开始添加属性字段；
            IFields fields = new FieldsClass();
            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;

            //添加字段“OID”；
            IField oidField = new FieldClass();
            IFieldEdit oidFieldEdit = (IFieldEdit)oidField;
            oidFieldEdit.Name_2 = "OID";
            oidFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            fieldsEdit.AddField(oidField);

            //添加字段“Shape”;
            IField geometryField = new FieldClass();
            IFieldEdit geometryFieldEdit = (IFieldEdit)geometryField;
            geometryFieldEdit.Name_2 = "Shape";
            geometryFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            geometryFieldEdit.GeometryDef_2 = geometryDef;
            fieldsEdit.AddField(geometryField);

            //添加属性字段
            for (int i = 0; i < AttributeName.Count; i++)
            {
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = AttributeName[i];
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                //pFieldEdit.Length_2 = 20;
                fieldsEdit.AddField(pField);
            }

            IFieldChecker fieldChecker = new FieldCheckerClass();
            IEnumFieldError enumFieldError = null;
            IFields validatedFields = null;
            fieldChecker.ValidateWorkspace = (IWorkspace)pFWS;
            fieldChecker.Validate(fields, out enumFieldError, out validatedFields);

            //在工作空间中生成FeatureClass;
            IFeatureClass pNewFeaCls = pFWS.CreateFeatureClass(shpName, validatedFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");

            for (int i = 0; i < Rivers.Count(); i++)
            {
                SpecRiver tempRiverObj = Rivers[i];

                //当前线中的折点个数
                int pointcount = tempRiverObj.RiverLine.Count();

                IPolyline pl = new PolylineClass();
                IPointCollection ptc = pl as IPointCollection;

                object missing = Type.Missing;

                for (int j = 0; j < tempRiverObj.RiverLine.Count(); j++)
                {
                    IPoint cpoint = tempRiverObj.RiverLine[j];
                    ptc.AddPoint(cpoint, missing, missing);
                }

                (pl as ITopologicalOperator).Simplify();

                IFeature feature = pNewFeaCls.CreateFeature();
                feature.Shape = pl;

                for (int j = 0; j < tempRiverObj.Attributes.Count(); j++)
                {
                    feature.set_Value(j + 2 , tempRiverObj.Attributes[j]);
                }

                feature.Store();
            }//for
            
        }

        ///// <summary>
        ///// 在某完整河段中生成指定起点的直角转弯河段
        ///// </summary>
        ///// <param name="river"></param>
        ///// <param name="bid">第一个角度边的起点</param>
        ///// <param name="bid2">第二个角度边的起点</param>
        ///// <param name="ST"></param>
        //public static int cut_rightpart(List<IPoint> river, int L1B, int L2B, double ST)
        //{
        //    List<IPoint> rig_temp = new List<IPoint>();

        //    int L1E = util.minstra(river, L1B, ST, 1);
        //    int L2E = util.minstra(river, L2B, ST, 1);

        //    for (int i = L1B; i < L2E; i++)
        //        rig_temp.Add(river[i]);
              
        //    Entity.Rigntangle_list.Add(rig_temp);
        //    Entity.tur_length.Add(sumdis_line(river, L1B, L2E));

        //    return 1;
        //    //IPoint L1Bp, L1Ep, L2Bp, L2Ep;
        //    ////int L1B, L1E, L2B, L2E;
        //    //double L1_dis, L2_dis, mid_dis;

        //    //L1B = L1E = L2B = L2E = 0;
        //    //L1_dis = L2_dis = mid_dis = 0;

        //    //while (L1E < river.Count() - 1)
        //    //{
        //    //    while ((L1_dis < LT) && (L1E < river.Count() - 1))
        //    //    {
        //    //        L1B = L1E;

        //    //        L1E = util.minstra(river, L1B, ST, 1);

        //    //        L1_dis = util.sumdis_line(river, L1B, L1E);
        //    //    }

        //    //    //如果整条河流都是直线 或 找不到长度符合条件的L1 或 不能找到L2
        //    //    if (L1E >= river.Count() - 1)
        //    //        break;

        //    //    L2E = L1E;

        //    //    while ((L2_dis < LT) && (L2B < river.Count() - 1))
        //    //    {
        //    //        L2B = L2E;

        //    //        L2E = util.minstra(river, L2B, ST, 1);

        //    //        L2_dis = util.sumdis_line(river, L2B, L2E);
        //    //    }

        //    //    //如果找不到符合条件的L2
        //    //    if (L2B >= river.Count() - 1)
        //    //        break;

        //    //    mid_dis = util.sumdis_line(river, L1E, L2B);

        //    //    //如果L1和L2没有相距过远
        //    //    if (mid_dis < MD)
        //    //    {
        //    //        L1Bp = river[L1B];
        //    //        L1Ep = river[L1E];
        //    //        L2Bp = river[L2B];
        //    //        L2Ep = river[L2E];

        //    //        double ang = util.getangle(L1Bp, L1Ep, L2Bp, L2Ep);

        //    //        if (Math.Abs(ang - 90) < AT)
        //    //        {
        //    //            //List<IPoint> Linetemp1 = new List<IPoint>();
        //    //            List<IPoint> Linetempsum = new List<IPoint>();
        //    //            //double sum_dis = 0;

        //    //            for (int j = L1B; j <= L1E; j++)
        //    //            {
        //    //                Linetempsum.Add(river[j]);
        //    //            }

        //    //            for (int j = L1E; j <= L2B; j++)
        //    //            {
        //    //                Linetempsum.Add(river[j]);
        //    //            }

                       
        //    //            for (int j = L2B; j <= L2E; j++)
        //    //            {
        //    //                Linetempsum.Add(river[j]);
        //    //            }

        //    //            //Linetemp1.Add(Linetempsum);

        //    //            Entity.Rigntangle_list.Add(Linetempsum);
        //    //            Entity.turn_angle.Add(ang);
        //    //            Entity.tur_length.Add(util.sumdis_line(river,L1B, L2E));
        //    //            Entity.tur_yuan_id.Add(id);
        //    //        }//if (Math.Abs(ang - 90) < AT)

        //    //    }//if (mid_dis < max_LT)

        //    //    L1B = L2B; L1E = L2E;

        //    //    L1_dis = L2_dis; L2_dis = mid_dis = 0;

        //    //}//while

        //    //return Linetemp1;
        //}


        public static void clear_all()
        {
            //Entity.Linelist.Clear();

            //Entity.arg_point.Clear();
            //Entity.arg_line.Clear();
            //Entity.to_id.Clear();
            //Entity.friver_id.Clear();

            //Entity.arg_line.Clear();
            //Entity.triver_id.Clear();
            //Entity.lturn_angle.Clear();
            //Entity.lturn_angle.Clear();
        }

        //将匹配出的子图 还原成河流线数据
        public static List<SpecRiver> Graph2River( List<GraphStruct> subGraphs)
        { 
            List<SpecRiver> resultRivers = new List<SpecRiver>();

            //把模式图还原成河流线数据
            for (int i = 0; i < subGraphs.Count;i++ )
            {
                GraphStruct tempGraph = subGraphs[i];

                for (int j = 0; j < tempGraph.VexNum; j++)
                {
                    if (tempGraph.VexList[j].Data.IsStore)
                    {
                        SpecRiver tempRiver = new SpecRiver();

                        ArgPoint nodeData = tempGraph.VexList[j].Data;

                        //倒钩河还原
                        //if (Entity.source_river[nodeData.RiverID].Count > 0)
                        //    break;
                        //tempRiver.RiverLine = Entity.Linelist[nodeData.RiverID];
                        //直角转弯河段还原
                        tempRiver.RiverLine = PartLine(Entity.Linelist[nodeData.RiverID], nodeData.BPointId, nodeData.DPointId);
                        tempRiver.Attributes.Add(nodeData.RiverID.ToString());
                        tempRiver.Attributes.Add(nodeData.Length.ToString());

                        resultRivers.Add(tempRiver);

                        //将边上的过渡河段还原
                        AdjNode aNode = tempGraph.VexList[j].FirstAdj;
                        while (aNode != null)
                        {
                            if ((aNode.EdgeValue.MidLength != 0) && (tempGraph.VexList[aNode.AdjVexId].Data.IsStore))
                            {
                                SpecRiver tempMidRiver = new SpecRiver();
                                tempMidRiver.RiverLine = aNode.EdgeValue.MidLine;
                                tempMidRiver.Attributes.Add(nodeData.RiverID.ToString());
                                tempMidRiver.Attributes.Add(aNode.EdgeValue.MidLength.ToString());

                                resultRivers.Add(tempMidRiver);
                            }

                            aNode = aNode.Next;
                        }


                    }//if (tempGraph.VexList[j].Data.IsStore)

                }//for
     
            }

            return resultRivers;
        }

        //截取河流中指定起点和终点的河段
        public static List<IPoint> PartLine(List<IPoint> river, int bid, int did)
        {
            List<IPoint> str_temp = new List<IPoint>();

            for (int i = bid; i <= did; i++)
            {
                str_temp.Add(river[i]);
            }

            return str_temp;
        }
    }
}
