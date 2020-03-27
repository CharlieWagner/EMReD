using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public AudioClip[] TutorialLines;

    public AudioSource IALineSource;
    private IEnumerator coroutine;
    [HideInInspector]
    public int tutorialStep = 0;
    int lastPlayed = 0;
    int comparator = 0;
    bool isPlaying = false;



    // Start is called before the first frame update
    void Awake()
    {
        coroutine = StartLines();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Tutorial Step = " + tutorialStep);
        //Debug.Log("Last Played = " + lastPlayed);
        //Debug.Log("Comparator = " + (tutorialStep - lastPlayed));
        //Debug.Log(IALineSource.clip.name);
        //Debug.Log("isPlaying = " + isPlaying);

        comparator = tutorialStep - lastPlayed;
        if (!isPlaying && comparator > 0 && !IALineSource.isPlaying)
        {
            coroutine = AudioQueue();
            StartCoroutine(coroutine);
        }

    }

    //public void playVoiceLine(int lineNumber)
    //{
    //    coroutine = PlayLine(lineNumber);
    //    StartCoroutine(coroutine);
    //}

    IEnumerator StartLines()
    {
        IALineSource.Stop();
        IALineSource.clip = TutorialLines[0];
        IALineSource.Play();
        yield return new WaitForSeconds(IALineSource.clip.length + 0.5f);
        IALineSource.Stop();
        IALineSource.clip = TutorialLines[1];
        IALineSource.Play();
        yield return new WaitForSeconds(IALineSource.clip.length + 0.5f);
        IALineSource.Stop();
        IALineSource.clip = TutorialLines[2];
        IALineSource.Play();
        yield return new WaitForSeconds(IALineSource.clip.length + 0.5f);
        IALineSource.Stop();
        IALineSource.clip = TutorialLines[3];
        IALineSource.Play();
        tutorialStep = 3;
        lastPlayed = 3;
        yield return new WaitForSeconds(IALineSource.clip.length + 0.5f);
        coroutine = AudioQueue();
        StartCoroutine(coroutine);
    }

    //IEnumerator PlayLine(int lineNumber)
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    IALineSource.Stop();
    //    IALineSource.clip = TutorialLines[lineNumber];
    //    IALineSource.Play();
    //    yield return new WaitForSeconds(0.5f);
    //}

    IEnumerator AudioQueue()
    {
        
        int comparator = tutorialStep - lastPlayed;
        //if (comparator > 0 && !IALineSource.isPlaying)
        if (comparator > 0)
        {
            //Debug.Log("bip");
            isPlaying = true;
            for (int i = lastPlayed; lastPlayed <  tutorialStep; i++)
            {
                lastPlayed++;
                IALineSource.clip = TutorialLines[lastPlayed];
                IALineSource.Play();
                yield return new WaitForSeconds(IALineSource.clip.length + 0.5f);
                if (lastPlayed == 16)
                {
                    gameObject.SetActive(false);
                }
            }
            isPlaying = false;
            //comparator = tutorialStep - lastPlayed;
            //Debug.Log(comparator);
        }
        else if (comparator <= 0)
        {
            //isPlaying = false;
            //Debug.Log("boop");
        }
    }


}
