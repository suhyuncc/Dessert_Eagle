using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private float genTime = 0.0f;
    private string[] monsterList = { "Earthworm", "EarthwormMother", "EarthwormFather" };

    void Update()
    {
        genTime += Time.deltaTime;

        if (genTime > 1.5f)
        {
            
            GameObject enemyGo = ObjectPoolManager.ObjectPoolManagerInstance.GetPooledGameObject(monsterList[Random.Range(0, 3)]);
            enemyGo.transform.position = this.transform.position;
            genTime = 0.0f;
        }
    }
}
