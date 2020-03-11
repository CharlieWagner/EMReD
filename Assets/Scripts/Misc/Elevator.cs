using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    private GameObject _Player;

    private Rigidbody _PlayerRB;

    [SerializeField]
    private float _forceStr;

    private void Start()
    {
        _Player = GameObject.FindWithTag("Player");
        _PlayerRB = _Player.GetComponent<Rigidbody>();
    }
    

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _PlayerRB.AddForce(new Vector3(0,-_forceStr,0), ForceMode.Acceleration);
        }
    }
}
