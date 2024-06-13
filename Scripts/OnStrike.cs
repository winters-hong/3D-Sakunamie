using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStrike : MonoBehaviour
{
    private ParticleSystem itself;
    void OnCollisionEnter(Collision coll)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        //Debug.Log("Enter " + coll.gameObject.name);
        gameObject.GetComponent<ParticleSystem>().Stop();
    }
}
