using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGizmo : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source;

    [SerializeField]
    [Range(0, 100)]
    private float _MinRange;
    [SerializeField]
    [Range(0,100)]
    private float _MaxRange;

    [SerializeField]
    private bool _SelectedOnly;

    private void OnDrawGizmos()
    {
        if (!_SelectedOnly)
        {
            _source.minDistance = _MinRange;
            _source.maxDistance = _MaxRange;

            Gizmos.color = Color.yellow;

            Gizmos.DrawWireSphere(transform.position, _source.minDistance);
            Gizmos.DrawWireSphere(transform.position, _source.maxDistance);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_SelectedOnly)
        {
            _source.minDistance = _MinRange;
            _source.maxDistance = _MaxRange;

            Gizmos.color = Color.yellow;

            Gizmos.DrawWireSphere(transform.position, _source.minDistance);
            Gizmos.DrawWireSphere(transform.position, _source.maxDistance);
        }
    }
}
