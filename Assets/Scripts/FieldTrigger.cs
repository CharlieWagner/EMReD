using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public ScannerScript Scanner;
    int _layerMask = 1 << 12;

    private void Update()
    {
        transform.localPosition = new Vector3(0f, 0f, .001f * Mathf.Sin(Time.time));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ScannableScript>() != null)
        {
            Debug.Log(other.gameObject.name);
            Scanner.HighlightScan(other);
        }

    }
}
