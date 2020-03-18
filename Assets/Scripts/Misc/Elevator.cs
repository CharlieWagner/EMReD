using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    private GameObject _Player;

    private Rigidbody _PlayerRB;

    [SerializeField]
    private float _forceStr;

    [SerializeField]
    private Transform _ElevatorTransform;

    private void Start()
    {
        _Player = GameObject.FindWithTag("Player");
        _PlayerRB = _Player.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _Player.transform.SetParent(_ElevatorTransform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _Player.transform.SetParent(null);
        }
    }


    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _PlayerRB.AddForce(new Vector3(0,-_forceStr,0), ForceMode.Acceleration);
        }
    }*/
}
