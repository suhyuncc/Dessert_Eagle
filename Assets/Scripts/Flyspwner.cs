using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyspwner : MonoBehaviour
{
    [SerializeField]
    private int _spawnCool;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        _spawnCool = (int)Random.Range(50.0f, 100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > _spawnCool)
        {
            GameObject enemyGo = ObjectPoolManager.ObjectPoolManagerInstance.GetPooledGameObject("Fly");
            enemyGo.transform.position = new Vector3(80f,Random.Range(-10f,25f),0f);
            timer = 0;
            _spawnCool = (int)Random.Range(50.0f, 100.0f);
        }
    }
}
