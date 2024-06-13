using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlusInfo : MonoBehaviour
{
    public Text City_Text;
    public Text WeatherDec_Text;
    public Text Temperature_Text;
    public Text WeatherTime_Text;
    public Text WeatherNotice_Text;

    // Start is called before the first frame update
    void Start()
    {
        ShowWeather();
    }

    void ShowWeather()
    {
        PlusInfo.Instance.SetWeatherDataEvent(
            (weatherData) =>
            {
                City_Text.text = weatherData.cityInfo.city;
                WeatherTime_Text.text = weatherData.date;
                WeatherDec_Text.text = weatherData.data.forecast[0].type;
                Temperature_Text.text =
                    weatherData.data.forecast[0].low
                    + "°C"
                    + "~"
                    + weatherData.data.forecast[0].high
                    + "°C";
                WeatherNotice_Text.text = weatherData.data.forecast[0].notice;
            }
        );
    }
}
