using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserExplode : MonoBehaviour
{
    public Rigidbody[] _SubRocks;
    public float ExplosionForce;

    public void Explode()
    {
        for (int i = 0; i <= _SubRocks.Length - 1; i++)
        {
            

            _SubRocks[i].isKinematic = false;

            _SubRocks[i].AddForce((_SubRocks[i].transform.position - transform.position) * ExplosionForce);

            _SubRocks[i].transform.parent = null;
        }



        Debug.Log("I go boom");

        Destroy(gameObject);
    }
}
