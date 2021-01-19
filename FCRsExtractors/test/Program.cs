using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Carto;
//using ESRI.ArcGIS.Display;
using System.IO;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;
using ESRI.ArcGIS.ADF;

namespace test
{
    static class Program
    {
        //private static LicenseInitializer m_AOLicenseInitializer = new WindowsFormMain.LicenseInitializer();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine);

            //#region 初始化许可
            IAoInitialize m_AoInitialize = new AoInitializeClass();
            esriLicenseStatus licenseStatus = esriLicenseStatus.esriLicenseUnavailable;
            licenseStatus = m_AoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngine);
            //licenseStatus = m_AoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced);
            //licenseStatus = m_AoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeArcInfo);

            licenseStatus = m_AoInitialize.CheckOutExtension(esriLicenseExtensionCode.esriLicenseExtensionCodeRuntimeAdvanced);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
