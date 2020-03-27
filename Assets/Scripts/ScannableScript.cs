using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScannableScript : MonoBehaviour
{
    //public string objectName;
    public string objectClassification;
    public string objectType;
    public string objectDangerousity;
    public string objectDescription;

    // Start is called before the first frame update
    public string DisplayInfo()
    {
        string toDipslay = null;
        toDipslay =
            "CLASSIFICATION: " + objectClassification + "\n" + "\n"
            + "TYPE: " + objectType + "\n" + "\n"
            + "DANGEROSITE: " + objectDangerousity + "\n" + "\n"
            + "DESCRIPTION: " + objectDescription;
        return toDipslay;
    }
}
