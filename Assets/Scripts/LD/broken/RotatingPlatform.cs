using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{

    //private Vector3 _previousPos;
    private Vector3 _previousEulerRot;

    //private Vector3 _PosOffset;
    private Vector3 _RotOffset;

    public Transform _Center;
    public float _Speed;

    // Update is called once per frame
    void Update()
    {
        //_PosOffset = transform.position - _previousPos;
        _RotOffset = transform.rotation.eulerAngles - _previousEulerRot;

        //_previousPos = transform.position;
        _previousEulerRot = transform.rotation.eulerAngles;
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.position.y >= (transform.position.y + (transform.lossyScale.y/2)*.9f))
        {
            //collision.transform.position += _PosOffset;
            //collision.transform.Rotate(_RotOffset);

            collision.transform.RotateAround(_Center.position, transform.up, _Speed * Time.deltaTime);
            Debug.Log("top");
        } else
        {
            Debug.Log("Else");
        }

        //collision.transform.position += 
    }
}
