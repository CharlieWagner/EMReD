using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticController : MonoBehaviour
{
    [Range(0f,1f)]
    public float _StaticAmount;

    [SerializeField]
    private RawImage _StaticImage;
    private Material _StaticMat;

    [SerializeField]
    private AudioSource _StaticSource;


    private void Start()
    {
        _StaticMat = _StaticImage.material;
    }

    private void Update()
    {
        _StaticMat.SetFloat("_Strength", _StaticAmount + .2f);
        _StaticSource.volume = _StaticAmount;
    }
}
