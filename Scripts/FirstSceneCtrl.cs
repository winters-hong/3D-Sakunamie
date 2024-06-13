using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstSceneCtrl : MonoBehaviour
{
    public GameObject shocker;
    public GameObject clock;
    private ClockControl clockCtrler;
    public ParticleSystem strikeWave;
    public ParticleSystem[] storms;
    public AnimationClip[] striking;

    public Canvas mask;

    public float time;
    private bool lighten;
    private bool strike;
    public bool countBegin;
    // Start is called before the first frame update
    void Start()
    {
        clockCtrler = clock.GetComponent<ClockControl>();
        time = 0f;
        lighten = false;
        strike = false;
        countBegin = true;
        for (int i = 0; i < storms.Length; i++)
        {
            storms[i].Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LightingStorm();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Striking();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            countBegin = !countBegin;
            time = 0f;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Scene0");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (countBegin)
        {
            TimeCount();
        }
    }
    public void TimeCount()
    {
        time += Time.deltaTime;
        if (!lighten && time >= 12f)
        {
            lighten = true;
            LightingStorm();
        }
        if (!strike && time >= 16f)
        {
            strike = true;
            Striking();
        }
    }
    public void LightingStorm()
    {
        for (int i = 0; i < storms.Length; i++)
        {
            storms[i].Play();
        }
    }
    public void Striking()
    {
        clockCtrler.isPause = true;
        Debug.Log(clockCtrler.GetSkybox_X());
        strikeWave.Play();
        shocker.GetComponent<Animation>().clip = striking[0];
        shocker.GetComponent<Animation>().Play();
        StartCoroutine(
            DelayToInvoke.DelayToInvokeDo(
                () =>
                {
                    mask.GetComponent<Animation>().clip = striking[1];
                    mask.GetComponent<Animation>().Play();
                    StartCoroutine(
                        DelayToInvoke.DelayToInvokeDo(
                            () =>
                            {
                                SceneManager.LoadScene("Scene1");
                            },
                            3f
                        )
                    );
                },
                5f
            )
        );
    }
}
