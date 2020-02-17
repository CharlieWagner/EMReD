using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    [Header("Type of Target")]
    [Tooltip("0 explodes, 1 charges, 2 bridge?")]
    public int _Type;

    public void GetShot()
    {
        Debug.Log("I was shot");
        if (_Type == 0)
        {
            GetComponent<LaserExplode>().Explode();
        }
    }
}
