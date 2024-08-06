using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private int _spawnRate;
    private bool _spawncheck;
    private string[] monsterList = { "Earthworm", "EarthwormMother", "EarthwormFather" };

    void Update()
    {

        if (_spawncheck)
        {
            GameObject enemyGo = ObjectPoolManager.ObjectPoolManagerInstance.GetPooledGameObject(monsterList[Random.Range(0, 3)]);
            enemyGo.transform.position = this.transform.position;
            _spawncheck = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("CheckBar"))
        {
            Checking();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("CheckBar"))
        {
            Checking();
        }
    }

    // Ȯ���� ���� ���� ������ ���� ���� ����
    private void Checking()
    {
        float check = Random.Range(0, 1.0f);

        //Ȯ�� ������
        if (check < (float)_spawnRate / 100.0f)
        {
            _spawncheck = true;
        }
        else
        {
            _spawncheck = false;
        }
    }
}
