using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampTrigger : MonoBehaviour
{
    [SerializeField]
    private bool _stateToSwitch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<RoverController>()._Tool_Lamp(_stateToSwitch);
        }
    }
}
