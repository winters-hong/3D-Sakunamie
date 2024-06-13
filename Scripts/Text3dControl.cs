using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text3dControl : MonoBehaviour
{
    public TextMesh thisText;
    private int index = 0;
    private Vector3[] pos = {new Vector3(0f, 12f,0f),new Vector3(0f, 20f, 0f),new Vector3(0f, 22f, 0f)};
    public GameObject controllerObj;
    private WeatherInfoController weather;
    // Start is called before the first frame update
    void Start()
    {
        weather = controllerObj.GetComponent<WeatherInfoController>();
        thisText = gameObject.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtCam();
    }

    public void LookAtCam()
    {
        transform.forward =
            new Vector3(transform.position.x, 0, transform.position.z)
            - new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
    }
        public void ShowPlusInfo(int index)
    {
        if (index == 1)
        {
            UpdateText3d("欢迎来到" + weather.cityInfo);
        }
        else if(index == 3)
        {
            UpdateText3d("今日的" + weather.cityInfo +", " + weather.tempInfo);
        }
        else if(index == 6)
        {
            UpdateText3d("今天天气" + weather.weatherInfo+", " + weather.noticeInfo);
        }
        else return;

        gameObject.SetActive(true);
        StartCoroutine(
                DelayToInvoke.DelayToInvokeDo(
                    () =>
                    {
                        gameObject.SetActive(false);
                    },
                    6f
                )
            );
    }
    public void UpdateText3d(string new_text)
    {
        thisText.text = new_text;
    }
    public void MoveUp()
    {
        transform.position = pos[index++];
    }
}
