using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 获取对应城市的天�?
/// 注意：温度时摄氏度°C
/// </summary>
public class PlusInfo : MonoSingleton<PlusInfo>
{
    /// <summary>
    /// 获取位置信息
    /// </summary>
    string PostionUrl =
        "http://api.map.baidu.com/location/ip?ak=wHiiAx78NfeHj69MpsXgzberonXEFBhM&coor=bd09ll";

    /// <summary>
    /// 获取天气
    /// </summary>
    string WeatherUrl = "http://t.weather.sojson.com/api/weather/city/";

    Action<WeatherDataStruct.WeathBody> WeatherDataEvent;

    // 多少秒后更新一次数�?
    private float updateWeatherTime = 10;

    public float UpdateWeatherTime
    {
        get => updateWeatherTime;
        set => updateWeatherTime = value;
    }

    void Start()
    {
        //获取位置
        StartCoroutine(RequestPos());
    }

    /// <summary>
    /// 设置天气�?据的监听事�?
    /// </summary>
    /// <param name="WeatherDataAction"></param>
    public void SetWeatherDataEvent(Action<WeatherDataStruct.WeathBody> WeatherDataAction)
    {
        WeatherDataEvent = WeatherDataAction;
    }

    /// <summary>
    /// 获取当前所在城�?
    /// </summary>
    /// <returns></returns>
    IEnumerator RequestPos()
    {
        WWW www = new WWW(PostionUrl);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            PositionDataStruct.ResponseBody t =
                LitJson.JsonMapper.ToObject<PositionDataStruct.ResponseBody>(www.text);
            Debug.Log(t.content.address_detail.city);
            //获取天气
            StartCoroutine(
                RequestWeather(WeatherDataStruct.GetWeatherId(t.content.address_detail.city))
            );
        }
    }

    /// <summary>
    /// ?��取当前所在城市的天气数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    IEnumerator RequestWeather(int id)
    {
        yield return new WaitForEndOfFrame();

        while (true)
        {
            WWW www = new WWW(WeatherUrl + id.ToString());
            Debug.Log(WeatherUrl + id.ToString());
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.text);
                WeatherDataStruct.WeathBody t =
                    LitJson.JsonMapper.ToObject<WeatherDataStruct.WeathBody>(www.text);
                Debug.Log(t.data.forecast[0].notice);
                if (WeatherDataEvent != null)
                {
                    for (int i = 0; i < t.data.forecast.Length; i++)
                    {
                        // 正则表达式获取温度数�?
                        string low = System.Text.RegularExpressions.Regex.Replace(
                            t.data.forecast[i].low,
                            @"[^0-9]+",
                            ""
                        );
                        string high = System.Text.RegularExpressions.Regex.Replace(
                            t.data.forecast[i].high,
                            @"[^0-9]+",
                            ""
                        );
                        t.data.forecast[i].low = low;
                        t.data.forecast[i].high = high;
                    }

                    WeatherDataEvent.Invoke(t);
                }
            }

            yield return new WaitForSeconds(updateWeatherTime);
        }
    }
}
