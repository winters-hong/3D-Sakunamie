using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchGenerator : MonoBehaviour
{
    public GameObject[] Trees;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        Trees[0].SetActive(true);
        for(int i=1; i< Trees.Length; i++)
        {
            Trees[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && index<Trees.Length)
        {
            Trees[index++].SetActive(false);
            Trees[index].SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) && index>=0)
        {
            Trees[index--].SetActive(false);
            Trees[index].SetActive(true);
        }
    }
}
