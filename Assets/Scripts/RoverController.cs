using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoverController : MonoBehaviour
{
    [Header("Contrôles caméra")]
    public float xsens = 2f;//sensi' horizontale
    public float ysens = 2f;//sensi' verticale

    public GameObject Camera;//Geez I wonder what that is
    public Camera CameraCamera;

    public GameObject[] CameraPos;
    public CameraScript[] CameraPosScript;
    //private float[] HorizontalRot;
    //private float[] VerticalRot;//rotation verticale de la caméra
    public int SelectedCam;
    public float Vrange = 90.0f;//LIMITE vericale de la caméra
    public float Hrange = 90.0f;
    [Header("Contrôles déplacement")]
    public float GroundSpeed;
    public float RotSpeed;
    public float MaxAcceleration;
    public float AirControl;
    public float JetpackSpeed;
    private Vector3 GroundedForwards;
    public float maxFloorAngle;

    public float WheelRotateSpeed;
    public Transform[] Wheels;
    [Header("Rover Stuff")]
    public GameObject MinimapCamera;

    public Text HudText;
    public string[] ControlModeText;
    public string[] ToolText;
    public int Slot01;
    public int Slot02;


    [Header("Sound")]
    public AudioClip[] Sound; // 0 Cam Switch, 1 Cam Motor, 2 Rover Motor, 3 Control Mode Switch
    public AudioSource Source;

    public int ControlMode; // 0 rover 1 camera 2 ???

    public Rigidbody PlayerRB;

    public Animator CanvasAnim;

    void Start()
    {

    }

    void Update()
    {

        if (ControlMode == 1)
        {
            CameraPosScript[SelectedCam].RotateCam(Input.GetAxis("Horizontal") * Time.deltaTime * RotSpeed, Input.GetAxis("Vertical") * Time.deltaTime * RotSpeed);

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

        Camera.transform.position = CameraPos[SelectedCam].transform.position;
        Camera.transform.rotation = CameraPos[SelectedCam].transform.rotation;
        CameraCamera.fieldOfView = CameraPosScript[SelectedCam].fov;

        

        HudText.text = ControlModeText[ControlMode] +
                "\n" + "Camera " + (SelectedCam + 1) +
                "\n" +
                "\n" + "Slot 01 : " + ToolText[Slot01] +
                "\n" + "Slot 02 : " + ToolText[Slot02];

        if (isGrounded())
        {
            if (ControlMode == 0)
            {
                AccelerateTowards(BaseVelocityTarget(GroundSpeed));

                RotatePlayer();
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
        //return (transform.rotation * new Vector3(Input.GetAxis("Vertical"), 0, 0) * Speed);
        //return ( * new Vector3(Input.GetAxis("Vertical"), 0, 0) * Speed);
        return (GroundedForwards * Input.GetAxis("Vertical") * Speed);
    }

    public void RotatePlayer()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0) * Time.deltaTime * RotSpeed);
    }

    public void AccelerateTowards(Vector3 Target)
    {
        Vector3 Acceleration;
        Vector3 XZVelocity;
        XZVelocity = PlayerRB.velocity - new Vector3(0, PlayerRB.velocity.y, 0);

        Acceleration = Target - XZVelocity;

        /*if (Acceleration.magnitude >= .1f)
        {
            Source.clip = Sound[2];
            Source.pitch = 1f;// + (XZVelocity.magnitude / 5);
            Source.Play();
        }*/

        PlayerRB.AddForce(Acceleration.normalized * Mathf.Clamp(Acceleration.magnitude, 0, MaxAcceleration));
    }

    public bool isGrounded()
    {
        //return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        bool toReturn;
        RaycastHit Hit;
        toReturn = Physics.SphereCast(transform.position, .3f, -Vector3.up, out Hit, 0.4f);
        if (Vector3.Angle(transform.up, Hit.normal) <= maxFloorAngle)
        {
            GroundedForwards = Quaternion.AngleAxis(-90, transform.forward) * Hit.normal;
            Debug.DrawLine(Hit.point, Hit.point + GroundedForwards);
        } else
        {
            GroundedForwards = transform.forward;
        }        
        return toReturn;
    }
}
