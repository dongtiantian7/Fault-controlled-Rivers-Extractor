using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace test
{
    class extract_river
    {
    //    public static List<SpecRiver> StrRiver = new List<SpecRiver>();
    //    public static List<SpecRiver> BarbRiver = new List<SpecRiver>();
    //    public static List<SpecRiver> RigRiver = new List<SpecRiver>();
    //    public static List<SpecRiver> CounRiver = new List<SpecRiver>();

    //    public static void extractRiver(List<List<IPoint>> Linelist, List<List<IPoint>> arg_point, List<ArgPoint> ListArgPoint, List<ArgLine> ListArgLine, List<GraphStruct> Graph, double AT, double LT, double ST)
    //    {
    //        //识别直线河流
    //        for (int i = 0; i < ListArgPoint.Count; i++)
    //        {
    //            //若该河段过短
    //            if (ListArgPoint[i].Length < LT)
    //                continue;
    //            ////若整条河流均为直线
    //            //if ((i == 0) && (ListArgPoint[i].RiverID != ListArgPoint[i + 1].RiverID))
    //            //{
    //            //    SpecRiver temRiver = new SpecRiver(ListArgPoint[i].CurbS,ListArgPoint[i].Length, ListArgPoint[i].RiverID);
    //            //    temRiver.RiverLine = Linelist[i];
    //            //}

    //            ////若整条河流均为直线
    //            //if ((i == ListArgPoint.Count-1) && (ListArgPoint[i].RiverID != ListArgPoint[i - 1].RiverID))
    //            //{
    //            //    SpecRiver temRiver = new SpecRiver(ListArgPoint[i].CurbS, ListArgPoint[i].Length, ListArgPoint[i].RiverID);
    //            //    temRiver.RiverLine = Linelist[i];
    //            //}

    //            int rid = ListArgPoint[i].RiverID;

    //            //若整条河流均为直线
    //            if ((ListArgPoint[i].BPointId == 0) && (ListArgPoint[i].DPointId == Linelist[rid].Count - 1))
    //            {
    //                SpecRiver temRiver = new SpecRiver(ListArgPoint[i].CurbS, ListArgPoint[i].Length, ListArgPoint[i].RiverID);
    //                temRiver.RiverLine = Linelist[rid];
    //                StrRiver.Add(temRiver);
    //            }
    //            else
    //            {
    //                if ((ListArgPoint[i].CurbS < 1.05) || (ListArgPoint[i].CurbS > 3000))
    //                {
    //                    SpecRiver temRiver = new SpecRiver(ListArgPoint[i].CurbS, ListArgPoint[i].Length, ListArgPoint[i].RiverID);
    //                    temRiver.RiverLine = PartLine(Linelist[rid], ListArgPoint[i].BPointId, ListArgPoint[i].DPointId);
    //                    StrRiver.Add(temRiver);
    //                }
    //            }
    //        }//for (int i = 0; i < ListArgPoint.Count; i++)

    //        for (int i = 0; i < Graph.Count; i++)
    //        {
    //            //边的端点序号
    //            int cid1 = Graph[i].FromPID;
    //            int cid2 = Graph[i].ToPID;

    //            //是否是河流转弯
    //            if (ListArgPoint[cid1].RiverID == ListArgPoint[cid2].RiverID)
    //            {
    //                double turnAngle = Graph[i].ALine.MidAngle;

    //                if (Math.Abs(turnAngle - 90) < AT)
    //                {
    //                    //直角转弯河段的长度
    //                    double turnLength = Graph[i].ALine.MidLength + ListArgPoint[cid1].Length + ListArgPoint[cid2].Length;
    //                    int rid = ListArgPoint[cid1].RiverID;

    //                    //存储直角转弯河段的 角度、长度、河流ID
    //                    SpecRiver temRiver = new SpecRiver(turnAngle, turnLength, ListArgPoint[cid1].RiverID);
    //                    temRiver.RiverLine = PartLine(Linelist[rid], ListArgPoint[cid1].BPointId, ListArgPoint[cid2].DPointId);
    //                    RigRiver.Add(temRiver);
    //                }
    //            }
    //            else
    //            { }

    //            //if (Entity.arg_line[i].Count < 3)
    //            //{
    //            //    if (Math.Abs(turn_angle[i] - 90) < AT)
    //            //    {
    //            //        int riverid = (int)Entity.priver_id[cid1];

    //            //        if (util.cut_rightpart(Linelist[riverid], (int)Entity.bpoint_id[cid1], (int)Entity.bpoint_id[cid2], ST) < 0) continue;

    //            //        Entity.turn_angle.Add(turn_angle[i]);
    //            //        Entity.tur_yuan_id.Add(cid1);
    //            //    }
    //            //}
    //            //else
    //            //{
    //            //    int cidt = (int)Entity.lturn_angle[i];

    //            //    double at = util.neariver_ang(Linelist[cid1], Linelist[cid2], ST, LT);
    //            //    if (Math.Abs(at - 180) < AT)
    //            //    {
    //            //        Entity.Counterpart_list.Add(Linelist[cid1]);
    //            //        Entity.Coun_turn_angle.Add(at);
    //            //        Entity.cou_length.Add(util.sumdis_line(Linelist[cid1], 0, Linelist[cid1].Count - 1));
    //            //        Entity.cou_yuan_id.Add(cid1);

    //            //        Entity.Counterpart_list.Add(Linelist[cid2]);
    //            //        Entity.Coun_turn_angle.Add(at);
    //            //        Entity.cou_length.Add(util.sumdis_line(Linelist[cid2], 0, Linelist[cid2].Count - 1));
    //            //        Entity.cou_yuan_id.Add(cid2);
    //            //    }
    //            //    else
    //            //    {
    //            //        double a1 = util.neariver_ang(Linelist[cid1], Linelist[cidt], ST, LT);
    //            //        double a2 = util.neariver_ang(Linelist[cid2], Linelist[cidt], ST, LT);

    //            //        if ((180 - a1 > 85) && (Math.Abs(a2 - 180) < AT))
    //            //        {
    //            //            Entity.Barb_riverline_list.Add(Linelist[cid1]);
    //            //            Entity.inter_angle.Add(180 - a1);
    //            //            Entity.bar_length.Add(util.sumdis_line(Linelist[cid1], 0, Linelist[cid1].Count - 1));
    //            //            Entity.bar_yuan_id.Add(cid1);
    //            //        }

    //            //        if ((180 - a2 > 85) && (Math.Abs(a1 - 180) < AT))
    //            //        {
    //            //            Entity.Barb_riverline_list.Add(Linelist[cid2]);
    //            //            Entity.inter_angle.Add(180 - a2);
    //            //            Entity.bar_length.Add(util.sumdis_line(Linelist[cid2], 0, Linelist[cid2].Count - 1));
    //            //            Entity.bar_yuan_id.Add(cid2);
    //            //        }
    //            //    }//else
    //            //}



    //            //switch (util.rela(toid, id1, id2))
    //            //{
    //            //    case 1:
    //            //        {
    //            //            if (Math.Abs(turn_angle[i] - 90) < AT)
    //            //            {
    //            //                util.cut_rightpart(Linelist[id1], id1, AT, LT, ST, MD);
    //            //            }
    //            //        } break;
    //            //    case 2:
    //            //        {
    //            //            if (Math.Abs(turn_angle[i] - 180) < AT)
    //            //            {
    //            //                Entity.Counterpart_list.Add(Linelist[id1]);
    //            //                Entity.Coun_turn_angle.Add(turn_angle[i]);
    //            //                Entity.cou_length.Add(util.sumdis_line(Linelist[id1], 0, Linelist[id1].Count-1));
    //            //                Entity.cou_yuan_id.Add(id1);

    //            //                Entity.Counterpart_list.Add(Linelist[id2]);
    //            //                Entity.Coun_turn_angle.Add(turn_angle[i]);
    //            //                Entity.cou_length.Add(util.sumdis_line(Linelist[id2], 0, Linelist[id2].Count - 1));
    //            //                Entity.cou_yuan_id.Add(id2);
    //            //            }
    //            //        }
    //            //        break;
    //            //    case 3:
    //            //        {
    //            //            if (180 - turn_angle[i] > 85)
    //            //            {
    //            //                Entity.Barb_riverline_list.Add(Linelist[id1]);
    //            //                Entity.inter_angle.Add(turn_angle[i]);
    //            //                Entity.bar_length.Add(util.sumdis_line(Linelist[id1], 0, Linelist[id1].Count - 1));
    //            //                Entity.bar_yuan_id.Add(id1);
    //            //            }
    //            //        }
    //            //        break;
    //            //    case 4:
    //            //        {
    //            //            if (180 - turn_angle[i] > 85)
    //            //            {
    //            //                Entity.Barb_riverline_list.Add(Linelist[id2]);
    //            //                Entity.inter_angle.Add(turn_angle[i]);
    //            //                Entity.bar_length.Add(util.sumdis_line(Linelist[id2], 0, Linelist[id2].Count - 1));
    //            //                Entity.bar_yuan_id.Add(id2);
    //            //            }
    //            //        }
    //            //        break;
    //            //    // default:  ;
    //            //}//switch



    //        }//for
    //    }

    //    //public static void extract(List<List<IPoint>> Linelist, List<List<IPoint>> arg_point,List<List<IPoint>> arg_line, List<double> friver_id, List<double> triver_id, List<double> turn_angle, double AT, double LT, double ST)
    //    //{
    //    //    int line_count = arg_line.Count;

    //    //    int k = 0;

    //    //    //识别直线河流
    //    //    for (int i = 0; i < arg_point.Count; i++)
    //    //    {
    //    //        for (int j = 0; j < arg_point[i].Count; j++,k++)
    //    //        {
    //    //            //若该河段过短
    //    //            if (Entity.sstr_length[k] < LT)
    //    //                continue;
                    
    //    //            //若整条河流均为直线
    //    //            if (arg_point[i].Count == 1)
    //    //            {
    //    //                Entity.straight_line.Add(Linelist[i]);
    //    //                Entity.curb_S.Add(Entity.sstr_curb[k]);
    //    //                Entity.strlength.Add(Entity.sstr_length[k]);
    //    //                Entity.str_yuan_id.Add(i);
    //    //            }
    //    //            else
    //    //            {
    //    //                if ((Entity.sstr_curb[k] < 1.05) || (Entity.sstr_length[k] > 3000))
    //    //                {
    //    //                    util.cut_straight(Linelist[i], (int)Entity.bpoint_id[k], LT, ST);
    //    //                    Entity.str_yuan_id.Add(i);
    //    //                }
    //    //            }
    //    //        }
    //    //    }

    //    //    for (int i = 0; i < line_count; i++)
    //    //    {
    //    //        int cid1 = (int)Entity.friver_id[i];
    //    //        int cid2 = (int)Entity.triver_id[i];

    //    //        if (Entity.arg_line[i].Count < 3)
    //    //        {
    //    //            if (Math.Abs(turn_angle[i] - 90) < AT)
    //    //            {
    //    //                int riverid = (int)Entity.priver_id[cid1];

    //    //                if (util.cut_rightpart(Linelist[riverid], (int)Entity.bpoint_id[cid1], (int)Entity.bpoint_id[cid2], ST) < 0) continue;

    //    //                Entity.turn_angle.Add(turn_angle[i]);
    //    //                Entity.tur_yuan_id.Add(cid1);
    //    //            }
    //    //         }
    //    //        else
    //    //        {
    //    //            int cidt = (int)Entity.lturn_angle[i];

    //    //            double at = util.neariver_ang(Linelist[cid1], Linelist[cid2], ST, LT);
    //    //            if (Math.Abs(at - 180) < AT)
    //    //            {
    //    //                Entity.Counterpart_list.Add(Linelist[cid1]);
    //    //                Entity.Coun_turn_angle.Add(at);
    //    //                Entity.cou_length.Add(util.sumdis_line(Linelist[cid1], 0, Linelist[cid1].Count - 1));
    //    //                Entity.cou_yuan_id.Add(cid1);

    //    //                Entity.Counterpart_list.Add(Linelist[cid2]);
    //    //                Entity.Coun_turn_angle.Add(at);
    //    //                Entity.cou_length.Add(util.sumdis_line(Linelist[cid2], 0, Linelist[cid2].Count - 1));
    //    //                Entity.cou_yuan_id.Add(cid2);
    //    //            }
    //    //            else
    //    //            {
    //    //                double a1 = util.neariver_ang(Linelist[cid1], Linelist[cidt], ST, LT);
    //    //                double a2 = util.neariver_ang(Linelist[cid2], Linelist[cidt], ST, LT);

    //    //                if ((180 - a1 > 85) && (Math.Abs(a2 - 180) < AT))
    //    //                {
    //    //                    Entity.Barb_riverline_list.Add(Linelist[cid1]);
    //    //                    Entity.inter_angle.Add(180 - a1);
    //    //                    Entity.bar_length.Add(util.sumdis_line(Linelist[cid1], 0, Linelist[cid1].Count - 1));
    //    //                    Entity.bar_yuan_id.Add(cid1);
    //    //                }

    //    //                if ((180 - a2 > 85) && (Math.Abs(a1 - 180) < AT))
    //    //                {
    //    //                    Entity.Barb_riverline_list.Add(Linelist[cid2]);
    //    //                    Entity.inter_angle.Add(180 - a2);
    //    //                    Entity.bar_length.Add(util.sumdis_line(Linelist[cid2], 0, Linelist[cid2].Count - 1));
    //    //                    Entity.bar_yuan_id.Add(cid2);
    //    //                }
    //    //            }//else
    //    //        }

    //    //        //switch (util.rela(toid, id1, id2))
    //    //        //{
    //    //        //    case 1:
    //    //        //        {
    //    //        //            if (Math.Abs(turn_angle[i] - 90) < AT)
    //    //        //            {
    //    //        //                util.cut_rightpart(Linelist[id1], id1, AT, LT, ST, MD);
    //    //        //            }
    //    //        //        } break;
    //    //        //    case 2:
    //    //        //        {
    //    //        //            if (Math.Abs(turn_angle[i] - 180) < AT)
    //    //        //            {
    //    //        //                Entity.Counterpart_list.Add(Linelist[id1]);
    //    //        //                Entity.Coun_turn_angle.Add(turn_angle[i]);
    //    //        //                Entity.cou_length.Add(util.sumdis_line(Linelist[id1], 0, Linelist[id1].Count-1));
    //    //        //                Entity.cou_yuan_id.Add(id1);

    //    //        //                Entity.Counterpart_list.Add(Linelist[id2]);
    //    //        //                Entity.Coun_turn_angle.Add(turn_angle[i]);
    //    //        //                Entity.cou_length.Add(util.sumdis_line(Linelist[id2], 0, Linelist[id2].Count - 1));
    //    //        //                Entity.cou_yuan_id.Add(id2);
    //    //        //            }
    //    //        //        }
    //    //        //        break;
    //    //        //    case 3:
    //    //        //        {
    //    //        //            if (180 - turn_angle[i] > 85)
    //    //        //            {
    //    //        //                Entity.Barb_riverline_list.Add(Linelist[id1]);
    //    //        //                Entity.inter_angle.Add(turn_angle[i]);
    //    //        //                Entity.bar_length.Add(util.sumdis_line(Linelist[id1], 0, Linelist[id1].Count - 1));
    //    //        //                Entity.bar_yuan_id.Add(id1);
    //    //        //            }
    //    //        //        }
    //    //        //        break;
    //    //        //    case 4:
    //    //        //        {
    //    //        //            if (180 - turn_angle[i] > 85)
    //    //        //            {
    //    //        //                Entity.Barb_riverline_list.Add(Linelist[id2]);
    //    //        //                Entity.inter_angle.Add(turn_angle[i]);
    //    //        //                Entity.bar_length.Add(util.sumdis_line(Linelist[id2], 0, Linelist[id2].Count - 1));
    //    //        //                Entity.bar_yuan_id.Add(id2);
    //    //        //            }
    //    //        //        }
    //    //        //        break;
    //    //        //    // default:  ;
    //    //        //}//switch



    //    //    }//for

                
    //    //}

    //    /// <summary>
    //    /// 在某完整河段中生成指定起点的直线河段
    //    /// </summary>
    //    /// <param name="river">完整河段</param>
    //    /// <param name="bid">直线河段起点</param>
    //    /// <param name="LT"></param>
    //    /// <param name="ST"></param>
    //    //public static void cut_straight(List<IPoint> river, int bid, double LT, double ST)
    //    //{
    //    //    List<IPoint> str_temp = new List<IPoint>();
    //    //    double strdis = 0, S = 0;

    //    //    int did = util.minstra(river, bid, ST, 1);

    //    //    for (int i = bid; i < did; i++)
    //    //    {
    //    //        str_temp.Add(river[i]);
    //    //        strdis += util.getDistanceOfTwoPoints(river[i], river[i + 1]);
    //    //    }

    //    //    S = strdis / util.getDistanceOfTwoPoints(river[bid], river[did]);

    //    //    Entity.straight_line.Add(str_temp);
    //    //    Entity.curb_S.Add(S);
    //    //    Entity.strlength.Add(strdis);
    //    //}



    }
}
