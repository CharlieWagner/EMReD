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

    [SerializeField]
    private float _AudioSourcesVolume;

    private AudioSource[] _childSources = new AudioSource[25];
    private int _blockCount;

    public bool _StartRotation = false;



    private void Start()
    {
        _blockCount = transform.childCount - 1;

        for (int i = 0; i <= _blockCount; i++)
        {
            if (transform.GetChild(i).GetComponent<AudioSource>() != null)
                _childSources[i] = transform.GetChild(i).GetComponent<AudioSource>();
            _childSources[i].volume = 0f;
        }
    }


    void Update()
    {
        if (_RotateByDefault)
        {
            transform.Rotate(new Vector3(0, _rotationSpeed * Time.deltaTime, 0));
        } else if (_StartRotation)
        {
            _CurrentRotationSpeed += Mathf.Sign(_rotationSpeed) * Time.deltaTime * _accelSpeed;
            _CurrentRotationSpeed = Mathf.Clamp(_CurrentRotationSpeed,-Mathf.Abs(_rotationSpeed), Mathf.Abs(_rotationSpeed));

            for (int i = 0; i <= _blockCount; i++)
            {
                _childSources[i].volume = (_CurrentRotationSpeed / _rotationSpeed) * _AudioSourcesVolume;
            }
            transform.Rotate(new Vector3(0, _CurrentRotationSpeed * Time.deltaTime, 0));
        }
    }
}
