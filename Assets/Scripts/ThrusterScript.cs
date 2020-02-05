using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrusterScript : MonoBehaviour
{
    public RoverController _C;

    public float _ThrusterFuel;
    public float _MaxThrusterFuel;
    public AnimationCurve _ThrusterForce;
    public float _ThrusterDepleteRate;
    public float _ThrusterRechargeRate;

    public int _GaugeLength;
    public Text _ThrusterGauge;

    public void Tool_Thruster()
    {
        if (Input.GetButton("Fire1"))
        {
            if (_ThrusterFuel > 0)
            {
                _C._PlayerRB.AddForce(Vector3.up * _ThrusterForce.Evaluate(_ThrusterFuel / _MaxThrusterFuel) * 15);
                Debug.DrawLine(_C._Camera.transform.position + _C._Camera.transform.forward, _C._Camera.transform.position + _C._Camera.transform.forward + (Vector3.up * _ThrusterForce.Evaluate(_ThrusterFuel / _MaxThrusterFuel) * 1.5f));
                _ThrusterFuel -= _ThrusterDepleteRate * Time.fixedDeltaTime;
                
                _ThrusterFuel = Mathf.Clamp(_ThrusterFuel, 0, _MaxThrusterFuel);
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
        string GaugeText = null;

        for (int i = 0; i < _GaugeLength; i++)
        {
            if (_ThrusterFuel > (_MaxThrusterFuel / _GaugeLength * i))
                GaugeText += "|";
            else
                GaugeText += ".";
        }

        _ThrusterGauge.text = "Thruster Fuel :" +
                        "\n" + "[" + GaugeText + "]";
    }

}
