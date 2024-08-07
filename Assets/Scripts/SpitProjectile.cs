using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitProjectile : MonoBehaviour
{
    [SerializeField] 
    private float projectileSpeed;
    [SerializeField] 
    private float projectileLifetime;
    [SerializeField]
    private AudioSource audio;

    private void Start()
    {
        audio.Play();
        Destroy(this.gameObject, projectileLifetime);
    }

    private void FixedUpdate()
    {
        this.transform.Translate(new Vector2(-1.0f,-2.0f) * projectileSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bird"))
        {
            // 체력 감소 처리 위치
            GameManager.instance.GetDamage(5);
            Destroy(this.gameObject);
        }
    }
}
