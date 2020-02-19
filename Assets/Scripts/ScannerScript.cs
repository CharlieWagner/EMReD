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

    [Header("Scanner Field")]
    public GameObject ScannerField;
    //public float ExpansionSpeed = 0.1f;
    Animator _fieldAnimator;

    [HideInInspector]
    public bool scannerActivated = false;


    ScannableScript _Scannable;
    // Update is called once per frame
    public void Awake()
    {
        _camera = GetComponentsInChildren<Camera>();
        _fieldAnimator = ScannerField.GetComponent<Animator>();
    }
    public void Tool_Scanner()
    {
        if (!scannerActivated)
            _fieldAnimator.Play("FieldExpand");
        scannerActivated = true;
        scannerDisplay.SetActive(true);
        //Debug.Log(_layerMask);
        //StartCoroutine(FieldExpansion());
        _scannedColliders = Physics.OverlapSphere(_camera[1].transform.position, scannerRange, _layerMask);
        if (_scannedColliders != null)
        {
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
            _scannedColliders = Physics.OverlapSphere(transform.position, scannerRange, _layerMask);

            if (_scannedColliders != null)
            {
                foreach (Collider scanned in _scannedColliders)
                {
                    Renderer renderer = scanned.GetComponent<Renderer>();
                    //_objectShaders[1] = renderer.material.shader;
                    //renderer.material.shader = Shader.Find("Universal Render Pipeline/Lit");
                    _EmitStrenghtID = renderer.material.shader.GetPropertyNameId(renderer.material.shader.FindPropertyIndex("_EmitStrenght"));
                    renderer.material.SetFloat(_EmitStrenghtID, 0);
                }
                _scannedColliders = null;
            }

            //scannerActivated = false;
            scannableInfo.text = null;
            scannerDisplay.SetActive(false);


            //ScannerField.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            scannerActivated = false;
        }
    }
}
