﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulders : MonoBehaviour
{
    private Rigidbody[] _Boulders = new Rigidbody[25];
    [SerializeField]
    private GameObject _GroundCollider;

    private bool _HasFallen = false;
    private int _BoulderCount;
    [SerializeField]
    private Transform _BoulderContainer;
    private GameObject _Player;

    [SerializeField]
    private GameObject _particlePlayer;
    
    private AudioSource Source;

    private void Start()
    {
        _Player = GameObject.FindWithTag("Player");
        
        _BoulderCount = _BoulderContainer.childCount - 1;

        for (int i = 0; i <= _BoulderCount; i++)
        {
            _Boulders[i] = _BoulderContainer.GetChild(i).GetComponent<Rigidbody>();
        }


        _SetBouldersKinematic(true);

        if (_GroundCollider != null)
            _GroundCollider.SetActive(true);

    }

    private void OnTriggerEnter(Collider other)
    {

        
        if (other.tag == "Player")
        {
            if (!_HasFallen)
            {
                _SetBouldersKinematic(false);
                if (_GroundCollider != null)
                    _GroundCollider.SetActive(false);

                if (_particlePlayer != null)
                    _particlePlayer.SetActive(true);
                Debug.Log("boom");
                if (TryGetComponent<AudioSource>(out Source))
                {
                    Source.Play();
                }

                _HasFallen = true;
            }
        }
    }


    private void _SetBouldersKinematic(bool status = false)
    {
        for (int i = 0; i <= _BoulderCount; i++)
        {
            _Boulders[i].isKinematic = status;
            _Boulders[i].AddForce(new Vector3(0,-10,0));
        }
    }
    

    /*private void SetPlayerRespawnPoint()
    {
        _Player.GetComponent<RoverController>()._PlayerRespawnPoint = _RoverRespawnPoint;
    }*/
}
