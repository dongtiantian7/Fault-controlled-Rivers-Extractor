using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;



namespace test
{
    public partial class 属性表 : Form
    {



        public 属性表()
        {
            InitializeComponent();

        }

        private void 属性表_Load(object sender, EventArgs e)
        {
            
        }

        public DataTable attributeTable;

        private static DataTable CreateDataTableByLayer(ILayer pLayer, string tableName)
        {
            //创建一个DataTable表
            DataTable pDataTable = new DataTable(tableName);
            //取得ITable接口
            ITable pTable = pLayer as ITable;
            IField pField = null;
            DataColumn pDataColumn;
            //根据每个字段的属性建立DataColumn对象
            for (int i = 0; i < pTable.Fields.FieldCount; i++)
            {
                pField = pTable.Fields.get_Field(i);
                //新建一个DataColumn并设置其属性
                pDataColumn = new DataColumn(pField.Name);
                if (pField.Name == pTable.OIDFieldName)
                {
                    pDataColumn.Unique = true;//字段值是否唯一
                }
                //字段值是否允许为空
                pDataColumn.AllowDBNull = pField.IsNullable;
                //字段别名
                pDataColumn.Caption = pField.AliasName;
                //字段数据类型
                pDataColumn.DataType = System.Type.GetType(ParseFieldType(pField.Type));
                //字段默认值
                pDataColumn.DefaultValue = pField.DefaultValue;
                //当字段为String类型是设置字段长度
                if (pField.VarType == 8)
                {
                    pDataColumn.MaxLength = pField.Length;
                }
                //字段添加到表中
                pDataTable.Columns.Add(pDataColumn);
                pField = null;
                pDataColumn = null;
            }
            return pDataTable;
        }


        //将EsriType转换为String
        public static string ParseFieldType(esriFieldType fieldType)
        {
            switch (fieldType)
            {
                case esriFieldType.esriFieldTypeBlob:
                    return "System.String";
                case esriFieldType.esriFieldTypeDate:
                    return "System.DateTime";
                case esriFieldType.esriFieldTypeDouble:
                    return "System.Double";
                case esriFieldType.esriFieldTypeGeometry:
                    return "System.String";
                case esriFieldType.esriFieldTypeGlobalID:
                    return "System.String";
                case esriFieldType.esriFieldTypeGUID:
                    return "System.String";
                case esriFieldType.esriFieldTypeInteger:
                    return "System.Int32";
                case esriFieldType.esriFieldTypeOID:
                    return "System.String";
                case esriFieldType.esriFieldTypeRaster:
                    return "System.String";
                case esriFieldType.esriFieldTypeSingle:
                    return "System.Single";
                case esriFieldType.esriFieldTypeSmallInteger:
                    return "System.Int32";
                case esriFieldType.esriFieldTypeString:
                    return "System.String";
                default:
                    return "System.String";
            }
        }

        public static DataTable CreateDataTable(ILayer pLayer, string tableName)
        {
            //创建空DataTable
            DataTable pDataTable = CreateDataTableByLayer(pLayer, tableName);
            //取得图层类型
            string shapeType = getShapeType(pLayer);
            //创建DataTable的行对象
            DataRow pDataRow = null;
            //从ILayer查询到ITable
            ITable pTable = pLayer as ITable;
            ICursor pCursor = pTable.Search(null, false);
            //取得ITable中的行信息
            IRow pRow = pCursor.NextRow();
            int n = 0;
            while (pRow != null)
            {
                //新建DataTable的行对象
                pDataRow = pDataTable.NewRow();
                for (int i = 0; i < pRow.Fields.FieldCount; i++)
                {
                    //如果字段类型为esriFieldTypeGeometry，则根据图层类型设置字段值
                    if (pRow.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        pDataRow[i] = shapeType;
                    }
                    //当图层类型为Anotation(注释)时，要素类中会有esriFieldTypeBlob类型的数据，
                    //其存储的是标注内容，如此情况需将对应的字段值设置为Element
                    else if (pRow.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeBlob)
                    {
                        pDataRow[i] = "Element";
                    }
                    else
                    {
                        pDataRow[i] = pRow.get_Value(i);
                    }
                }
                //添加DataRow到DataTable
                pDataTable.Rows.Add(pDataRow);
                pDataRow = null;
                n++;
                //为保证效率，一次只装载最多条记录
                if (n == 2000)
                {
                    pRow = null;
                }
                else
                {
                    pRow = pCursor.NextRow();
                }
            }
            return pDataTable;
        }


        public static string getShapeType(ILayer pLayer)
        {
            IFeatureLayer pFeatLyr = (IFeatureLayer)pLayer;
            switch (pFeatLyr.FeatureClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    return "Point";
                case esriGeometryType.esriGeometryPolyline:
                    return "Polyline";
                case esriGeometryType.esriGeometryPolygon:
                    return "Polygon";
                default:
                    return "";
            }
        }


        public void CreateAttributeTable(ILayer player)
        {
            string tableName;
            tableName = getValidFeatureClassName(player.Name);
            attributeTable = CreateDataTable(player, tableName);
            this.dataGridView1.DataSource = attributeTable;
            this.Text = "属性表[" + tableName + "]  " + "记录数：" + attributeTable.Rows.Count.ToString();
        }

        public static string getValidFeatureClassName(string FCname)
        {
            int dot = FCname.IndexOf(".");
            if (dot != -1)
            {
                return FCname.Replace(".", "_");
            }
            return FCname;
        }

    }
}
