using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace test
{
    class Get_Counterpart
    {
        //public static double LT_c = 200;

        //public static List<List<IPoint>> counterpart_list = new List<List<IPoint>>();

        ////获取对口河
        //public static List<List<IPoint>> getCounterpart(List<List<IPoint>> Linelist, List<String> toid, double ST,double LT)
        //{
        //    Form1.yuan_id.Clear();

        //    List<List<int>> source_river = new List<List<int>>();

        //    //线的数目
        //    int num_line = Linelist.Count();

        //    for (int i = 0; i < num_line; i++)
        //    {
        //        List<int> temp_lineid = new List<int>();

        //        source_river.Add(temp_lineid);
        //    }

        //    //记录每条河流的源头河流序号
        //    for (int i = 0; i < num_line; i++)
        //    {
        //        int tid = int.Parse(toid[i]) - 1;

        //        if (tid < 0)
        //            continue;

        //        //int fid = int.Parse(fromid[i]) - 1;

        //        int fid = i;

        //        source_river[tid].Add(fid);
        //    }

        //    int colineid1, colineid2;

        //    for (int i = 0; i < num_line; i++)
        //    {
        //        for (int j = 0; j < source_river[i].Count() - 1; j++)
        //        {
        //            colineid1 = source_river[i][j];

        //            for (int k = j + 1; k < source_river[i].Count(); k++)
        //            {
        //                colineid2 = source_river[i][k];

        //                //double s_curb = near_curb(Linelist[colineid1], Linelist[colineid2],ST,LT);

        //                double s_curb = near_ang(Linelist[colineid1], Linelist[colineid2], ST, LT, 20);

        //                //if (s_curb > 0)
        //                //{
        //                //    Form1.Coun_curb_S.Add(s_curb);

        //                //    //Form1.yuan_id.Add(colineid1);
                        
        //                //    Form1.Coun_curb_S.Add(s_curb);

        //                //    //Form1.yuan_id.Add(colineid2);
        //                //}

                        
        //            }//for (int k
        //        }// for (int j

        //    }// for (int i
            
        //    return counterpart_list;
        //}

        ////用直线近似度识别对口河
        //public static double near_curb(List<IPoint> line1, List<IPoint> line2,double ST,double LT)
        //{
        //    List<IPoint> t_line1 = new List<IPoint>();

        //    List<IPoint> t_line2 = new List<IPoint>();

        //    List<IPoint> line_sum = new List<IPoint>();

        //    double s = 0, line1_dis = 0, line2_dis = 0;

        //    int n1, n2;

        //    switch (util.adjacent(line1, line2))
        //    {
        //        case 1:
        //            {
        //                n1 = util.minstra(line1, 0, ST,1);

        //                n2 = util.minstra(line2, 0, ST, 1);

        //                for (int i = n1; i > 0; i--)
        //                {
        //                    line1_dis += util.getDistanceOfTwoPoints(line1[i], line1[i - 1]);

        //                    t_line1.Add(line1[i]);
        //                    line_sum.Add(line1[i]);
        //                }
        //                if (line1_dis < LT) return -1;
        //                t_line1.Add(line1[0]);
        //                line_sum.Add(line1[0]);

        //                for (int i = 0; i < n2; i++)
        //                {
        //                    line2_dis += util.getDistanceOfTwoPoints(line2[i], line2[i + 1]);

        //                    t_line2.Add(line2[i]);
        //                    line_sum.Add(line2[i]);
        //                }
        //                if (line2_dis < LT) return -1;
        //                t_line2.Add(line2[n2]);
        //                line_sum.Add(line2[n2]);

        //            } break;
        //        case 2:
        //            {
        //                n1 = util.minstra(line1, 0, ST,1);

        //                n2 = util.minstra(line2, line2.Count() - 1, ST,-1);

        //                for (int i = n2; i < line2.Count() - 1; i++)
        //                {
        //                    line2_dis += util.getDistanceOfTwoPoints(line2[i], line2[i + 1]);

        //                    t_line2.Add(line2[i]);
        //                    line_sum.Add(line2[i]);
        //                }
        //                if (line2_dis < LT) return -1;
        //                t_line2.Add(line2[line2.Count() - 1]);
        //                line_sum.Add(line2[line2.Count() - 1]);

        //                for (int i = 0; i < n1; i++)
        //                {
        //                    line1_dis += util.getDistanceOfTwoPoints(line1[i], line1[i + 1]);

        //                    t_line1.Add(line1[i]);
        //                    line_sum.Add(line1[i]);
        //                }
        //                if (line1_dis < LT) return -1;
        //                t_line1.Add(line1[n1]);
        //                line_sum.Add(line1[n1]);        

        //            }
        //            break;
        //        case 3:
        //            {
        //                n1 = util.minstra(line1, line1.Count() - 1, ST,-1);

        //                n2 = util.minstra(line2, 0, ST,1);

        //                for (int i = n1; i < line1.Count() - 1; i++)
        //                {
        //                    line1_dis += util.getDistanceOfTwoPoints(line1[i], line1[i + 1]);

        //                    t_line1.Add(line1[i]);
        //                    line_sum.Add(line1[i]);
        //                }
        //                if (line1_dis < LT) return -1;
        //                t_line1.Add(line1[line1.Count() - 1]);
        //                line_sum.Add(line1[line1.Count() - 1]);

        //                for (int i = 0; i < n2; i++)
        //                {
        //                    line2_dis += util.getDistanceOfTwoPoints(line2[i], line2[i + 1]);

        //                    t_line2.Add(line2[i]);
        //                    line_sum.Add(line2[i]);
        //                }
        //                if (line2_dis < LT) return -1;
        //                t_line2.Add(line2[n2]);
        //                line_sum.Add(line2[n2]);

        //            }
        //            break;
        //        case 4:
        //            {
        //                n1 = util.minstra(line1, line1.Count() - 1, ST,-1);

        //                n2 = util.minstra(line2, line2.Count() - 1, ST,-1);

        //                for (int i = n1; i < line1.Count() - 1; i++)
        //                {
        //                    line1_dis += util.getDistanceOfTwoPoints(line1[i], line1[i + 1]);

        //                    t_line1.Add(line1[i]);
        //                    line_sum.Add(line1[i]);
        //                }
        //                if (line1_dis < LT) return -1;
        //                t_line1.Add(line1[line1.Count() - 1]);
        //                line_sum.Add(line1[line1.Count() - 1]);

        //                for (int i = line2.Count() - 1; i > n2; i--)
        //                {
        //                    line2_dis += util.getDistanceOfTwoPoints(line2[i], line2[i - 1]);

        //                    t_line2.Add(line2[i]);
        //                    line_sum.Add(line2[i]);
        //                }
        //                if (line2_dis < LT) return -1;
        //                t_line2.Add(line2[n2]);
        //                line_sum.Add(line2[n2]);

        //            } break;

        //        default: return -1; 
        //    }

        //    s = (line1_dis + line2_dis) / util.getDistanceOfTwoPoints(line1[n1], line2[n2]);

        //    double n_x = 0;
        //    double n_y = 100;
        //    double f_x = line2[n2].X - line1[n1].X;
        //    double f_y = line2[n2].Y - line1[n1].Y;

        //    double nl = Math.Sqrt(Math.Pow(n_x, 2) + Math.Pow(n_y, 2));

        //    double fl = Math.Sqrt(Math.Pow(f_x, 2) + Math.Pow(f_y, 2));

        //    double f_ang = 0;

        //    if (f_x > 0)
        //        f_ang = Math.Acos((n_x * f_x + n_y * f_y) / (nl * fl)) / Math.PI * 180;
        //    else
        //        f_ang = 360 - Math.Acos((n_x * f_x + n_y * f_y) / (nl * fl)) / Math.PI * 180;


        //    if ( s <= ST)
        //    {
        //        double sumdis_c = line1_dis + line2_dis;

        //        counterpart_list.Add(line_sum);
        //        Form1.length.Add(sumdis_c);
        //        Form1.yuan_id.Add(f_ang);
        //        Form1.Coun_curb_S.Add(s);

        //        //Form1.Coun_curb_S.Add(s);
        //        //counterpart_list.Add(t_line2);
        //        //Form1.length.Add(line2_dis);
        //        //Form1.yuan_id.Add(f_ang);

        //        return s;
        //    }
        //    else
        //    {
        //        return -1;
        //    }


        //}

        ////用河流交汇角识别对口河
        //public static double near_ang(List<IPoint> line1, List<IPoint> line2,double ST,double LT,double AT)
        //{
        //    List<IPoint> t_line1 = new List<IPoint>();

        //    List<IPoint> t_line2 = new List<IPoint>();

        //    Point fr_p = new Point();
        //    Point mid_p = new Point();
        //    Point to_p = new Point(); 

        //    double line1_dis = 0, line2_dis = 0;

        //    int n1, n2;

        //    switch (util.adjacent(line1, line2))
        //    {
        //        case 1:
        //            {
        //                mid_p.X = line1[0].X;

        //                mid_p.Y = line1[0].Y;

        //                n1 = util.minstra(line1, 0, ST, 1);

        //                n2 = util.minstra(line2, 0, ST, 1);

        //                for (int i = 0; i < n1; i++)
        //                {
        //                    line1_dis += util.getDistanceOfTwoPoints(line1[i], line1[i + 1]);

        //                    t_line1.Add(line1[i]);
        //                }
        //                if (line1_dis < LT) return -1;
        //                t_line1.Add(line1[n1]);

        //                for (int i = 0; i < n2; i++)
        //                {
        //                    line2_dis += util.getDistanceOfTwoPoints(line2[i], line2[i + 1]);

        //                    t_line2.Add(line2[i]);
        //                }
        //                if (line2_dis < LT) return -1;
        //                t_line2.Add(line2[n2]);

        //            } break;
        //        case 2:
        //            {
        //                mid_p.X = line1[0].X;

        //                mid_p.Y = line1[0].Y;

        //                n1 = util.minstra(line1, 0, ST, 1);
        //                n2 = util.minstra(line2, line2.Count() - 1, ST, -1);
                    

        //                for (int i = 0; i < n1; i++)
        //                {
        //                    line1_dis += util.getDistanceOfTwoPoints(line1[i], line1[i + 1]);

        //                    t_line1.Add(line1[i]);
        //                }
        //                if (line1_dis < LT) return -1;
        //                t_line1.Add(line1[n1]);

        //                for (int i = n2; i < line2.Count() - 1; i++)
        //                {
        //                    line2_dis += util.getDistanceOfTwoPoints(line2[i], line2[i + 1]);

        //                    t_line2.Add(line2[i]);
        //                }
        //                if (line2_dis < LT) return -1;
        //                t_line2.Add(line2[line2.Count() - 1]);

        //            }
        //            break;
        //        case 3:
        //            {
        //                mid_p.X = line1[line1.Count() - 1].X;

        //                mid_p.Y = line1[line1.Count() - 1].Y;

        //                n1 = util.minstra(line1, line1.Count() - 1, ST, -1);

        //                n2 = util.minstra(line2, 0, ST,1);

        //                for (int i = n1; i < line1.Count() - 1; i++)
        //                {
        //                    line1_dis += util.getDistanceOfTwoPoints(line1[i], line1[i + 1]);

        //                    t_line1.Add(line1[i]);
        //                }
        //                if (line1_dis < LT)  return -1;
        //                t_line1.Add(line1[line1.Count() - 1]);

        //                for (int i = 0; i < n2; i++)
        //                {
        //                    line2_dis += util.getDistanceOfTwoPoints(line2[i], line2[i + 1]);

        //                    t_line2.Add(line2[i]);
        //                }
        //                if (line2_dis < LT)  return -1;
        //                t_line2.Add(line2[n2]);

        //            }
        //            break;
        //        case 4:
        //            {
        //                mid_p.X = line1[line1.Count() - 1].X;

        //                mid_p.Y = line1[line1.Count() - 1].Y;

        //                n1 = util.minstra(line1, line1.Count() - 1, ST, -1);

        //                n2 = util.minstra(line2, line2.Count() - 1, ST, -1);

        //                for (int i = n1; i < line1.Count() - 1; i++)
        //                {
        //                    line1_dis += util.getDistanceOfTwoPoints(line1[i], line1[i + 1]);

        //                    t_line1.Add(line1[i]);
        //                }
        //                if (line1_dis < LT)  return -1;
        //                t_line1.Add(line1[line1.Count() - 1]);

        //                for (int i = n2; i < line2.Count() - 1; i++)
        //                {
        //                    line2_dis += util.getDistanceOfTwoPoints(line2[i], line2[i + 1]);

        //                    t_line2.Add(line2[i]);
        //                }
        //                if (line2_dis < LT)  return -1;
        //                t_line2.Add(line2[line2.Count() - 1]);

        //            } break;

        //        default: return -1; 
        //    }

        //    double a = line1[n1].X - mid_p.X;
        //    double b = line1[n1].Y - mid_p.Y;
        //    double c = line2[n2].X - mid_p.X;
        //    double d = line2[n2].Y - mid_p.Y;

        //    double nn1 = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));

        //    double nn2 = Math.Sqrt(Math.Pow(c, 2) + Math.Pow(d, 2));

        //    double ang = Math.Acos((a * c + b * d) / (nn1 * nn2)) / Math.PI * 180;

        //    if (Math.Abs(ang - 180) < AT)
        //    {
        //        counterpart_list.Add(t_line1);
        //        Form1.length.Add(line1_dis);

        //        counterpart_list.Add(t_line2);
        //        Form1.length.Add(line2_dis);

        //        return ang;
        //    }
        //    else
        //    {
        //        return -1;
        //    }

        //    //double curb_c = (line1_dis + line2_dis) / Form1.getDistanceOfTwoPoints(fr_p, to_p);

        //    //if (curb_c < ST)
        //    //{
        //    //    counterpart_list.Add(t_line1);
        //    //    Form1.length.Add(line1_dis);

        //    //    counterpart_list.Add(t_line2);
        //    //    Form1.length.Add(line2_dis);

        //    //    return curb_c;
        //    //}
        //    //else
        //    //{
        //    //    return -1;
        //    //}

        //}

       

    }
}
