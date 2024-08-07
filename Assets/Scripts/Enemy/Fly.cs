using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : PooledObject
{
    [SerializeField]
    private int increasing_hp;
    [SerializeField]
    private float speed;

    void Update()
    {
        if (this.transform.position.x < -80)
        {
            ReleaseToPool();
        }

        this.transform.localPosition += Vector3.left * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bird"))
        {
            ReleaseToPool();
            GameManager.instance.GetHealth(increasing_hp);
        }
    }
}
