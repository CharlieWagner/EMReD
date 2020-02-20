using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    private Vector3 _previousPos;
    private Vector3 _previousEulerRot;

    private Vector3 _PosOffset;
    private Vector3 _RotOffset;


    // Update is called once per frame
    void Update()
    {
        _PosOffset = transform.position - _previousPos;
        _RotOffset = transform.rotation.eulerAngles - _previousEulerRot;

        _previousPos = transform.position;
        _previousEulerRot = transform.rotation.eulerAngles;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.position.y >= (transform.position.y + (transform.lossyScale.y/2)*.9f))
        {
            collision.transform.position += _PosOffset;
            collision.transform.Rotate(_RotOffset);
            Debug.Log("top");
        } else
        {
            Debug.Log("Else");
        }

        //collision.transform.position += 
    }
}
