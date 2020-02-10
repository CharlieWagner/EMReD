using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulders : MonoBehaviour
{

    public Rigidbody[] _Boulders;
    public GameObject _GroundCollider;
    private bool _HasFallen;

    private void Start()
    {
        _SetBouldersKinematic(true);

        _GroundCollider.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {

        
        if (other.tag == "Player")
        {
            _SetBouldersKinematic(false);
            _GroundCollider.SetActive(false);
            Debug.Log("boom");
        }
    }


    private void _SetBouldersKinematic(bool status = false)
    {
        for (int i = 0; i <= _Boulders.Length - 1; i++)
        {
            _Boulders[i].isKinematic = status;
        }
    }
}
