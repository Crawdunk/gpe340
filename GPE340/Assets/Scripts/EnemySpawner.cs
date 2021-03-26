using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float spawnDelay;
    public int currentActiveEnemies;
    public int maxActiveEnemies;
    
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnDelay);
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.isPaused)
            return;

        if (currentActiveEnemies >= maxActiveEnemies)
        {
            CancelInvoke("SpawnEnemy");
        }
    }

    private void SpawnEnemy()
    {
        if (currentActiveEnemies >= maxActiveEnemies)
            return;
   
        Instantiate(enemyPrefab, transform.position, transform.rotation);
        currentActiveEnemies++;
    }
}
