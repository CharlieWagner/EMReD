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

    bool menuActivated;

    [Header("Sound")]
    [SerializeField]
    private AudioSource[] _Source; // 0 Cam Switch, 1 Cam Motor, 2 Rover Motor
    private float _WheelPitch;

    [Header("Tutorial")]
    [SerializeField]
    //private int tutorialStep = 0;
    public TutorialManager tutorial;
    bool movedLeft, movedRight, movedFront, movedBack; 
   

    public Rigidbody _PlayerRB;

    public Animator _CanvasAnim;

    public ThrusterScript _Thruster;
    public LaserScript _Laser;
    public ScannerScript _Scanner;
    public MenuManager _Menu;

    private StaticController _StaticCont;

    void Start()
    {
        _StaticCont = GetComponent<StaticController>();

    }
    private void Update()
    {
        //Debug.Log(Time.timeScale);
        if (!_Offline) // ------------------------------------------------------------------------ IF ROVER NOT OFFLINE
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetButtonUp("DisquetteA") || Input.GetButtonUp("DisquetteB") || Input.GetButtonUp("DisquetteC") || Input.GetButtonUp("DisquetteD"))
            {
                _CurrentTool = 0;
                _Source[0].Play();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetButtonDown("DisquetteA"))
            {
                _CurrentTool = 1;
                _Source[0].Play();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetButtonDown("DisquetteB"))
            {
                _CurrentTool = 2;
                _Source[0].Play();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetButtonDown("DisquetteC"))
            {
                _CurrentTool = 3;
                _Source[0].Play();
            }

            if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetButtonDown("DisquetteD"))
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
                //if (tutorial.tutorialStep == 11)
                //    tutorial.tutorialStep = 11;
            } else
            {
                _Laser.Tool_DisableUI();
            }

            if (_CurrentTool == 3)
                _Scanner.Tool_Scanner();
            else
            {
                _Scanner.Tool_Scanner_Disable(); 
                if (tutorial.tutorialStep == 15)
                    tutorial.tutorialStep = 16;
            }
        if (_CurrentTool == 4 && !menuActivated)
            {
                menuActivated = true;
                _Menu.Pause();
            }
            else if (_CurrentTool != 4 && menuActivated)
            {
                menuActivated = false;
                _Menu.Resume();
            }
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

        if (tutorial.tutorialStep == 3 && movedRight && movedLeft)
        {
            tutorial.tutorialStep = 4;
            //tutorial.AdvanceTutorial();
        }
        if (tutorial.tutorialStep == 4 && movedFront && movedBack)
        {
            tutorial.tutorialStep = 8;
            //tutorial.AdvanceTutorial();
        }
        

    }

    void FixedUpdate()
    {
        if (!_Offline) // ------------------------------------------------------------------------ IF ROVER NOT OFFLINE
        {
            if (_CurrentTool == 1)
            { // THRUSTERS
                _Thruster.Tool_Thruster();
                if (tutorial.tutorialStep == 8)
                {
                    tutorial.tutorialStep = 9;
                }
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
        if(Input.GetAxis("Horizontal") > 0 && tutorial.tutorialStep == 3)
        {
            movedRight = true;
        }
        if (Input.GetAxis("Horizontal") < 0 && tutorial.tutorialStep == 3)
        {
            movedLeft = true;
        }
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
        if (Input.GetAxis("Vertical") > 0 && tutorial.tutorialStep == 4)
        {
            movedFront= true;
        }
        if (Input.GetAxis("Vertical") < 0 && tutorial.tutorialStep == 4)
        {
            movedBack = true;
        }
    }

    public bool isGrounded()
    {
        bool toReturn;
        RaycastHit Hit;
        toReturn = Physics.SphereCast(transform.position, .3f, -Vector3.up, out Hit, 1f);
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
