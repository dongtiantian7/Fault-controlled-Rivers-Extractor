using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace test
{
    class Entity
    {
        public static List<List<IPoint>> Linelist = new List<List<IPoint>>();
        public static List<String> from_id = new List<String>();
        public static List<String> to_id = new List<String>();
        public static List<List<int>> source_river = new List<List<int>>();

        public static double LT;
        public static double ST;

        public static List<string> AttributeName = new List<String>();
    

        public static List<List<IPoint>> arg_point = new List<List<IPoint>>();
        public static List<double> priver_id = new List<double>();
        public static List<double> sstr_length = new List<double>();
        public static List<double> sstr_curb = new List<double>();
        public static List<double> bpoint_id = new List<double>();

        public static List<List<IPoint>> arg_line = new List<List<IPoint>>();
        public static List<double> friver_id = new List<double>();
        public static List<double> triver_id = new List<double>();
        public static List<double> lturn_angle = new List<double>();



        //public static double AT;
        //public static double MD;

        //public static List<List<IPoint>> straight_line = new List<List<IPoint>>();
        //public static List<double> curb_S = new List<double>();
        //public static List<double> strlength = new List<double>();
        //public static List<double> str_yuan_id = new List<double>();

        //public static List<List<IPoint>> Rigntangle_list = new List<List<IPoint>>();
        //public static List<double> turn_angle = new List<double>();
        //public static List<double> tur_length = new List<double>();
        //public static List<double> tur_yuan_id = new List<double>();

        //public static List<List<IPoint>> Barb_riverline_list = new List<List<IPoint>>();
        //public static List<double> inter_angle = new List<double>();
        //public static List<double> bar_length = new List<double>();
        //public static List<double> bar_yuan_id = new List<double>();

        //public static List<List<IPoint>> Counterpart_list = new List<List<IPoint>>();
        //public static List<double> Coun_turn_angle = new List<double>();
        //public static List<double> cou_length = new List<double>();
        //public static List<double> cou_yuan_id = new List<double>();

    }
}
