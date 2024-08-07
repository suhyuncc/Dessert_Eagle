using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitProjectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifetime;

    private Vector2 dir;

    private void Start()
    {
        Transform eagleTransform = GameObject.FindGameObjectWithTag("Bird").transform;
        
        dir = eagleTransform.position - transform.position;
        // float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0, 0, rotZ);
        
        Destroy(this.gameObject, projectileLifetime);
    }

    private void FixedUpdate()
    {
        this.transform.Translate(dir.normalized * projectileSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bird"))
        {
            // 체력 감소 처리 위치
            
            Destroy(this.gameObject);
        }
    }
}
