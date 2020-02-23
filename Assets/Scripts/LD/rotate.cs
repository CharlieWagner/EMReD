using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    [SerializeField]
    private bool _RotateByDefault = false;
    [SerializeField]
    private float _rotationSpeed;


    public bool _StartRotation = false;

    void Update()
    {
        if (_RotateByDefault)
        {
            transform.Rotate(new Vector3(0, _rotationSpeed * Time.deltaTime, 0));
        } else if (_StartRotation)
        {
            transform.Rotate(new Vector3(0, _rotationSpeed * Time.deltaTime, 0));
        }

        
    }
}
