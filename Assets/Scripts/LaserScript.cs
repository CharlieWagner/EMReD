using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    Camera[] _camera;
    LineRenderer _laserLine;
    public GameObject _LaserHitFX;
    // Start is called before the first frame update
    public void Awake()
    {
        _camera = GetComponentsInChildren<Camera>();
        _laserLine = _camera[1].GetComponent<LineRenderer>();
    }
    public void Tool_Laser()
    {
        if (Input.GetButton("Fire1"))
        {

            _laserLine.enabled = true;

            RaycastHit hit;
            //Debug.DrawRay(_camera[1].transform.position, _camera[1].transform.forward * 10f, Color.red);
            if( Physics.Raycast(_camera[1].transform.position, _camera[1].transform.forward, out hit, 10f))
            {
                if (hit.transform.tag == "LaserTarget")
                {
                    //Debug.Log("Yolo swag hit the sack");
                }
                if (Time.frameCount % 3 == 0)
                    Instantiate(_LaserHitFX, hit.point + (hit.normal*.05f), Quaternion.identity);
            }

        } else {
            _laserLine.enabled = false;
        }
    }
}