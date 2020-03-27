using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScannerScript : MonoBehaviour
{
    RaycastHit hit;
    int _layerMask = 1 << 12; // Layer = Scannable
    Shader[] _objectShaders = new Shader[30];
    int _EmitStrenghtID;
    public int _EmitStrenght = 1;

    public float scannerRange = 10f;
    List<Collider> _scannedColliders =  new List<Collider>();

    Camera[] _camera;

    public GameObject scannerDisplay;
    public Text scannableInfo;
    public GameObject infoIndicator;

    [Header("Scanner Field")]
    public GameObject ScannerField;
    //public float ExpansionSpeed = 0.1f;
    Animator _fieldAnimator;

    [HideInInspector]
    public bool scannerActivated = false;
    bool infoDisplayed = false;


    ScannableScript _Scannable;
    // Update is called once per frame

    [Header("Tutorial")]
    public TutorialManager tutorial;
    public void Awake()
    {
        _camera = GetComponentsInChildren<Camera>();
        _fieldAnimator = ScannerField.GetComponent<Animator>();
    }
    public void Tool_Scanner()
    {
        ScannerField.SetActive(true);
        if (!scannerActivated)
            _fieldAnimator.Play("FieldExpand", -1, 0f);
        scannerActivated = true;
        scannerDisplay.SetActive(true);
        //Debug.Log(_layerMask);
        //StartCoroutine(FieldExpansion());
        //_scannedColliders = Physics.OverlapSphere(_camera[1].transform.position, scannerRange, _layerMask);
        if (_scannedColliders.Count > 0)
        {
            //foreach (Collider scanned in _scannedColliders)
            //{
            //    Debug.Log(scanned.name);
                
            //    //renderer.enabled = false;
            //}
            if (Input.GetButton("Fire1"))
            {
                if (Physics.Raycast(_camera[1].transform.position, _camera[1].transform.forward, out hit, Mathf.Infinity, _layerMask))
                {
                    _Scannable = hit.transform.GetComponent<ScannableScript>();
                    scannableInfo.text = _Scannable.DisplayInfo();
                    infoIndicator.SetActive(true);
                    infoDisplayed = true;
                    if (tutorial.tutorialStep == 14)
                        tutorial.tutorialStep = 15;
                }
                
            }
            if (infoDisplayed)
            {
                if (!Physics.Raycast(_camera[1].transform.position, _camera[1].transform.forward, out hit, Mathf.Infinity, _layerMask))
                {
                    infoDisplayed = false;
                    scannableInfo.text = null;
                    infoIndicator.SetActive(false);
                }
            }
        }
    }

    //IEnumerator FieldExpansion()
    //{

    //    for (float fieldsize = 0.1f; fieldsize <= 50f; fieldsize += ExpansionSpeed)
    //    {
    //        //Vector3 POS = ScannerField.transform.localPosition;
    //        ScannerField.transform.localScale = new Vector3(fieldsize, fieldsize, fieldsize);
    //        //ScannerField.transform.localPosition = POS + new Vector3(0.1f, 0f, 0.1f) ;
    //         yield return null;
    //    }
    //}

    public void Tool_Scanner_Disable()
    {
        if (scannerActivated)
        {
            //_scannedColliders = Physics.OverlapSphere(transform.position, scannerRange, _layerMask);

            if (_scannedColliders != null)
            {
                foreach (Collider scanned in _scannedColliders)
                {
                    Renderer renderer = scanned.GetComponent<Renderer>();
                    //_objectShaders[1] = renderer.material.shader;
                    //renderer.material.shader = Shader.Find("Universal Render Pipeline/Lit");
                    _EmitStrenghtID = renderer.material.shader.GetPropertyNameId(renderer.material.shader.FindPropertyIndex("_EmitStrenght"));
                    Debug.Log(renderer.material.GetFloat(_EmitStrenghtID)); 
                    renderer.material.SetFloat(_EmitStrenghtID, 0);
                }
                _scannedColliders.Clear();
            }

            //scannerActivated = false;
            scannableInfo.text = null;
            infoIndicator.SetActive(false);
            scannerDisplay.SetActive(false);

            ScannerField.SetActive(false);
            //ScannerField.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            scannerActivated = false;
        }
    }

    public void HighlightScan(Collider scanned)
    {
        Renderer renderer = scanned.GetComponent<Renderer>();
        //_objectShaders[1] = renderer.material.shader;
        _EmitStrenghtID = renderer.material.shader.GetPropertyNameId(renderer.material.shader.FindPropertyIndex("_EmitStrenght"));
        renderer.material.SetFloat(_EmitStrenghtID, 1f);  
        _scannedColliders.Add(scanned);
    }
}
