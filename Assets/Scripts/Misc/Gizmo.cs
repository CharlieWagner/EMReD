using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    [SerializeField]
    private Mesh _mesh;

    [SerializeField]
    private Color _GizmoColor;

    [SerializeField]
    private float _Scale;

    [SerializeField]
    private Vector3 _heading;


    private void OnDrawGizmos()
    {
        Gizmos.color = _GizmoColor;
        Gizmos.DrawMesh(_mesh, transform.position, transform.rotation * Quaternion.Euler(_heading * 90), Vector3.one * _Scale);
    }
}
