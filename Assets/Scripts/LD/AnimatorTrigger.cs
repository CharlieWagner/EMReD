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

    [SerializeField]
    private float _delay = 00f;

    
    private TutorialManager tutorial;

    private void Awake()
    {
        tutorial = GameObject.FindGameObjectWithTag("TutorialManager").GetComponent<TutorialManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && tutorial.tutorialStep == 16)
        {
            Invoke("ChangeProperty", _delay);
        }
    }

    private void ChangeProperty()
    {
        if (_propertyType == 0)
        {
            _targetController.SetTrigger(_propertyName);
        }
        else if (_propertyType == 1)
        {
            _targetController.SetBool(_propertyName, _stateToSet);
        }
    }
}
