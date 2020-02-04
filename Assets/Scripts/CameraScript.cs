using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public RoverController Rover;
    public GameObject RoverGameObject;

    public Transform YAxisTransform;

    public float initXRot;

    public float RotationX;
    public float RotationY;

    public float RangeX;
    public float RangeY;

    public float fov = 90;

    public bool upsideDown = false;

    public Transform _FinalTransform;

    private void Start()
    {
        initXRot = transform.eulerAngles.y;
    }

    private void Update()
    {
        if (!upsideDown)
        {
            transform.localRotation = Quaternion.Euler(0, RotationX + initXRot, 0);
        } else
        {
            transform.localRotation = Quaternion.Euler(0, RotationX + initXRot, 180);
        }
        YAxisTransform.localRotation = Quaternion.Euler(RotationY, 0, 0);
    }
    public void RotateCam(float Xaxis, float Yaxis)
    {
        if (upsideDown)
        {
            RotationX += Xaxis;
            RotationX = Mathf.Clamp(RotationX, -RangeX, RangeX);

            RotationY += Yaxis;
            RotationY = Mathf.Clamp(RotationY, -RangeY, RangeY);
        } else
        {
            RotationX += Xaxis;
            RotationX = Mathf.Clamp(RotationX, -RangeX, RangeX);

            RotationY -= Yaxis;
            RotationY = Mathf.Clamp(RotationY, -RangeY, RangeY);
        }
    }
}
