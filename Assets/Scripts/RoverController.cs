using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoverController : MonoBehaviour
{
    [Header("Contrôles caméra")]
    public float _xsens = 2f;//sensi' horizontale
    public float _ysens = 2f;//sensi' verticale

    public GameObject _Camera;//Geez I wonder what that is
    public Camera _CameraCamera;

    public CameraScript[] _CameraPosScript;
    
    /*public float _Vrange = 90.0f;//LIMITE vericale de la caméra
    public float _Hrange = 90.0f;*/
    [Header("Contrôles déplacement")]
    public float _GroundSpeed;
    public float _RotSpeed;
    public float _MaxAcceleration;
    public float _AirControl;
    private Vector3 _GroundedForwards;
    public float _maxFloorAngle;

    public float _WheelRotateSpeed;
    public Transform[] _Wheels;
    [Header("Rover Stuff")]

    public GameObject _MinimapCamera;

    public Text _HudText;
    [Range(0, 4)]
    public int _CurrentTool; // 0 Nothing // 1 Thruster // 2 Laser // 3 Scanner // 4 Menu
    public string[] _ToolText;
    public string[] _ToolTip;


    [Header("Sound")]
    public AudioClip[] _Sound; // 0 Cam Switch, 1 Cam Motor, 2 Rover Motor, 3 Control Mode Switch
    public AudioSource _Source;

    public Rigidbody _PlayerRB;

    public Animator _CanvasAnim;

    public ThrusterScript _Thruster;


    void Start()
    {
    }
    private void Update()
    {
        /*
        if (ControlMode == 1)
        {
            
            

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                Source.clip = Sound[1];
                Source.pitch = 1;
                Source.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.B)) // SWITCH CONTROL MODE
        {
            ControlMode += 1;
            ControlMode = ControlMode % 2;
            Source.clip = Sound[3];
            Source.pitch = 1;
            Source.Play();
        }

        if (Input.GetKeyDown(KeyCode.C)) // SWITCH CAMERA
        {
            SelectedCam = SelectedCam + 1;
            SelectedCam = SelectedCam % (CameraPos.Length);

            Source.clip = Sound[0];
            Source.pitch = 1;
            Source.Play();
            CanvasAnim.SetTrigger("CamSwitch");
        }
        */

        if (Input.GetKey(KeyCode.Alpha1))
            _CurrentTool = 0;

        if (Input.GetKey(KeyCode.Alpha2))
            _CurrentTool = 1;

        if (Input.GetKey(KeyCode.Alpha3))
            _CurrentTool = 2;

        if (Input.GetKey(KeyCode.Alpha4))
            _CurrentTool = 3;

        if (Input.GetKey(KeyCode.Alpha5))
            _CurrentTool = 4;

        _Camera.transform.position = _CameraPosScript[_CurrentTool]._FinalTransform.position;
        _Camera.transform.rotation = _CameraPosScript[_CurrentTool]._FinalTransform.transform.rotation;
        _CameraCamera.fieldOfView  = _CameraPosScript[_CurrentTool].fov;

        // ACTIVE CAMERA QUAND NECESSAIRE
        if (_CurrentTool == 1 || _CurrentTool == 2 || _CurrentTool == 3)
            _CameraPosScript[_CurrentTool].RotateCam(Input.GetAxis("Horizontal") * Time.deltaTime * _RotSpeed, Input.GetAxis("Vertical") * Time.deltaTime * _RotSpeed);


        _HudText.text = "CURRENT TOOL : " + _ToolText[_CurrentTool] +
                "\n" + _ToolTip[_CurrentTool];


        if (_CurrentTool == 0) // LAMP
        {
            _Tool_Lamp();
        } else if (_CurrentTool == 1) { // THRUSTERS
            //_Tool_Thruster();
        }

        

    }

    void FixedUpdate()
    {

        if (_CurrentTool == 1) { // THRUSTERS
            _Thruster.Tool_Thruster();
        }


        if (isGrounded())
        {
            if (_CurrentTool == 0 || _CurrentTool == 1)
            {
                AccelerateTowards(BaseVelocityTarget(_GroundSpeed));
                RotatePlayer();
                Vector3 test = transform.rotation.eulerAngles;
            }
        }
        else
        {
            /*
            float dotVectors;
            dotVectors = Vector3.Dot(BaseVelocityTarget(AirControl).normalized, new Vector3(PlayerRB.velocity.x, 0, PlayerRB.velocity.z).normalized);
            dotVectors = -dotVectors + 1;
            dotVectors = Mathf.Clamp(dotVectors, 0, 1);
            Debug.Log(dotVectors);
            PlayerRB.AddForce(BaseVelocityTarget(AirControl) * dotVectors);*/
        }
    }

    public Vector3 BaseVelocityTarget(float Speed) // Base X & Z axis velocity target
    {
        return (_GroundedForwards * Input.GetAxis("Vertical") * Speed);
    }

    public void RotatePlayer()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0) * Time.deltaTime * _RotSpeed);
    }

    public void AccelerateTowards(Vector3 Target)
    {
        Vector3 Acceleration;
        Vector3 XZVelocity;
        XZVelocity = _PlayerRB.velocity - new Vector3(0, _PlayerRB.velocity.y, 0);

        Acceleration = Target - XZVelocity;

        /*if (Acceleration.magnitude >= .1f)
        {
            Source.clip = Sound[2];
            Source.pitch = 1f;// + (XZVelocity.magnitude / 5);
            Source.Play();
        }*/

        _PlayerRB.AddForce(Acceleration.normalized * Mathf.Clamp(Acceleration.magnitude, 0, _MaxAcceleration));
    }

    public bool isGrounded()
    {
        bool toReturn;
        RaycastHit Hit;
        toReturn = Physics.SphereCast(transform.position, .3f, -Vector3.up, out Hit, 0.4f);
        if (Vector3.Angle(transform.up, Hit.normal) <= _maxFloorAngle)
        {
            _GroundedForwards = Quaternion.AngleAxis(-90, transform.forward) * Hit.normal;
            Debug.DrawLine(Hit.point, Hit.point + _GroundedForwards);
        } else
        {
            _GroundedForwards = transform.forward;
        }        
        return toReturn;
    }

    public void _Tool_Lamp()
    {
        GameObject Lamp = transform.GetChild(8).gameObject;
        Lamp.SetActive(Input.GetButton("Fire1"));
    }

}
