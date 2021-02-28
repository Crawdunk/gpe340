using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemy;
    public Vector3 enemySpawnPoint;
    public bool enemyIsSpawned;
    public int timeToRespawn;

    void Update()
    {
        if (enemyIsSpawned == false)
        {
            StartCoroutine(SpawnAlien(timeToRespawn));
            enemyIsSpawned = true;
        }
        else if (enemyIsSpawned == true)
        {
            StopCoroutine(SpawnAlien(timeToRespawn));
        }
    }

    public IEnumerator SpawnAlien(int respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);
        Instantiate(enemy, enemySpawnPoint, Quaternion.identity);
    }

    public IEnumerator KillAlien()
    {
        Destroy(GameObject.FindWithTag("Enemy"));
        enemyIsSpawned = false;
        yield break;
    }

}
