using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public enum State_Boss03 {
    Attack1=0,
    Attack2=1,
    Attack3=2,
    Attack4=3,
    Move=4,
    Idle=5,
}
public class Boss03Info
{
    static int[][][] Boss03RateInfo = new int[3][][]{
        //hp>75%
        new int [6][]{
        new int[]{ 30,20,-1,30,0,20 },  //移动字典
        new int[] {10,30,-1,30,20,10 }, //动作1字典
        new int[] {30,10,-1,30,20,10 }, //动作2字典
        new int[] {-1,-1,-1,-1,-1,-1 }, //动作3字典
        new int[] {20,30,-1,10,30,10 }, //动作4字典
        new int[] {30,20,-1,30,20,-1 }, //待机字典
        },
        //30%<hp<75%
        new int [6][]{
        new int[]{ 30,20,15,25,0,10 },  //移动字典
        new int[] {10,20,20,25,20,5 }, //动作1字典
        new int[] {20,10,20,25,20,5 }, //动作2字典
        new int[] {20,20,10,25,20,5 }, //动作3字典
        new int[] {20,20,20,15,20,5 }, //动作4字典
        new int[] {20,20,20,20,20,-1 }, //待机字典
        },
        //hp<30%
        new int [6][]{
        new int[]{ 25,20,25,25,-1,5 },  //移动字典
        new int[] {10,15,25,25,20,5 }, //动作1字典
        new int[] {20,15,25,25,10,5 }, //动作2字典
        new int[] {20,25,5,20,25,5 }, //动作3字典
        new int[] {20,20,25,10,20,5 }, //动作4字典
        new int[] {20,20,25,25,10,-1 }, //待机字典
        }
    };

    ////////////////////程序用到的Float几率表//////////////////////
    public static Dictionary<State_Boss03, float> moveRateTable=new Dictionary<State_Boss03, float> ();
    public static Dictionary<State_Boss03, float> attack01RateTable=new Dictionary<State_Boss03, float> ();
    public static Dictionary<State_Boss03, float> attack02RateTable = new Dictionary<State_Boss03, float>();
    public static Dictionary<State_Boss03, float> attack03RateTable = new Dictionary<State_Boss03, float>();
    public static Dictionary<State_Boss03, float> attack0Rate4Table = new Dictionary<State_Boss03, float>();
    public static Dictionary<State_Boss03, float> idleTable = new Dictionary<State_Boss03, float>();


    ///////////////////初始化int值的字典////////////////////////////
    public static void InitDictionary(int hpLevel = 0)
    {
        int currentRate=0;
        List<Dictionary<State_Boss03, float>> ListOfSortedRateTabel = new List<Dictionary<State_Boss03, float>>();
        ListOfSortedRateTabel.Add(moveRateTable);
        ListOfSortedRateTabel.Add(attack01RateTable);
        ListOfSortedRateTabel.Add(attack02RateTable);
        ListOfSortedRateTabel.Add(attack03RateTable);
        ListOfSortedRateTabel.Add(attack0Rate4Table);
        ListOfSortedRateTabel.Add(idleTable);

        //清空
        foreach (var item in ListOfSortedRateTabel)
        {
            item.Clear();
        }

        Dictionary<State_Boss03, int>[] originalArray = new Dictionary<State_Boss03, int>[6] {
            new Dictionary<State_Boss03, int>(),
            new Dictionary<State_Boss03, int>(),
            new Dictionary<State_Boss03, int>(),
            new Dictionary<State_Boss03, int>(),
            new Dictionary<State_Boss03, int>(),
            new Dictionary<State_Boss03, int>()
    };

        //初始化字典(未排序，int)
        for (int i = 0; i < originalArray.Length; i++)
        {
            for (int j = 0; j < Boss03RateInfo[hpLevel][i].Length; j++)
            {
                originalArray[i].Add((State_Boss03)j, Boss03RateInfo[hpLevel][i][j]);
            }
        }

        //排序，int->float
        for (int i = 0; i < originalArray.Length; i++)
        {
            var dicSort = from objDic in originalArray[i]
                          where objDic.Value > 0
                          orderby objDic.Value ascending
                          select objDic;
            foreach (KeyValuePair<State_Boss03 ,int> kvp in dicSort)
            {
                currentRate += kvp.Value;
                ListOfSortedRateTabel[i].Add(kvp.Key, currentRate / 100f);
            }
            currentRate = 0;
        }
    }
   
}
