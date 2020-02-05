using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    Camera[] _camera;
    LineRenderer _laserLine;
    // Start is called before the first frame update
    public void Awake()
    {
        _camera = GetComponentsInChildren<Camera>();
        _laserLine = _camera[1].GetComponent<LineRenderer>();
    }
    public void Tool_Laser()
    {
        if (Input.GetButtonDown("Fire1"))
            _laserLine.enabled = true;
        if (Input.GetButton("Fire1"))
        {
            
            RaycastHit hit;
            //Debug.DrawRay(_camera[1].transform.position, _camera[1].transform.forward * 10f, Color.red);
            if( Physics.Raycast(_camera[1].transform.position, _camera[1].transform.forward, out hit, 10f))
            {
                if (hit.transform.tag == "LaserTarget")
                {
                    //Debug.Log("Yolo swag hit the sack");
                }
            }

        }
        if (Input.GetButtonUp("Fire1"))
            _laserLine.enabled = false;
    }
}