using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreController : MonoBehaviour
{
    private Renderer _R;
    private Material _CoreMat;

    [SerializeField]
    [Range(-1f,1f)]
    private float _Strength = -.06f;

    [SerializeField]
    private rotate[] _RotationHubs;

    private void Start()
    {
        _R = transform.GetChild(0).GetComponent<Renderer>();
        _CoreMat = _R.material;
    }

    private void Update()
    {
        _CoreMat.SetFloat("_DisplaceStr", _Strength);
    }

    public void initRotate()
    {
        for (int i = 0; i < _RotationHubs.Length; i++)
        {
            _RotationHubs[i]._StartRotation = true;
        }

    }
}
