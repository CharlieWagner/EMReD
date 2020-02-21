using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserExplode : MonoBehaviour
{
    private Rigidbody[] _SubRocks =  new Rigidbody[25];

    [SerializeField]
    private float ExplosionForce;
    [SerializeField]
    private GameObject _ExplodeFX;
    private int _SubRocksCount;
    

    private void Start()
    {
        _SubRocksCount = transform.childCount - 1;

        for (int i = 0; i <= _SubRocksCount; i++)
        {
            _SubRocks[i] = transform.GetChild(i).GetComponent<Rigidbody>();
        }
        
    }

    public void Explode()
    {
        for (int i = 0; i <= _SubRocksCount - 1; i++)
        {
            

            _SubRocks[i].isKinematic = false;

            _SubRocks[i].AddForce((_SubRocks[i].transform.position - transform.position) * ExplosionForce);

            _SubRocks[i].transform.parent = null;
        }

        Instantiate(_ExplodeFX, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
