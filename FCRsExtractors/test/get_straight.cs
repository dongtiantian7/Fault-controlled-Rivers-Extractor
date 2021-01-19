 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace test
{
    class get_straight
    {


        ////获取直线河段
        //public static List<List<IPoint>> getStraightLine(List<List<IPoint>> Linelist,double LT,double ST)
        //{
        //    //存储直线链的序号
        //    List<List<IPoint>> straightlist = new List<List<IPoint>>();

        //    Form1.yuan_id.Clear();
        //    Form1.length.Clear();
        //    Form1.curb_S.Clear();

        //    //线的数目
        //    int num_line = Linelist.Count();

        //    int n = 0;

        //    for (int i = 0; i < num_line; i++)
        //    {
        //        ////当前线中的折点个数
        //        //int pointcount = Linelist[i].Count();

        //        //double sum_dis = 0;

        //        //for (int j = 0; j < pointcount - 1; j++)
        //        //{
        //        //    sum_dis += util.getDistanceOfTwoPoints(Linelist[i][j], Linelist[i][j + 1]);
        //        //}

        //        //if (sum_dis < LT) continue;

        //        //double S = sum_dis / util.getDistanceOfTwoPoints(Linelist[i][0], Linelist[i][pointcount - 1]);

        //        //if (S <= ST)
        //        //{
        //        //    double n_x = 0;
        //        //    double n_y = 100;
        //        //    double f_x = Linelist[i][pointcount - 1].X - Linelist[i][0].X;
        //        //    double f_y = Linelist[i][pointcount - 1].Y - Linelist[i][0].Y;

        //        //    double nl = Math.Sqrt(Math.Pow(n_x, 2) + Math.Pow(n_y, 2));

        //        //    double fl = Math.Sqrt(Math.Pow(f_x, 2) + Math.Pow(f_y, 2));

        //        //    double ang = 0;

        //        //    if (f_x < 0)
        //        //        ang = 360 - Math.Acos((n_x * f_x + n_y * f_y) / (nl * fl)) / Math.PI * 180;
        //        //    else
        //        //        ang = Math.Acos((n_x * f_x + n_y * f_y) / (nl * fl)) / Math.PI * 180;

        //        //    //若当前待存储直线河段是第一条符合条件的河流
        //        //    if (n == 0)
        //        //    {
        //        //        straightlist.Add(Linelist[i]);

        //        //        Form1.curb_S.Add(S);

        //        //        Form1.yuan_id.Add(i);

        //        //        Form1.length.Add(sum_dis);

        //        //        n++;
        //        //    }
        //        //    else
        //        //    {
        //        //        int c = straightlist[n - 1].Count();
        //        //        double tem_sumdis = Form1.curb_S[n - 1] * util.getDistanceOfTwoPoints(straightlist[n - 1][0], straightlist[n - 1][c - 1]) + sum_dis;
        //        //        double s1 = 0;

        //        //        switch (util.adjacent(straightlist[n - 1], Linelist[i]))
        //        //        {
        //        //            case 1: s1 = (tem_sumdis + sum_dis) / util.getDistanceOfTwoPoints(straightlist[n - 1][c - 1], Linelist[i][pointcount - 1]); break;
        //        //            case 2: s1 = (tem_sumdis + sum_dis) / util.getDistanceOfTwoPoints(straightlist[n - 1][c - 1], Linelist[i][0]); break;
        //        //            case 3: s1 = (tem_sumdis + sum_dis) / util.getDistanceOfTwoPoints(straightlist[n - 1][0], Linelist[i][pointcount - 1]); break;
        //        //            case 4: s1 = (tem_sumdis + sum_dis) / util.getDistanceOfTwoPoints(straightlist[n - 1][0], Linelist[i][0]); break;
        //        //            default: s1 = 10; break;
        //        //        }

        //        //        if (s1 <= ST)
        //        //            for (int j = 0; j < pointcount; j++)
        //        //            {
        //        //                straightlist[n - 1].Add(Linelist[i][j]);
        //        //                Form1.curb_S[n - 1] = s1;
        //        //                Form1.length[n - 1] += sum_dis;
        //        //            }
        //        //        else
        //        //        {
        //        //            straightlist.Add(Linelist[i]);
        //        //            Form1.curb_S.Add(S);
        //        //            Form1.yuan_id.Add(ang);
        //        //            Form1.length.Add(sum_dis);

        //        //            n++;
        //        //        }
        //        //    }

        //        //}//if
        //        //else
        //        //{ 
        //        ////LB = 0;
        //        //    int LB = 0,LE = 0;
        //        //    int lastLB = -1,lastLE = -1,lastLM = -1;

        //        //    while (LE < Linelist[i].Count()-1)
        //        //    {
        //        //        List<IPoint> Linetp = new List<IPoint>();

        //        //        if (lastLM >= 0)
        //        //            Linetp.Add(Linelist[i][lastLM]);

        //        //        LB = LE;

        //        //        LE = util.minstra(Linelist[i], LB, ST, 1);

        //        //        if ((util.sumdis_line(Linelist[i], LB, LE) < LT)) continue;

        //        //        //LM = (LB + LE) / 2;

        //        //        //Entity.arg_point[i].Add(Linelist[i][LM]);
        //        //        //Entity.priver_id.Add(i);
        //        //        //Entity.bpoint_id.Add(LB);
        //        //        //Entity.sstr_length.Add(util.sumdis_line(Linelist[i], LB, LE));

        //        //        S = util.sumdis_line(Linelist[i], LB, LE) / util.getDistanceOfTwoPoints(Linelist[i][LB], Linelist[i][LE]);
                        
        //        //        Form1.curb_S.Add(S);

        //        //        Form1.yuan_id.Add(ang);

        //        //        Form1.length.Add(sum_dis);

        //        //        //Entity.sstr_curb.Add(S);

        //        //        //pcount++;

        //        //        if (lastLM >= 0)
        //        //        {
        //        //            Linetp.Add(Linelist[i][LM]);

        //        //            double a = util.getangle(Linelist[i][lastLB], Linelist[i][lastLE], Linelist[i][LB], Linelist[i][LE]);

        //        //            //记录河流转弯
        //        //            Entity.arg_line.Add(Linetp);
        //        //            Entity.friver_id.Add(pcount-2);
        //        //            Entity.triver_id.Add(pcount-1);
        //        //            Entity.lturn_angle.Add(a);
        //        //        }
                        
        //        //        lastLM = LM; lastLB = LE; lastLE = LB;
        //        //    }//while
        //        //}

             
        //        //当前线中的折点个数
        //        int pointcount = Linelist[i].Count();

        //        double sum_dis = util.sumdis_line(Linelist[i], 0, pointcount - 1);

        //        //如果河流过短，则不记录。
        //        if (sum_dis < 2 * LT) continue;

        //        double S = sum_dis / util.getDistanceOfTwoPoints(Linelist[i][0], Linelist[i][pointcount - 1]);

        //        //当前河段若为直线河段
        //        if (S < ST)
        //        {
        //            straightlist.Add(Linelist[i]);
        //            Form1.curb_S.Add(S);
        //            Form1.yuan_id.Add(i);
        //            Form1.length.Add(sum_dis);

        //        }
        //        else//当前河段若发生转弯
        //        {
        //            int LB = 0, LE = 0;

        //            while (LE < Linelist[i].Count() - 1)
        //            {
        //                List<IPoint> Linetp = new List<IPoint>();

        //                LB = LE;

        //                LE = util.minstra(Linelist[i], LB, ST, 1);

        //                sum_dis = util.sumdis_line(Linelist[i], LB, LE) ;

        //                if ((sum_dis < LT)) continue;

        //                for (int j = LB; j <= LE ; j++)
        //                {
        //                    Linetp.Add(Linelist[i][j]);
        //                }
                        
        //                S = sum_dis / util.getDistanceOfTwoPoints(Linelist[i][LB], Linelist[i][LE]);

        //                straightlist.Add(Linetp);
        //                Form1.curb_S.Add(S);
        //                Form1.yuan_id.Add(i);
        //                Form1.length.Add(sum_dis);

        //            }//while
        //        }//else当前河段若发生转弯

        //    }//for i
        //    return straightlist;
        //}

       
    }
}
