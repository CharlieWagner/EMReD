using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySound : MonoBehaviour
{
    [SerializeField]
    private AudioSource Source;
    [SerializeField]
    private AudioSource Source2;

    [SerializeField]
    private string SceneToLoad;

    [SerializeField]
    private GameObject EndPrompt;

    public void PlaySoundEvent()
    {
        Source.Play();
    }

    public void PlaySoundEvent2()
    {
        Source2.Play();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && EndPrompt.activeSelf)
        {
            SceneManager.LoadScene(SceneToLoad);
        }
    }

}
