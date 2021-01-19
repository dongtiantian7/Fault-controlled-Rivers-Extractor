using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;
using System.IO;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Carto;

using ESRI.ArcGIS.AnalysisTools;         //添加引用
using ESRI.ArcGIS.Geoprocessor;


using System.Data;
using System.Drawing;


using System.Windows.Forms;
using ESRI.ArcGIS.DataManagementTools;



namespace test
{
    class Get_synturning
    {
        public static List<List<IPoint>> lturn = new List<List<IPoint>>();

        public static List<List<IPoint>> rturn = new List<List<IPoint>>();

        ////获取同步转弯
        //public static List<List<IPoint>> getsynturning(List<List<IPoint>> Linelist, double ST, double LT, double MD, double AT)
        //{
        //   List<List<IPoint>> synturning_list = new List<List<IPoint>>();
           
        //   Form1.yuan_id.Clear();
        //   Form1.length.Clear();
           
        //   //线的数目
        //   int num_line = Linelist.Count();

        //   for (int i = 0; i < num_line; i++)
        //   {
        //       getturn(Linelist[i], ST, LT);
        //   }
           
           
        //   return synturning_list;

        //}

       /// <summary>
       /// 获取TIN
       /// </summary>
       /// <param name="pFeatureClass">点图层</param>
       /// <param name="TinName">TIN的存储路径</param>
       /// <returns></returns>
       public static ITin CreateTin(IFeatureClass pFeatureClass, string TinName)
       {
           //TinName是路径
           IField pField = pFeatureClass.Fields.get_Field(2);

           IGeoDataset pGeoDataset = pFeatureClass as IGeoDataset;
           ITinEdit pTinEdit = new Tin() as ITinEdit;
           pTinEdit.InitNew(pGeoDataset.Extent);
           object pObj = Type.Missing;

           pTinEdit.AddFromFeatureClass(pFeatureClass, null, pField, null, esriTinSurfaceType.esriTinMassPoint, ref pObj);
           if (Directory.Exists(TinName))
           {
               Directory.Delete(TinName, true);
           }
           pTinEdit.SaveAs(TinName, ref pObj);
           pTinEdit.Refresh();

           return pTinEdit as ITin;
       }
      
        /// <summary>
        /// 用TIN创建泰森多边形
        /// </summary>
        /// <param name="pFeatureClass">空的面图层，用来存放生成的V图</param>
        /// <param name="pTin"></param>
       public static void CreateVr(IFeatureClass pFeatureClass, ITin pTin)
       { 
           ITinNodeCollection pTinColl = pTin as ITinNodeCollection;

           pTinColl.ConvertToVoronoiRegions(pFeatureClass, null, null, "", "");
       }

       //调用GP工具创建泰森多边形
       public static void GpCreateVr(string pointName, string ThiessenName)
       {
           Geoprocessor pGeop = new Geoprocessor();
           pGeop.OverwriteOutput = true;
           ESRI.ArcGIS.AnalysisTools.CreateThiessenPolygons pThiessen = new ESRI.ArcGIS.AnalysisTools.CreateThiessenPolygons();
           pThiessen.in_features = pointName;
           pThiessen.out_feature_class = ThiessenName;
           pThiessen.fields_to_copy = "ONLY_FID";
           pGeop.Execute(pThiessen, null);
       }

        /// <summary>
        /// 面转线（GP法）（获取相邻面）需要最高ArcEngine权限
        /// </summary>
        /// <param name="IN_Featureclass">要转换的要素类</param>
        /// <param name="IN_SaveNeighborInfor">是否保存相邻区域信息</param>
        /// <param name="IN_LineName">转换后的要素名</param>
        /// <returns>转换后的要素</returns>
        private IFeatureClass PRV_PolyGonToLine_GP(IFeatureClass IN_Featureclass,bool IN_SaveNeighborInfor,string IN_LineName)
        {
            //要素数据集路径
            string Temp_FeaturedatasetPath;
            //要素数据集
            IFeatureDataset Temp_FeatureDataset = IN_Featureclass.FeatureDataset;
            if (Temp_FeatureDataset == null)
                Temp_FeatureDataset = IN_Featureclass as IFeatureDataset;
            Temp_FeaturedatasetPath = Temp_FeatureDataset.Workspace.PathName + "\\" + Temp_FeatureDataset.Name + "\\";
            //面转线
            Geoprocessor GP_Tool = new Geoprocessor();//GP运行工具
            ESRI.ArcGIS.DataManagementTools.PolygonToLine GP_PolyGonToline = new PolygonToLine();
            GP_PolyGonToline.in_features = Temp_FeaturedatasetPath + IN_Featureclass.AliasName;
            GP_PolyGonToline.neighbor_option = IN_SaveNeighborInfor.ToString().ToLower();
            GP_PolyGonToline.out_feature_class = Temp_FeaturedatasetPath + IN_LineName;
            GP_Tool.Execute(GP_PolyGonToline, null);
            //获取生成的要素类
            IFeatureClass Temp_FeatureClass = (Temp_FeatureDataset.Workspace as IFeatureWorkspace).OpenFeatureClass(IN_LineName);
            return Temp_FeatureClass;
        }


       //得到河流邻接表
       public static void adjturn(IFeatureClass pFeatureClass)
       {
           List<LinkedList<int>> adj = new List<LinkedList<int>>();

           
       }

        /// <summary>
        /// 判断给定的三个河流转弯是否为同步转弯
        /// </summary>
        /// <param name="turning1"></param>
        /// <param name="turning2"></param>
        /// <param name="turning3"></param>
        /// <param name="AT">向量夹角的阈值</param>
        /// <returns>返回判断结果</returns>  
       //public static bool synturn(List<IPoint> turning1, List<IPoint> turning2, List<IPoint> turning3, double AT)
       //{
       //    IPoint p1 = turning1[0];
       //    IPoint p2 = turning2[0];
       //    IPoint p3 = turning3[0];

       //    double da = util.getangle(p1, p2, p1, p3);

       //    if ((da <= AT) || Math.Abs(da - 180) <= AT)
       //    {
       //        return true;
       //    }
       //    else
       //        return false;
       //}

       /// <summary>
       /// 获取单条河流的急转弯河段
       /// </summary>
       /// <param name="river"></param>
       /// <param name="ST">提取直线所需参数</param>
       /// <param name="LT">提取直线所需参数</param>
       //public static void getturn(List<IPoint> river, double ST, double LT)
       //{
       //    IPoint T1Bp, T1Ep, T2Bp, T2Ep;

       //    int T1B, T1E, T2B, T2E;

       //    int L1B, L1E, L2B, L2E;

       //    double L1_dis, L2_dis;

       //    L1B = L1E = L2B = L2E = 0;

       //    L1_dis = L2_dis = 0;

       //    while (L1E < river.Count() - 1)
       //    {
       //        while ((L1_dis < LT) && (L1E < river.Count() - 1))
       //        {
       //            L1B = L1E;

       //            L1E = util.minstra(river, L1B, ST,1);

       //            L1_dis = util.getDistanceOfTwoPoints(river[L1B], river[L1E]);
       //        }

       //        //如果整条河流都是直线 或 找不到长度符合条件的L1 或 不能找到L2
       //        if (L1E >= river.Count() - 1)
       //            break;

       //        L2E = L1E;

       //        while ((L2_dis < LT) && (L2B < river.Count() - 1))
       //        {
       //            L2B = L2E;

       //            L2E = util.minstra(river, L2B, ST,1);

       //            L2_dis = util.getDistanceOfTwoPoints(river[L2B], river[L2E]);
       //        }

       //        //如果找不到符合条件的L2
       //        if (L2B >= river.Count() - 1)
       //            break;

       //        T1B = L1E; T1E = util.minstra(river, T1B, ST, 1);

       //        T2E = L2B; T2B = util.minstra(river, T2E, ST, -1);

       //        T1Bp = river[T1B]; T1Ep = river[T1E];

       //        T2Bp = river[T2B]; T2Ep = river[T2E];

       //        double tang = util.getangle(T1Bp, T1Ep, T2Bp, T2Ep);

       //        if (tang > 90)
       //        {
       //            List<IPoint> t = new List<IPoint>();

       //            for (int i = L1E; i <= L2B; i++)
       //            {
       //                t.Add(river[i]);
       //            }

       //            if (judge_turndir(t) > 0)
       //                lturn.Add(t);
       //            else
       //                rturn.Add(t);

       //        }//if(tang > 90)   
       //    }// while (L1E < river.Count() - 1)
       //}


        /// <summary>
        /// 判断转弯河段的转弯方向
        /// </summary>
        /// <param name="turning">转弯河段</param>
        /// <returns></returns>
       public static int judge_turndir(List<IPoint> turning)
       {
           int n = turning.Count();
           int m = n / 2;

           IPoint p1 = turning[0];
           IPoint p2 = turning[n - 1];
           IPoint p = turning[m];

           double tmpx = (p1.X - p2.X) / (p1.Y - p2.Y) * (p.Y - p2.Y) + p2.X;

           if (tmpx > p.X)//当tmpx>p.x的时候，说明点在线的左边，小于在右边，等于则在线上
               return 1;

           return -1;
       }

        //计算转弯河段与理想直线之间围成的面积，衡量转弯河段的规模
       public static int judge_turnscale(List<IPoint> river, int b, int d )
       {
           int scale = 0;

           double area = 0;

           for (int i = b; i < d; i++)
           {
               area += ((river[i].Y - river[d].Y) + (river[i + 1].Y - river[d].Y)) * (river[i].X - river[i + 1].X) / 2;
           }

           area = Math.Abs( area - (river[b].Y - river[d].Y) * (river[b].X - river[d].X) / 2 );

           return scale;
       }
        
    //   public static int minstra_t(List<IPoint> Line, int id, double ST,int d)
    //   {
    //       int n = id, i = id;
    //       double S = 0;
    //       double sum_dis = 0;

    //       if (d > 0)
    //       {
    //           for (i = id; i < Line.Count() - 1; i++)
    //           {
    //               sum_dis += Form1.getDistanceOfTwoPoints(Line[i], Line[i + 1]);

    //               S = sum_dis / Form1.getDistanceOfTwoPoints(Line[id], Line[i + 1]);

    //               if (S >= ST) break;
    //           }
    //       }
    //       else
    //       {
    //           for (i = id; i > 0; i--)
    //           {
    //               sum_dis += Form1.getDistanceOfTwoPoints(Line[i], Line[i - 1]);

    //               S = sum_dis / Form1.getDistanceOfTwoPoints(Line[id], Line[i - 1]);

    //               if (S >= ST) break;
    //           }
    //       }
         
    //       n = i;

    //       return n;
    //   }
  
    }
}
