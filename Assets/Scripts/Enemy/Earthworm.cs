using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthworm : PooledObject
{
    private float moveSpeed = 5.0f;
    
    void Update()
    {
        if (this.transform.position.x < -10)
        {
            ReleaseToPool();
        }
        
        this.transform.Translate(Vector3.left * this.moveSpeed * Time.deltaTime);
    }
}
