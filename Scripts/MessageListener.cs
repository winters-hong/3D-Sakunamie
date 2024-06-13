using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageListener : MonoBehaviour
{
    public GameObject gameCtrler;
    public Controller controller;
    private bool listenWater;
    private bool listenStick;
    // Start is called before the first frame update
    void Start()
    {
        listenWater = true;
        listenStick = true;
        controller = gameCtrler.GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update() { }

    //Invoked when a line of data is received from the serial device
    void OnMessageArrived(string messages)
    {
        //Debug.Log(messages);
        string[] msg = messages.Split('#');
        if (listenWater && (msg[0] == "drown" || msg[0] == "water"))
        {
            Debug.Log(msg[0]);
            controller.WaterIn();
            listenWater = false;
            StartCoroutine(
                DelayToInvoke.DelayToInvokeDo(
                    () =>
                    {
                        listenWater = true;
                    },
                    10f
                )
            );
        }
        if (listenStick && msg[1] == "d")
        {
            controller.Stick();
            listenStick = false;
            StartCoroutine(
                DelayToInvoke.DelayToInvokeDo(
                    () =>
                    {
                        listenStick = true;
                    },
                    10f
                )
            );
        }
    }

    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}
