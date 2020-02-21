using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField]
    private bool _fireOnce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<RoverController>().Kill();

            if (_fireOnce)
                Destroy(gameObject);
        }
    }
}
