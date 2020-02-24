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

    [SerializeField]
    private float _LaserCharge = 0;
    [SerializeField]
    private float _LaserChargeCap = 3;

    [SerializeField]
    private int _GaugeLength = 5;

    [SerializeField]
    private AudioClip[] _LaserSounds; // 0 charge / 1 charged / 2 shoot
    private AudioSource _Audio;


    [SerializeField]
    private LayerMask _laserMask;


    // Start is called before the first frame update
    public void Awake()
    {
        _camera = GetComponentsInChildren<Camera>();
        _Audio = _LaserTip.gameObject.GetComponent<AudioSource>();
    }
    public void Tool_Laser()
    {
        //string Warning = "";

        _LaserUI.SetActive(true); // Enable laser UI
        _LaserChargeEffect.SetActive(Input.GetButton("Fire1"));

        if (Input.GetButtonDown("Fire1"))
        {
            _Audio.Play();
            _Audio.loop = true;
        }

        if (Input.GetButton("Fire1")) // Charge Laser
        {
            
            
            _LaserCharge += Time.deltaTime;
            //_LaserCharge = Mathf.Clamp(_LaserCharge, 0, _LaserChargeCap);

            if (_LaserCharge < 1.95)
            {
                _Audio.clip = _LaserSounds[0];
            } else
            {
                _Audio.clip = _LaserSounds[1];
            }

            if (!_Audio.isPlaying)
                _Audio.Play();
        }



        if (Input.GetButtonUp("Fire1")) // Try shoot laser
        {
            _Audio.loop = false;

            if (_LaserCharge >= _LaserChargeCap) // if charged enough
            {
                Debug.Log("Shoot");

                Instantiate(_LaserShootFX, _LaserTip.position, Quaternion.LookRotation(_LaserTip.forward));
                _Audio.clip = _LaserSounds[2];
                if (!_Audio.isPlaying)
                    _Audio.Play();

                RaycastHit hit;
                if (Physics.Raycast(_camera[1].transform.position, _camera[1].transform.forward, out hit, 150f, _laserMask))
                {
                    Debug.Log("raycast");
                    Instantiate(_LaserHitFX, hit.point, Quaternion.LookRotation(hit.normal));

                    if (hit.collider.transform.tag == "LaserTarget") // if Hit
                    {

                        Debug.Log("is laserTarget");
                        // Do the thing
                        LaserBehaviour TargetBehaviour;

                        TargetBehaviour = hit.collider.transform.GetComponent<LaserBehaviour>();

                        TargetBehaviour.GetShot();
                        
                    }
                }

            }
            else // if not charged enough
            {
                _Audio.Stop();
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