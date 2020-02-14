using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateCameraScript : MonoBehaviour
{
    public Camera PlayerCam;
    public Camera PortalCam;

    public Transform Player;

    public GameObject PlayerSidePortal;
    public GameObject OtherSidePortal;


    public bool wentThrough = false;

    public Vector3 PlayerOffset;

    private Vector3 Offset;

    private void Update()
    {
        if (!wentThrough)
        {
            Offset = PlayerCam.transform.position - PlayerSidePortal.transform.position;

            PortalCam.transform.position = OtherSidePortal.transform.position + Offset;
            PortalCam.transform.rotation = PlayerCam.transform.rotation;
            PortalCam.fieldOfView = PlayerCam.fieldOfView;
            PortalCam.nearClipPlane = Mathf.Clamp(Vector3.Distance(PortalCam.transform.position, OtherSidePortal.transform.position) - 3, .01f, 10000);

            PlayerOffset = Player.position - PlayerSidePortal.transform.position;
        } else
        {
            PortalCam.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = OtherSidePortal.transform.position + PlayerOffset;
            wentThrough = true;
        }
    }
}
