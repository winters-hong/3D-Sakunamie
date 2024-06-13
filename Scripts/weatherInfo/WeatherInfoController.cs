using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherInfoController : MonoBehaviour
{
    public Text cityText;
    public Text weatherText;
    public Text tempText;
    public Text noticeText;
    public Text dateText;

    public string cityInfo;
    public string weatherInfo;
    public string tempInfo;
    public string noticeInfo;
    public string dateInfo;

    /// <summary>
    /// Access location info from IP address
    /// </summary>
    string Posurl =
        "http://api.map.baidu.com/location/ip?ak=wHiiAx78NfeHj69MpsXgzberonXEFBhM&coor=bd09ll";

    /// <summary>
    /// Get weather info
    /// </summary>
    string Weatherurl = "http://t.weather.sojson.com/api/weather/city/";

    void Start()
    {
        //Request for location info
        StartCoroutine(RequestPos());
    }

    IEnumerator RequestPos()
    {
        WWW www = new WWW(Posurl);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            positionTool.ResponseBody t = LitJson.JsonMapper.ToObject<positionTool.ResponseBody>(
                www.text
            );
            Debug.Log(t.content.address_detail.city);

            //Attach weather info to string var
            //cityText.text = t.content.address_detail.city;
            cityInfo = t.content.address_detail.city;
            StartCoroutine(RequestWeather(weatherTool.GetWeatherId(t.content.address_detail.city)));
        }
    }

    IEnumerator RequestWeather(int id)
    {
        WWW www = new WWW(Weatherurl + id.ToString());
        //Debug.Log(Weatherurl + id.ToString());
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            //Debug.Log(www.text);
            weatherTool.WeathBody t = LitJson.JsonMapper.ToObject<weatherTool.WeathBody>(www.text);
            Debug.Log(t.data.forecast[0].type);

            // dateText.text = t.date;
            // weatherText.text = t.data.forecast[0].type;
            // tempText.text = t.data.forecast[0].low + "°C" + "~" + t.data.forecast[0].high + "°C";
            // noticeText.text = t.data.forecast[0].notice;

            dateInfo = t.date;
            weatherInfo = t.data.forecast[0].type;
            tempInfo = t.data.forecast[0].low + "°C" + "~" + t.data.forecast[0].high + "°C";
            noticeInfo = t.data.forecast[0].notice;
        }
    }
}
