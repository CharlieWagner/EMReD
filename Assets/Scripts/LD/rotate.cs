using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    [SerializeField]
    private bool _RotateByDefault = false;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private float _accelSpeed = 1;

    public float _CurrentRotationSpeed = 0f;


    public bool _StartRotation = false;

    void Update()
    {
        if (_RotateByDefault)
        {
            transform.Rotate(new Vector3(0, _rotationSpeed * Time.deltaTime, 0));
        } else if (_StartRotation)
        {
            _CurrentRotationSpeed += Mathf.Sign(_rotationSpeed) * Time.deltaTime * _accelSpeed;
            _CurrentRotationSpeed = Mathf.Clamp(_CurrentRotationSpeed,-Mathf.Abs(_rotationSpeed), Mathf.Abs(_rotationSpeed));

            transform.Rotate(new Vector3(0, _CurrentRotationSpeed * Time.deltaTime, 0));
        }
    }
}
