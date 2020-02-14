using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAmbience : MonoBehaviour
{
    private AudioSource _Source;
    public bool _isActive;
    public float _maxVolume;
    public float _CurrentVolume;
    public float _TransitionSpeed;

    private void Start()
    {
        _Source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _Source.volume = Mathf.Clamp(_CurrentVolume, 0, _maxVolume);

        if (_isActive)
        {
            if (!_Source.isPlaying)
            {
                _Source.Play();
            }

            if (_CurrentVolume < _maxVolume)
            {
                _CurrentVolume += _TransitionSpeed * Time.deltaTime;
            }
        } else
        {
            if (_CurrentVolume > 0)
            {
                _CurrentVolume -= _TransitionSpeed * Time.deltaTime;
            }
        }

        if (_CurrentVolume == 0)
        {
            _Source.Stop();
        }

    }

    public void SetActiveState(bool state)
    {
        _isActive = state;
    }

}
