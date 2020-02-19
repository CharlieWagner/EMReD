using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserScript : MonoBehaviour
{
    Camera[] _camera;
    public LineRenderer _laserLine;
    public GameObject _LaserHitFX;
    public GameObject _LaserShootFX;
    public GameObject _LaserChargeEffect;

    public Transform _LaserTip;

    public GameObject _LaserUI;
    public Text _LaserDisplayText;

    public float _LaserCharge = 0;
    public float _LaserChargeCap = 3;

    public int _GaugeLength = 5;

    // Start is called before the first frame update
    public void Awake()
    {
        _camera = GetComponentsInChildren<Camera>();
    }
    public void Tool_Laser()
    {
        //string Warning = "";

        _LaserUI.SetActive(true); // Enable laser UI
        _LaserChargeEffect.SetActive(Input.GetButton("Fire1"));

        if (Input.GetButton("Fire1")) // Charge Laser
        {
            
            
            _LaserCharge += Time.deltaTime;
            _LaserCharge = Mathf.Clamp(_LaserCharge, 0, _LaserChargeCap);
        }



        if (Input.GetButtonUp("Fire1")) // Try shoot laser
        {


            if (_LaserCharge >= _LaserChargeCap) // if charged enough
            {
                Debug.Log("Shoot");

                RaycastHit hit;
                if (Physics.Raycast(_camera[1].transform.position, _camera[1].transform.forward, out hit, 50f))
                {
                    Debug.Log("raycast");
                    Instantiate(_LaserHitFX, hit.point, Quaternion.LookRotation(hit.normal));
                    Instantiate(_LaserShootFX, _LaserTip.position, Quaternion.LookRotation(_LaserTip.forward));

                    if (hit.transform.tag == "LaserTarget") // if Hit
                    {

                        Debug.Log("is laserTarget");
                        // Do the thing
                        LaserBehaviour TargetBehaviour;

                        TargetBehaviour = hit.transform.GetComponent<LaserBehaviour>();

                        TargetBehaviour.GetShot();
                        
                    }
                }


            }
            else // if not charged enough
            {
            }

            _LaserCharge = 0;
        }

        string GaugeText = ""; // Gauge
        for (int i = 0; i < _GaugeLength; i++)
        {
            if (_LaserCharge > (_LaserChargeCap / _GaugeLength * i))
                GaugeText += "|";
            else
                GaugeText += ".";
        }

        _LaserDisplayText.text = "niveau de charge : " + "\n" + "[" + GaugeText + "]"; // UI Display Update

        /*if (Input.GetButton("Fire1"))
        {

            

            RaycastHit hit;
            //Debug.DrawRay(_camera[1].transform.position, _camera[1].transform.forward * 10f, Color.red);
            if( Physics.Raycast(_camera[1].transform.position, _camera[1].transform.forward, out hit, 10f))
            {
                if (hit.transform.tag == "LaserTarget")
                {
                    //Debug.Log("Yolo swag hit the sack");
                }
                if (Time.frameCount % 3 == 0)
                    
            }

        } else {
            _laserLine.enabled = false;
        }*/
    }

    public void ShootLaser()
    {
        _laserLine.enabled = true;
    }



    public void Tool_DisableUI()
    {
        _LaserUI.SetActive(false); // Disable Laser UI
    }
}