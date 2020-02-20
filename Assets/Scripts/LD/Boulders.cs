using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulders : MonoBehaviour
{
    [SerializeField]
    private Rigidbody[] _Boulders;
    [SerializeField]
    private GameObject _GroundCollider;
    [SerializeField]
    private Transform _RoverRespawnPoint;

    private bool _HasFallen;

    private GameObject _Player;

    [SerializeField]
    private AudioSource Source;

    private void Start()
    {
        _Player = GameObject.FindWithTag("Player");

        _SetBouldersKinematic(true);

        _GroundCollider.SetActive(true);

    }

    private void OnTriggerEnter(Collider other)
    {

        
        if (other.tag == "Player")
        {
            _SetBouldersKinematic(false);
            _GroundCollider.SetActive(false);
            
            SetPlayerRespawnPoint();

            Debug.Log("boom");
            if (TryGetComponent<AudioSource>(out Source))
            {
                Source.Play();
            }
        }
    }


    private void _SetBouldersKinematic(bool status = false)
    {
        for (int i = 0; i <= _Boulders.Length - 1; i++)
        {
            _Boulders[i].isKinematic = status;
        }
    }
    

    private void SetPlayerRespawnPoint()
    {
        _Player.GetComponent<RoverController>()._PlayerRespawnPoint = _RoverRespawnPoint;
    }
}
