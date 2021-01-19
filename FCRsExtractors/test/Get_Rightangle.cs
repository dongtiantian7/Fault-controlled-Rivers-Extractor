using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace test
{
    class Get_Rightangle
    {
        ////获取直角转弯
        //public static List<List<IPoint>> getRightangle(List<List<IPoint>> Linelist, double ST,double LT, double MD, double AT)
        //{
        //    List<List<IPoint>> rightangle_list = new List<List<IPoint>>();

        //    Form1.yuan_id.Clear();
        //    Form1.length.Clear();

        //    //线的数目
        //    int num_line = Linelist.Count();

        //    IPoint L1Bp, L1Ep, L2Bp, L2Ep;

        //    int L1B, L1E, L2B, L2E;

        //    double L1_dis, L2_dis, mid_dis;

        //    for (int i = 0; i < num_line; i++)
        //    {
        //        L1B = L1E = L2B = L2E = 0;

        //        L1_dis = L2_dis = mid_dis = 0;

        //        //double temp_sumdis = 0;

        //        //for (int ii = 0; ii < Linelist[i].Count() - 1; ii++)
        //        //{
        //        //    temp_sumdis += Form1.getDistanceOfTwoPoints(Linelist[i][ii], Linelist[i][ii+1]);
        //        //}

                    
        //        while (L1E < Linelist[i].Count() - 1)
        //        {
        //            while ((L1_dis < LT) && (L1E < Linelist[i].Count() - 1))
        //            {
        //                L1B = L1E;

        //                L1E = util.minstra(Linelist[i], L1B, ST, 1);

        //                L1_dis = util.sumdis_line(Linelist[i], L1B, L1E);
        //            }

        //            //如果整条河流都是直线 或 找不到长度符合条件的L1 或 不能找到L2
        //            if (L1E >= Linelist[i].Count() - 1)
        //                break;

        //            L2E = L1E;

        //            while ((L2_dis < LT) && (L2B < Linelist[i].Count() - 1))
        //            {
        //                L2B = L2E;

        //                L2E = util.minstra(Linelist[i], L2B, ST, 1);

        //                L2_dis = util.sumdis_line(Linelist[i], L2B, L2E);
        //            }

        //            //如果找不到符合条件的L2
        //            if (L2B >= Linelist[i].Count() - 1)
        //                break;

        //            mid_dis = util.sumdis_line(Linelist[i], L1E, L2B);
        //            mid_dis = util.sumdis_line(Linelist[i], L1E, L2B);

        //            //如果L1和L2没有相距过远
        //            if (mid_dis < MD)
        //            {
        //                L1Bp = Linelist[i][L1B];
        //                L1Ep = Linelist[i][L1E];
        //                L2Bp = Linelist[i][L2B];
        //                L2Ep = Linelist[i][L2E];


        //                //double L1_x = L1Ep.X - L1Bp.X;
        //                //double L1_y = L1Ep.Y - L1Bp.Y;
        //                //double L2_x = L2Ep.X - L2Bp.X;
        //                //double L2_y = L2Ep.Y - L2Bp.Y;

        //                //double L1_n = Math.Sqrt(Math.Pow(L1_x, 2) + Math.Pow(L1_y, 2));

        //                //double L2_n = Math.Sqrt(Math.Pow(L2_x, 2) + Math.Pow(L2_y, 2));

        //                //double ang = Math.Acos((L1_x * L2_x + L1_y * L2_y) / (L1_n * L2_n)) / Math.PI * 180;

        //                double ang = util.getangle(L1Bp, L1Ep, L2Bp, L2Ep);

        //                if (Math.Abs(ang - 90) < AT)
        //                {
        //                    //double n_x = 0;
        //                    //double n_y = 100;
        //                    //double f_x = L2Ep.X - L2Bp.X;
        //                    //double f_y = L2Ep.Y - L2Bp.Y;

        //                    //double nl = Math.Sqrt(Math.Pow(n_x, 2) + Math.Pow(n_y, 2));

        //                    //double fl = Math.Sqrt(Math.Pow(f_x, 2) + Math.Pow(f_y, 2));

        //                    //double f_ang = 0;

        //                    //if (f_x > 0)
        //                    //    f_ang = Math.Acos((n_x * f_x + n_y * f_y) / (nl * fl)) / Math.PI * 180;
        //                    //else
        //                    //    f_ang = 360 - Math.Acos((n_x * f_x + n_y * f_y) / (nl * fl)) / Math.PI * 180;

        //                    List<IPoint> Linetemp1 = new List<IPoint>();

        //                    List<IPoint> Linetempsum = new List<IPoint>();

        //                    for (int j = L1B; j <= L1E; j++)
        //                    {
        //                        Linetemp1.Add(Linelist[i][j]);
        //                        Linetempsum.Add(Linelist[i][j]);
        //                    }

        //                    rightangle_list.Add(Linetemp1);

        //                    Form1.turn_angle.Add(ang);

        //                    double s1 = L1_dis / util.getDistanceOfTwoPoints(Linelist[i][L1B], Linelist[i][L1E]);
        //                    Form1.yuan_id.Add(s1);

        //                    Form1.length.Add(L1_dis);

        //                    List<IPoint> Linetemp2 = new List<IPoint>();
        //                    for (int j = L1E; j <= L2B; j++)
        //                    {
        //                        Linetemp2.Add(Linelist[i][j]);
        //                        Linetempsum.Add(Linelist[i][j]);
        //                    }
        //                    rightangle_list.Add(Linetemp2);

        //                    Form1.turn_angle.Add(ang);

        //                    double sm = mid_dis / util.getDistanceOfTwoPoints(Linelist[i][L1E], Linelist[i][L2B]);
        //                    Form1.yuan_id.Add(sm);

        //                    Form1.length.Add(mid_dis);


        //                    List<IPoint> Linetemp3 = new List<IPoint>();
        //                    for (int j = L2B; j <= L2E; j++)
        //                    {
        //                        Linetemp3.Add(Linelist[i][j]);
        //                        Linetempsum.Add(Linelist[i][j]);
        //                    }

        //                    rightangle_list.Add(Linetemp3);

        //                    Form1.turn_angle.Add(ang);

        //                    double s2 = L2_dis / util.getDistanceOfTwoPoints(Linelist[i][L2B], Linelist[i][L2E]);
        //                    Form1.yuan_id.Add(s2);

        //                    Form1.length.Add(L2_dis);

        //                    //输出河流
        //                    //rightangle_list.Add(Linelist[i]);

        //                    //输出整个直角河段
        //                    //rightangle_list.Add(Linetempsum);

        //                    //Form1.turn_angle.Add(ang);

        //                    //Form1.yuan_id.Add(f_ang);

        //                    //double sumdis_r = L1_dis + L2_dis + mid_dis;
        //                    //Form1.length.Add(sumdis_r);

        //                    //break;

        //                }//if (Math.Abs(ang - 90) < AT)

        //            }//if (mid_dis < max_LT)


        //            L1B = L2B;

        //            L1E = L2E;

        //            L1_dis = L2_dis; L2_dis = mid_dis = 0;


        //        }//while
        //    }//for

        //    return rightangle_list;
        //}

    }
}
