using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineTrigger : MonoBehaviour
{
    public AudioSource Source;

    [SerializeField]
    private float _delay = 00f;

    private void Start()
    {
        Source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Invoke("PlaySource", _delay);
        }
    }

    private void PlaySource()
    {
        Source.Play();
    }
}
