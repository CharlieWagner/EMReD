using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    [Header("Canvases")]
    public GameObject MenuUI;
    public GameObject PauseMenuUI;
    public GameObject SoundMenuUI;

    public GameObject FirstButton;
    public GameObject SoundButton;
    public AudioMixer mainMixer;
    public Slider SoundLevel;



    public void Pause()
    {
        Time.timeScale = 0.1f;
        MenuUI.SetActive(true);
        PauseMenuUI.SetActive(true);
        SoundMenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(FirstButton);

    }

    public void Resume()
    {
        Time.timeScale = 1f;
        MenuUI.SetActive(false);
        PauseMenuUI.SetActive(true);
        SoundMenuUI.SetActive(false);

    }

    public void NewGame()
    {
        //Time.timeScale = 1f;
        Debug.Log("NewGame");
        //Time.timeScale = 0.1f;
        SceneManager.LoadScene("TitleScreen");
    }

    public void Quit()
    {
        //Time.timeScale = 1f;
        Debug.Log("Quit");
        //Time.timeScale = 0f;
        Application.Quit();
    }

    public void OpenSound()
    {
        //Time.timeScale = 1f;
        //Debug.Log("OpenSound");
        PauseMenuUI.SetActive(false);
        SoundMenuUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(SoundButton);
        //Time.timeScale = 0f;
    }
    
    public void Return()
    {
        PauseMenuUI.SetActive(true);
        SoundMenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(FirstButton);
    }

    public void SetVolume()
    {
        float volume;
        volume = SoundLevel.value;
        mainMixer.SetFloat("volume", volume);
    }

    //public void Update()
    //{
    //    float volume;
    //    mainMixer.GetFloat("volume", out volume);
    //    TextUpdate(volume);

    //    if (EventSystem.current.tag == "SoundButton")
    //    {
    //        //float volume;
    //        mainMixer.GetFloat("volume", out volume);
    //        if (volume >= -80f && Input.GetAxis("Horizontal") > 0f)
    //        {
    //            mainMixer.SetFloat("volume", volume + 1f);
    //            //TextUpdate(volume + 1f);
    //            Debug.Log(mainMixer.GetFloat("volume", out volume));
    //        }
    //        if (volume <= 0f && Input.GetAxis("Horizontal") < 0f)
    //        {
    //            mainMixer.SetFloat("volume", volume - 1f);
    //            //TextUpdate(volume - 1f);
    //            Debug.Log(mainMixer.GetFloat("volume", out volume));
    //        }
    //    }



    //}

    //public void TextUpdate(float volume)
    //{
    //    string GaugeText = null;

    //    for (int i = 0; i < _GaugeLength; i++)
    //    {
    //        if (-volume > (80 / _GaugeLength * i ))
    //            GaugeText += ".";
    //        else
    //            GaugeText += "|";
    //    }

    //    SoundGauge.text = GaugeText;
    //}

}
