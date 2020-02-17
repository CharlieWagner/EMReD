using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScannerScript : MonoBehaviour
{
    int _layerMask = 1 << 12; // Layer = Scannable
    Shader[] _objectShaders = new Shader[30];
    int _EmitStrenghtID;
    public int _EmitStrenght = 30;

    public float scannerRange = 10f;
    Collider[] _scannedColliders;

    Camera[] _camera;

    public GameObject scannerDisplay;
    public Text scannableInfo;


    ScannableScript _Scannable;
    // Update is called once per frame
    public void Awake()
    {
        _camera = GetComponentsInChildren<Camera>();
    }
    public void Tool_Scanner()
    {
        scannerDisplay.SetActive(true);
        //Debug.Log(_layerMask);
        _scannedColliders = Physics.OverlapSphere(_camera[1].transform.position, scannerRange, _layerMask);
        foreach (Collider scanned in _scannedColliders)
        {
            Debug.Log(scanned.name);
            Renderer renderer = scanned.GetComponent<Renderer>();
            //_objectShaders[1] = renderer.material.shader;
            _EmitStrenghtID = renderer.material.shader.GetPropertyNameId(renderer.material.shader.FindPropertyIndex("_EmitStrenght"));
            renderer.material.SetFloat(_EmitStrenghtID, Mathf.Lerp(0, _EmitStrenght, 0.025f));
            //renderer.enabled = false;
        }
        if (Input.GetButton("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(_camera[1].transform.position, _camera[1].transform.forward, out hit, scannerRange, _layerMask))
            {
                _Scannable = hit.transform.GetComponent<ScannableScript>();
                scannableInfo.text = _Scannable.DisplayInfo();
            }
        }
    }
    public void Tool_Scanner_Disable()
    {
        _scannedColliders = Physics.OverlapSphere(transform.position, scannerRange, _layerMask);
        foreach (Collider scanned in _scannedColliders)
        {
            Renderer renderer = scanned.GetComponent<Renderer>();
            //_objectShaders[1] = renderer.material.shader;
            //renderer.material.shader = Shader.Find("Universal Render Pipeline/Lit");
            _EmitStrenghtID = renderer.material.shader.GetPropertyNameId(renderer.material.shader.FindPropertyIndex("_EmitStrenght"));
            renderer.material.SetFloat(_EmitStrenghtID, 0);
        }   
        _scannedColliders = null;
        scannableInfo.text = null;
        scannerDisplay.SetActive(false);
    }
}
