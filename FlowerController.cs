using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    public ParticleSystem[] flowerSpots;
    public Controller controller;

    public float maxRate;
    public float minRate;
    public float rateStep;
    public float rateNow;
    // Start is called before the first frame update
    void Start()
    {
        maxRate = 2f;
        minRate = 2f;
        rateStep = 0f;
        controller = gameObject.GetComponent<Controller>();
        for (int i = 0; i < flowerSpots.Length; i++)
        {
            var em = flowerSpots[i].emission;
            em.rateOverTime = minRate;
            flowerSpots[i].Stop();
        }
    }

    // Update is called once per frame
    void Update() { }

    public void MoreFlowers()
    {
        for (int i = 0; i < flowerSpots.Length; i++)
        {
            var em = flowerSpots[i].emission;
            var rate = em.rateOverTime.constant;
            var shape = flowerSpots[i].shape;
            if (flowerSpots[i].isPlaying && rate + rateStep < maxRate)
            {
                em.rateOverTime = rate + rateStep;
                shape.radius += 0.1f;
            }
        }
        //Debug.Log(flowerSpots[0].emission.rateOverTime.constant);
    }
    public void LessFlowers()
    {
        for (int i = 0; i < flowerSpots.Length; i++)
        {
            var em = flowerSpots[i].emission;
            var rate = em.rateOverTime.constant;
            if (flowerSpots[i].isPlaying && rate - rateStep >= minRate)
                em.rateOverTime = rate - rateStep;
        }
    }
    public void MorePlayers()
    {
        for (int i = 0; i < flowerSpots.Length; i++)
        {
            if (!flowerSpots[i].isPlaying)
            {
                flowerSpots[i].Play();
                //Debug.Log("More players!");
                return;
            }
        }
        //Debug.Log("Max players!");
    }
    public bool NeedMoreFlower()
    {
        for (int i = 0; i < flowerSpots.Length; i++)
        {
            if (
                flowerSpots[i].isPlaying
                && flowerSpots[i].emission.rateOverTime.constant + rateStep < maxRate
            )
            {
                rateNow = flowerSpots[i].emission.rateOverTime.constant;
                Debug.Log(i + " (" + rateNow + "): Need more flowers");
                return true;
            }
        }
        Debug.Log("Dont need flower");
        return false;
    }
    public void Upgrade()
    {
        maxRate += 10f;
        rateStep += 5f;
    }
}
