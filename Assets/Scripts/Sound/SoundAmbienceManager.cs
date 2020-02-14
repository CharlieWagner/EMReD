using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAmbienceManager : MonoBehaviour
{
    public SoundAmbience[] _Amb = new SoundAmbience[10];
    public string[] _AmbID = new string[10];
    private int _AmbCount;

    private void Start()
    {
        _AmbCount = transform.childCount - 1;
        
        for (int i = 0; i <= _AmbCount; i++)
        {
            _Amb[i] = transform.GetChild(i).GetComponent<SoundAmbience>();
            _AmbID[i] = transform.GetChild(i).name;
        }
    }
    
    public void TriggerAmb(string TargetID)
    {
        for (int i = 0; i <= _AmbCount; i++)
        {
            if (TargetID == _AmbID[i])
            {
                _Amb[i].SetActiveState(true);
            } else
            {
                _Amb[i].SetActiveState(false);
            }
        }
    }
}
