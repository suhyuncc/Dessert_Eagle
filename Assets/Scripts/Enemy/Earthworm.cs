using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthworm : PooledObject
{
    [SerializeField]
    private float speed;

    void Update()
    {
        if (this.transform.position.x < -80)
        {
            ReleaseToPool();
        }

        this.transform.localPosition += Vector3.left * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bird"))
        {
            ReleaseToPool();
        }
    }
}
