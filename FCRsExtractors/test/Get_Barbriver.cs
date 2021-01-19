using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace test
{
    class Get_Barbriver
    {
        ////获取倒钩河
        //public static List<List<IPoint>> getBarbriver(List<List<IPoint>> Linelist, List<String> fromid, List<String> toid, double ST, double LT)
        //{
        //    List<List<IPoint>> Barb_riverlist = new List<List<IPoint>>();

        //    Form1.yuan_id.Clear();
        //    Form1.length.Clear();

        //    //线的数目
        //    int num_line = Linelist.Count();
           
        //    for (int i = 0; i < num_line; i++)
        //    {
        //        //记录当前河流的序号
        //        //int fid = int.Parse(fromid[i]) - 1;

        //        int fid = int.Parse(fromid[i]);
        //        int tid = int.Parse(toid[i]) - 1;

        //        if (tid < 0) continue;

        //        //当前线中的折点个数
        //        int pcount1 = Linelist[fid].Count();
        //        int pcount2 = Linelist[tid].Count();

        //        double x0 = 0, y0 = 0, x1, x2, y1, y2;

        //        int i1 = -1, i2 = -1;

        //        List<IPoint> line_temp = new List<IPoint>();

        //        double sumdis_bar = 0;

        //        switch (util.adjacent(Linelist[fid], Linelist[tid]))
        //        {
                 
        //            case 1:
        //                {
        //                    x0 = Linelist[fid][0].X;

        //                    y0 = Linelist[fid][0].Y;

        //                    i1 = util.minstra(Linelist[fid], 0, ST,1);

        //                    for (int j = 0; j < i1 ; j++)
        //                    {
        //                        line_temp.Add(Linelist[fid][j]);
        //                        sumdis_bar += util.getDistanceOfTwoPoints(Linelist[fid][j], Linelist[fid][j + 1]);
        //                    }
        //                    line_temp.Add(Linelist[fid][i1]);

        //                    i2 = util.minstra(Linelist[tid], 0, ST,1); 
        //                } break;
        //            case 2:
        //                {
        //                    x0 = Linelist[fid][0].X;

        //                    y0 = Linelist[fid][0].Y;

        //                    i1 = util.minstra(Linelist[fid], 0, ST, 1);

        //                    for (int j = 0; j < i1; j++)
        //                    {
        //                        line_temp.Add(Linelist[fid][j]);
        //                        sumdis_bar += util.getDistanceOfTwoPoints(Linelist[fid][j], Linelist[fid][j + 1]);
        //                    }
        //                    line_temp.Add(Linelist[fid][i1]);

        //                    i2 = util.minstra(Linelist[tid], pcount2 - 1, ST, -1);

        //                }break;
        //            case 3:
        //                {
        //                    x0 = Linelist[fid][pcount1 - 1].X;

        //                    y0 = Linelist[fid][pcount1 - 1].Y;

        //                    i1 = util.minstra(Linelist[fid], pcount1 - 1, ST, -1);

        //                    for (int j = i1; j < pcount1 - 1; j++)
        //                    {
        //                        line_temp.Add(Linelist[fid][j]);
        //                        sumdis_bar += util.getDistanceOfTwoPoints(Linelist[fid][j], Linelist[fid][j + 1]);
        //                    }
        //                    line_temp.Add(Linelist[fid][pcount1-1]);

        //                    i2 = util.minstra(Linelist[tid], 0, ST, 1);
        //                }
        //                break;
        //            case 4:
        //                {
        //                    x0 = Linelist[i][pcount1 - 1].X;

        //                    y0 = Linelist[i][pcount1 - 1].Y;

        //                    i1 = util.minstra(Linelist[fid], pcount1 - 1, ST, -1);

        //                    for (int j = i1; j < pcount1 - 1 ; j++)
        //                    {
        //                        line_temp.Add(Linelist[fid][j]);
        //                        sumdis_bar += util.getDistanceOfTwoPoints(Linelist[fid][j], Linelist[fid][j + 1]);
        //                    }
        //                    line_temp.Add(Linelist[fid][pcount1-1]);

        //                    i2 = util.minstra(Linelist[tid], pcount2 - 1, ST, -1);

        //                } break;
        //        }

        //        if ((i1 < 0) || (i2 < 0))
        //            continue;

        //        x1 = Linelist[fid][i1].X;  y1 = Linelist[fid][i1].Y;

        //        x2 = Linelist[tid][i2].X;  y2 = Linelist[tid][i2].Y;

        //        double a = x0 - x1;
        //        double b = y0 - y1;
        //        double c = x2 - x0;
        //        double d = y2 - y0;

        //        double n1 = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));

        //        double n2 = Math.Sqrt(Math.Pow(c, 2) + Math.Pow(d, 2));

        //        if ((sumdis_bar < LT))
        //        { continue; }

        //        double ang = Math.Acos((a * c + b * d) / (n1 * n2)) / Math.PI*180;

        //        if ( ang > 85 )
        //        {
        //            double n_x = 0;
        //            double n_y = 100;
        //            double f_x = a;
        //            double f_y = b;

        //            double nl = Math.Sqrt(Math.Pow(n_x, 2) + Math.Pow(n_y, 2));

        //            double fl = Math.Sqrt(Math.Pow(f_x, 2) + Math.Pow(f_y, 2));

        //            double f_ang = 0;

        //            if (f_x > 0)
        //                f_ang = Math.Acos((n_x * f_x + n_y * f_y) / (nl * fl)) / Math.PI * 180;
        //            else
        //                f_ang = 360 - Math.Acos((n_x * f_x + n_y * f_y) / (nl * fl)) / Math.PI * 180;

        //            //整条河流
        //            //Barb_riverlist.Add(Linelist[fid]);

        //            Barb_riverlist.Add(line_temp);

        //            Form1.intersection.Add(ang);

        //            //Form1.yuan_id.Add(f_ang);
        //            Form1.yuan_id.Add(i);
        //            //double tempdis = 0;
        //            //for (int j = 0; j < Linelist[fid].Count()- 1; j++)
        //            //{
        //            //    tempdis += Form1.getDistanceOfTwoPoints(Linelist[fid][j], Linelist[fid][j+1]);
        //            //}

        //            //Form1.length.Add(tempdis);
        //            Form1.length.Add(sumdis_bar);
        //        }

        //    }//for i

        //    return Barb_riverlist;
        //}

    }
}
