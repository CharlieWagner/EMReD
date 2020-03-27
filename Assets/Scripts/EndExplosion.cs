using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class EndExplosion : MonoBehaviour
{
    [SerializeField]
    private Animator _AnimCtrl;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioMixer mainMixer;
    [SerializeField]
    private Transform _BloomBall;

    [SerializeField]
    private string _CreditsScene;

    private bool _goBoom = false;
    private GameObject Player;

    private bool _CanSelfDestruct = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player = other.gameObject;
            _CanSelfDestruct = true;
        }
    }

    private void Update()
    {
        if (_CanSelfDestruct && Input.GetButton("SelfDestruct"))
        {
            _AnimCtrl.SetTrigger("boom");
            _goBoom = true;
            float volume;
            mainMixer.GetFloat("volume", out volume);
            _audioSource.volume = volume;
        }

        if (_goBoom)
        {
            _BloomBall.transform.position = Player.transform.position;
        }

    }

    public void _Playsource()
    {
        _audioSource.Play();
    }

    public void _End()
    {
        SceneManager.LoadScene(_CreditsScene);
    }
}
