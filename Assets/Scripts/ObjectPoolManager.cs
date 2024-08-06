using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    private class PoolingObject
    {
        public string objectName;
        public GameObject objectPrefab;
        public int defaultCount;
    }
    
    public static ObjectPoolManager ObjectPoolManagerInstance;
    public IObjectPool<GameObject> ObjectPool { get; private set; }

    [SerializeField] private PoolingObject[] poolingObjects = null;

    private Dictionary<string, IObjectPool<GameObject>> objPoolDic = new Dictionary<string, IObjectPool<GameObject>>();
    private Dictionary<string, GameObject> goDic = new Dictionary<string, GameObject>();
    private string targetObjectName;
    
    void Awake()
    {
        if (ObjectPoolManagerInstance == null)
        {
            ObjectPoolManagerInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < poolingObjects.Length; i++)
        {
            IObjectPool<GameObject> objectPool = new ObjectPool<GameObject>(
                CreatePooledObject, OnTakeFromPool, OnReturnToPool,
                OnDestroyPooledObject, true, 
                poolingObjects[i].defaultCount, 
                poolingObjects[i].defaultCount);

            if (goDic.ContainsKey(poolingObjects[i].objectName))
            {
                Debug.LogFormat("{0} : 오브젝트풀에 이미 등록된 오브젝트입니다.", poolingObjects[i].objectName);
                continue;
            }
            
            goDic.Add(poolingObjects[i].objectName, poolingObjects[i].objectPrefab);
            objPoolDic.Add(poolingObjects[i].objectName, objectPool);
            
            for (int j = 0; j < poolingObjects[i].defaultCount; j++)
            {
                targetObjectName = poolingObjects[i].objectName;
                PooledObject pooledObject = CreatePooledObject().GetComponent<PooledObject>();
                pooledObject.ObjectPool.Release(pooledObject.gameObject);
            }
            
            Debug.LogFormat("{0} : 오브젝트풀 추가 완료.", poolingObjects[i].objectName);
        }
        
        Debug.Log("오브젝트풀 준비 완료.");
    }

    private GameObject CreatePooledObject()
    {
        GameObject gameObjectPooled = Instantiate(goDic[targetObjectName]);
        gameObjectPooled.GetComponent<PooledObject>().ObjectPool = objPoolDic[targetObjectName];
        return gameObjectPooled;
    }

    private void OnTakeFromPool(GameObject gameObjectPooled)
    {
        gameObjectPooled.SetActive(true);
    }

    private void OnReturnToPool(GameObject gameObjectPooled)
    {
        gameObjectPooled.SetActive(false);
    }

    private void OnDestroyPooledObject(GameObject gameObjectPooled)
    {
        Destroy(gameObjectPooled);
    }

    public GameObject GetPooledGameObject(string goName)
    {
        targetObjectName = goName;

        if (goDic.ContainsKey(targetObjectName) == false)
        {
            Debug.LogFormat("{0} : 오브젝트풀에 등록되지 않은 오브젝트입니다.", targetObjectName);
            return null;
        }

        return objPoolDic[targetObjectName].Get();
    }
}
