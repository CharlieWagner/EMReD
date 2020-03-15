using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcheroo : MonoBehaviour
{
    [SerializeField]
    private GameObject _FakeObelisk;

    [SerializeField]
    private GameObject _RealObelisk;

    private void Start()
    {
        _FakeObelisk.SetActive(true);
        _RealObelisk.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("switch");
        _FakeObelisk.SetActive(false);
        _RealObelisk.SetActive(true);
    }
}
