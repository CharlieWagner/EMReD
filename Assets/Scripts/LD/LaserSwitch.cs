using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject Target;

    private Renderer _Renderer;

    private bool _switched = false;
    private float _opening = 1;

    private void Start()
    {
        _Renderer = GetComponent<Renderer>();

        _Renderer.material.SetFloat("_Opening", _opening);
    }

    private void Update()
    {
        if (_switched)
            _opening -= Time.deltaTime * .6f;

        _opening = Mathf.Clamp(_opening, 0, 1);

        _Renderer.material.SetFloat("_Opening", _opening);
    }

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

        _switched = true;
    }
}
