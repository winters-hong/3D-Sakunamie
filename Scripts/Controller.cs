using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public enum state // Represent the stages of tree
    {
        seed,
        grow1,
        grow2,
        full_grown
    };
    private state treeState;
    private FlowerController flowerController;
    private WeatherInfoController weatherInfo;
    public GameObject textObj;
    private Text3dControl textCtrl;
    public int playerN;
    public AnimationClip[] treeClips;
    public AnimationClip[] flowerClips;
    public GameObject branches;
    public GameObject flowers;
    public ParticleSystem rain;
    private int indexF;
    private int indexT;
    private int infoIndex;

    // Start is called before the first frame update
    void Start()
    {
        treeState = state.seed;
        playerN = 0;
        indexF = 0;
        indexT = 0;
        infoIndex = 1;
        rain.Stop();
        flowerController = gameObject.GetComponent<FlowerController>();
        weatherInfo = gameObject.GetComponent<WeatherInfoController>();
        textCtrl = textObj.GetComponent<Text3dControl>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardMode();
    }

    private void KeyboardMode()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            WaterIn();
            //flowerController.MoreFlowers();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            flowerController.LessFlowers();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Stick();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TreeGrow();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Scene0");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void WaterIn()
    {
        if (treeState == state.seed)
        {
            TreeGrow();
            treeState++;
        }
        else if (playerN <= 0)
        {
            playerN++;
            flowerController.MorePlayers();
        }
        else if (flowerController.NeedMoreFlower())
        {
            Debug.Log("more flower");
            flowerController.MoreFlowers();
        }
        else if (treeState != state.full_grown)
        {
            TreeGrow();
            treeState++;
        }

        rain.Play();
        StartCoroutine(
            DelayToInvoke.DelayToInvokeDo(
                () =>
                {
                    rain.Stop();
                },
                5f
            )
        );

        textCtrl.ShowPlusInfo(infoIndex++);
        if (infoIndex > 6)
        {
            infoIndex = 1;
        }
    }

    public void Stick()
    {
        if (treeState == state.seed)
        {
            TreeGrow();
            treeState++;
        }
        else if (
            (treeState == state.grow1 && playerN < 2)
            || (treeState == state.grow2 && playerN < 4)
            || (treeState == state.full_grown && playerN < 6)
        )
        {
            playerN++;
            flowerController.MorePlayers();
        }
        else if (treeState == state.full_grown || flowerController.NeedMoreFlower())
        {
            flowerController.MoreFlowers();
        }
    }

    public void TreeGrow()
    {
        if (indexT < treeClips.Length)
        {
            branches.GetComponent<Animation>().clip = treeClips[indexT++];
            branches.GetComponent<Animation>().Play();
        }
        if (treeState != state.seed)
        {
            flowerController.Upgrade();
            if (indexF < flowerClips.Length)
            {
                flowers.GetComponent<Animation>().clip = flowerClips[indexF++];
                flowers.GetComponent<Animation>().Play();
            }
        }
        Debug.Log("Growing");
        textCtrl.MoveUp();
    }

    public state GetTreeState()
    {
        return treeState;
    }
}
