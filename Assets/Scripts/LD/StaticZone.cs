using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticZone : MonoBehaviour
{
    [SerializeField]
    private float _StaticAmount;

    private GameObject _Player;
    private StaticController _PlayerStatic;

    private void Start()
    {
        _Player = GameObject.FindWithTag("Player");
        _PlayerStatic = _Player.GetComponent<StaticController>();
    }

    private void OnTriggerStay(Collider other)
    {
        float _Dist = Vector3.Distance(_Player.transform.position, transform.position);
        
        float _newStatic = (1 - (2 * (_Dist / transform.localScale.x))) * _StaticAmount;


        /*if (_PlayerStatic._StaticAmount < _newStatic)
        {*/
            _PlayerStatic._StaticAmount = _newStatic;
        //}
    }
}
