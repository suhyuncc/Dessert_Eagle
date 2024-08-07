using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthworm : PooledObject
{
    [SerializeField]
    private int increasing_hp;
    [SerializeField]
    private float speed;

    void Update()
    {
        speed = GameManager.instance.Earth_speed;

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
            if (collision.gameObject.GetComponent<Bird>().Iscrictic)
            {
                GameManager.instance.PopEffect(transform.localPosition);
            }

            ReleaseToPool();
            GameManager.instance.GetHealth(increasing_hp);
            
        }
    }
}
