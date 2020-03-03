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

    public CameraScript[] _CameraPosScript; // 0 Nothing // 1 Thruster // 2 Laser // 3 Scanner // 4 Menu

    /*public float _Vrange = 90.0f;//LIMITE vericale de la caméra
    public float _Hrange = 90.0f;*/
    [Header("Contrôles déplacement")]
    public float _GroundSpeed;
    public float _RotSpeed;
    public float _MaxAcceleration;
    public float _AirControl;
    private Vector3 _GroundedForwards;
    public float _maxFloorAngle;
    

    [Header("Rover Stuff")]

    public GameObject _MinimapCamera;
    public GameObject Lamp;

    public Text _HudText;
    [Range(0, 4)]
    public int _CurrentTool; // 0 Nothing // 1 Thruster // 2 Laser // 3 Scanner // 4 Menu
    public string[] _ToolText;
    public string[] _ToolTip;


    public bool _Offline;
    [SerializeField]
    private GameObject _RebootScreen;

    public Transform _PlayerRespawnPoint;

    [Header("Sound")]
    [SerializeField]
    private AudioSource[] _Source; // 0 Cam Switch, 1 Cam Motor, 2 Rover Motor
    private float _WheelPitch;

    public Rigidbody _PlayerRB;

    public Animator _CanvasAnim;

    public ThrusterScript _Thruster;
    public LaserScript _Laser;
    public ScannerScript _Scanner;

    private StaticController _StaticCont;

    void Start()
    {
        _StaticCont = GetComponent<StaticController>();

    }
    private void Update()
    {
        if (!_Offline) // ------------------------------------------------------------------------ IF ROVER NOT OFFLINE
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _CurrentTool = 0;
                _Source[0].Play();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _CurrentTool = 1;
                _Source[0].Play();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _CurrentTool = 2;
                _Source[0].Play();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                _CurrentTool = 3;
                _Source[0].Play();
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                _CurrentTool = 4;
                _Source[0].Play();
            }

            _Camera.transform.position = _CameraPosScript[_CurrentTool]._FinalTransform.position;
            _Camera.transform.rotation = _CameraPosScript[_CurrentTool]._FinalTransform.transform.rotation;
            _CameraCamera.fieldOfView  = _CameraPosScript[_CurrentTool].fov;

            // ACTIVE CAMERA QUAND NECESSAIRE
            if (_CurrentTool == 1 || _CurrentTool == 2 || _CurrentTool == 3)
            {
                _CameraPosScript[_CurrentTool].RotateCam(Input.GetAxis("Horizontal") * Time.deltaTime * _RotSpeed, Input.GetAxis("Vertical") * Time.deltaTime * _RotSpeed);

                if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && _CurrentTool != 1)
                {
                    if (!_Source[1].isPlaying)
                        _Source[1].Play();
                } else
                {
                    _Source[1].Stop();
                }

            }

            _HudText.text = "CURRENT TOOL : " + _ToolText[_CurrentTool] +
                    "\n" + _ToolTip[_CurrentTool];



            if (_CurrentTool == 0) // ------------------------------------------------- Son roues /
            {

                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    _WheelPitch += Time.deltaTime;
                }

                if (_WheelPitch != 0)
                {
                    if (!_Source[2].isPlaying)
                        _Source[2].Play();
                }
            }

            if (_CurrentTool == 2)
            {
                _Laser.Tool_Laser();
            } else
            {
                _Laser.Tool_DisableUI();
            }

            if (_CurrentTool == 3)
                _Scanner.Tool_Scanner();
            else
                _Scanner.Tool_Scanner_Disable();
        }
        else // ------------------------------------------------------------------------ IF ROVER OFFLINE
        {

            _Source[0].Stop();
            _Source[1].Stop();
            _Source[2].Stop();



            if (Input.GetKey(KeyCode.Backspace))
            {
                Debug.Log("Rebooting");

                Reboot();
            }


            //_RebootScreen.SetActive(true);
        }
        // ------------------------------------------------- Pitch son roues /

        _WheelPitch -= Time.deltaTime * .5f;
        _WheelPitch = Mathf.Clamp(_WheelPitch, 0, 1);

        if (_WheelPitch == 0)
        {
            _Source[2].Stop();
        }

        float _newPitch;
        _newPitch = (_WheelPitch * .3f) + .2f;
        _Source[2].pitch = _newPitch;

    }

    void FixedUpdate()
    {
        if (!_Offline) // ------------------------------------------------------------------------ IF ROVER NOT OFFLINE
        {
            if (_CurrentTool == 1)
            { // THRUSTERS
                _Thruster.Tool_Thruster();
            }
            else
            {
                _Thruster.EmptyGauge();
            }


            if (isGrounded())
            {
                if (_CurrentTool == 0)
                {
                    AccelerateTowards(BaseVelocityTarget(_GroundSpeed));
                    RotatePlayer();
                    Vector3 test = transform.rotation.eulerAngles;

                }

                if (_CurrentTool != 1)
                {
                    _Thruster.RestoreThruster();
                }
            }
            else
            {
            }
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
        toReturn = Physics.SphereCast(transform.position, .3f, -Vector3.up, out Hit, 0.6f);
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

    public void _Tool_Lamp(bool state)
    {
        Lamp.SetActive(state);
    }

    public void Reboot()
    {
        _Offline = false;
        _RebootScreen.SetActive(false);
    }

    public void Kill()
    {
        Debug.Log("KilledRover");

        _Offline = true;
        transform.position = _PlayerRespawnPoint.position;
        transform.rotation = _PlayerRespawnPoint.rotation;
        _RebootScreen.SetActive(true);
    }
    
}
