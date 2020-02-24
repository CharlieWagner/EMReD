using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject Target;

    public void Switch()
    {
        CoreController _Ctrl;
        if (Target.TryGetComponent<CoreController>(out _Ctrl))
        {
            _Ctrl.initRotate();
        } else
        {
            Target.GetComponent<Animator>().SetTrigger("_Trigger");
        }
    }
}
