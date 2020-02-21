using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField]
    [Range(-.1f,1f)]
    private float _dissolveAmount;

    private Rigidbody RB;
    private Animator Anim;
    private Material Mat;

    private void Start()
    {
        Mat = GetComponent<Renderer>().material;
        RB = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        Mat.SetFloat("_Visi", _dissolveAmount);

        if (!RB.isKinematic)
        {
            Anim.SetBool("Boom", true);
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
