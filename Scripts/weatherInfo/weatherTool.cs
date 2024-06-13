using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weatherTool
{
    public static Dictionary<string, int> PosToId = new Dictionary<string, int>();

    public static bool initDic = false;

    public static int GetWeatherId(string name)
    {
        int id = 0;
        if(!initDic)
        {
            initDic = true;
            TextAsset ta = Resources.Load<TextAsset>("WeatherId");
            List<Pos2Id> temp = LitJson.JsonMapper.ToObject<List<Pos2Id>>(ta.text);
            foreach(Pos2Id t in temp)
            {
                PosToId[t.placeName] = t.id;
            }
        }
        for(int i=1;i<name.Length;i++)
        {
            string tn = name.Substring(0, i);
            if(PosToId.ContainsKey(tn))
            {
                id = PosToId[tn];
            }
        }
        return id;
    }

    public class Pos2Id
    {
        public string placeName;
        public int id;

        public Pos2Id()
        {

        }
        public Pos2Id(string name,int id)
        {
            placeName = name;
            this.id = id;
        }
    }

    public class WeathBody
    {
        public string time;
        public CityInfo cityInfo;
        public string date;
        public string message;
        public int status;
        public WeathData data;
    }

    public class CityInfo
    {
        public string city;
        public string cityId;
        public string parent;
        public string updateTime;
    }

    public class WeathData
    {
        public string shidu;
        public double pm25;
        public double pm10;
        public string quality;
        public string wendu;
        public string ganmao;
        public WeathDetailData yesterday;
        public WeathDetailData[] forecast;
    }

    public class WeathDetailData
    {
        public string date;
        public string sunrise;
        public string high;
        public string low;
        public string sunset;
        public double aqi;
        public string ymd;
        public string week;
        public string fx;
        public string fl;
        public string type;
        public string notice;
    }

}
