using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{

    public SoundAmbienceManager AmbManager;
    public string AmbienceName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AmbManager.TriggerAmb(AmbienceName);
        }
    }
}
