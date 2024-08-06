using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{
    public IObjectPool<GameObject> ObjectPool { get; set; }
    
    public void ReleaseToPool()
    {
        ObjectPool.Release(this.gameObject);
    }
}
