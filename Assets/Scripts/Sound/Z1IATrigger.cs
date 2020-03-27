using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z1IATrigger : MonoBehaviour
{

    private GameObject _Player;
    private RoverController _PlayerController;
    private bool _done = false;

    [SerializeField]
    private AudioSource Source;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _Player == null)
        {
            _Player = other.gameObject;
            _PlayerController = _Player.GetComponent<RoverController>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_Player != null && !_done)
        {
            if (_PlayerController._CurrentTool == 3 && Input.GetButton("Fire1"))
            {
                Source.Play();
                _done = true;
            }
        }
    }
}
