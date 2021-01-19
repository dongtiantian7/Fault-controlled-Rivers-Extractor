using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;




namespace test
{
    class pointz
    {
        //将DEM转成MapControl可以打开的格式
        public static ILayer openDEMLayer(string fullPath)
        {
            string pathToWorkspace = System.IO.Path.GetDirectoryName(fullPath);
            string demName = System.IO.Path.GetFileName(fullPath);
            IWorkspaceFactory pWSFact = new RasterWorkspaceFactoryClass();
            IWorkspace pWS = pWSFact.OpenFromFile(pathToWorkspace, 0);
            IRasterWorkspace pRasterWorkspace = pWS as IRasterWorkspace;
            IRasterLayer pRasterLayer = new RasterLayerClass();
            try
            {
                IRasterDataset pRasterDataset = (IRasterDataset)pRasterWorkspace.OpenRasterDataset(demName);
                pRasterLayer.CreateFromDataset(pRasterDataset);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return pRasterLayer;
        }

        /// 获取x,y,高程值
        /// </summary>
        /// <param name="raster">DEM</param>
        /// <param name="point">指定点</param>
        /// <param name="ptX">点的横坐标</param>
        /// <param name="ptY"></param>
        /// <param name="ptHeight">高程</param>
        void getXYAndHeight(IRaster raster, IPoint point, out double ptX, out double ptY, out double ptHeight)
        {
            ptX = 0.0;
            ptY = 0.0;
            ptHeight = 0.0;
            try
            {
                IGeoDataset geoDt = raster as IGeoDataset;
                ISpatialReference spatialreference = geoDt.SpatialReference;
                IRasterSurface rasterSurface = new RasterSurfaceClass();
                rasterSurface.PutRaster(raster, 0);
                ISurface surface = rasterSurface as ISurface;
                if (point.SpatialReference == null)
                {
                    point.Project(spatialreference);
                    ptX = point.X;
                    ptY = point.Y;
                    //获取高程
                    ptHeight = surface.GetElevation(point);
                }
                else
                {
                    ptX = point.X;
                    ptY = point.Y;
                    //获取高程
                    point.Project(spatialreference);
                    ptHeight = surface.GetElevation(point);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //public void f(string CentroidName, string ThiessenName)
        //{
        //    Geoprocessor pGeop = new Geoprocessor();
        //    pGeop.OverwriteOutput = true;
        //    ESRI.ArcGIS.AnalysisTools.CreateThiessenPolygons pThiessen = new ESRI.ArcGIS.AnalysisTools.CreateThiessenPolygons();
        //    pThiessen.in_features = CentroidName;
        //    pThiessen.out_feature_class = ThiessenName;
        //    pThiessen.fields_to_copy = "ONLY_FID";
        //    pGeop.Execute(pThiessen, null);
        //}

        public static List<IPoint> getPoint(List<List<IPoint>> Linelist)
        {
            List<IPoint> pcolloc = new List<IPoint>();

            //线的数目
            int num_line = Linelist.Count();

            for (int i = 0; i < num_line; i++)
            {
                int n = Linelist[i].Count();

                pcolloc.Add(Linelist[i][0]);
                pcolloc.Add(Linelist[i][n-1]);

            }

            return pcolloc;
        }
    }
}
