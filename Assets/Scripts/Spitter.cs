using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spitter : MonoBehaviour
{
    [SerializeField] private GameObject spitProjectile;
    [SerializeField] private float spitCooltime;

    private float counterCooltime = 0.0f;

    void FixedUpdate()
    {
        counterCooltime += Time.fixedDeltaTime;

        if (spitCooltime < counterCooltime)
        {
            Spitting();
            counterCooltime = 0;
        }
    }

    private void Spitting()
    {
        GameObject projectile = Instantiate(spitProjectile, 
            this.transform.position,
            this.transform.rotation);
    }
}
