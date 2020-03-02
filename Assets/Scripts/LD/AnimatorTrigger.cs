using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTrigger : MonoBehaviour
{
    
    [SerializeField]
    private string _propertyName;

    [SerializeField]
    [Tooltip("0 means trigger, 1 means boolean")]
    [Range(0, 1)]
    private int _propertyType;

    [SerializeField]
    private bool _stateToSet;

    [SerializeField]
    private Animator _targetController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_propertyType == 0)
            {
                _targetController.SetTrigger(_propertyName);
            } else if (_propertyType == 1)
            {
                _targetController.SetBool(_propertyName, _stateToSet);
            }
        }
    }
}
