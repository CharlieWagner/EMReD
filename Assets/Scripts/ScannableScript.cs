using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScannableScript : MonoBehaviour
{
    public string objectName;
    public string objectClassification;
    public string objectType;
    public string objectDangerousity;
    public string objectDescription;

    // Start is called before the first frame update
    public string DisplayInfo()
    {
        string toDipslay = null;
        toDipslay = "ANALYSE: " + objectName + "\n"
            + "CLASSIFICATION: " + objectClassification + "\n"
            + "TYPE: " + objectType + "\n"
            + "DANGEROSITE: " + objectDangerousity + "\n"
            + "DESCRIPTION: " + objectDescription + "\n";
        return toDipslay;
    }
}
