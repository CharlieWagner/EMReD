using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameObject _Player;

    [SerializeField]
    private Transform _SpawnPos;

    private void Start()
    {
        _Player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        _Player.GetComponent<RoverController>()._PlayerRespawnPoint = _SpawnPos;
    }
}
