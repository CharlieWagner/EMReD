using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrusterScript : MonoBehaviour
{
    public RoverController _C;

    public float _ThrusterFuel;
    public float _MaxThrusterFuel;
    public AnimationCurve _ThrusterForceCurve;
    public float _ThrusterForce;
    public float _ThrusterDepleteRate;
    public float _ThrusterRechargeRate;

    public float _HoverSpeed;

    public AudioSource _ThrusterSoundSource;
    public AudioClip _ThrusterSound;

    public int _GaugeLength;
    public Text _ThrusterGauge;

    public void Tool_Thruster()
    {
        if (Input.GetButton("Fire1"))
        {
            if (_ThrusterFuel > 0)
            {
                _C._PlayerRB.AddForce(Vector3.up * _ThrusterForceCurve.Evaluate(_ThrusterFuel / _MaxThrusterFuel) * _ThrusterForce);

                _C.AccelerateTowards(ThrustVelocityTarget(_HoverSpeed));

                _ThrusterFuel -= _ThrusterDepleteRate * Time.fixedDeltaTime;
                
                _ThrusterFuel = Mathf.Clamp(_ThrusterFuel, 0, _MaxThrusterFuel);

                /*if (Time.frameCount % 5 == 0)
                {
                    _ThrusterSoundSource.clip = _ThrusterSound;
                    _ThrusterSoundSource.pitch = _ThrusterForceCurve.Evaluate(_ThrusterFuel / _MaxThrusterFuel);// + (XZVelocity.magnitude / 5);
                    _ThrusterSoundSource.Play();
                }*/

            }
        } else
        {
            if (_C.isGrounded())
                RestoreThruster();
        }


        TextUpdate();
    }

    public void RestoreThruster()
    {
        _ThrusterFuel += _ThrusterRechargeRate * Time.fixedDeltaTime;
        _ThrusterFuel = Mathf.Clamp(_ThrusterFuel, 0, _MaxThrusterFuel);
    }

    public void TextUpdate()
    {
        string WarningText = null;
        string GaugeText = null;

        for (int i = 0; i < _GaugeLength; i++)
        {
            if (_ThrusterFuel > (_MaxThrusterFuel / _GaugeLength * i))
                GaugeText += "|";
            else
                GaugeText += ".";
        }

        if ((Mathf.Sin(Time.time * 10) > 0) && (_ThrusterFuel <= _MaxThrusterFuel * .5f))
            WarningText = "ENERGY LOW";

        //Debug.Log(Time.time);

        _ThrusterGauge.text = WarningText +
                        "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "Thruster Energy :" +
                        "\n" + "[" + GaugeText + "]";
    }

    public void EmptyGauge()
    {
        if (_ThrusterGauge.text != null)
            _ThrusterGauge.text = null;
    }

    public Vector3 ThrustVelocityTarget(float Speed) // Base X & Z axis velocity target
    {
        return ((transform.right * Input.GetAxis("Vertical") * Speed) + (transform.forward * -Input.GetAxis("Horizontal") * Speed));
    }

}
