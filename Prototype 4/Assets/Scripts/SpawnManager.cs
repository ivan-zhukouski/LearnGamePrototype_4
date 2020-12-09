using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnPos = 9f;
    private float posX;
    private float posZ;
    public int enemyCount;
    public int waveNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
    }
    void SpawnEnemyWave( int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; i ++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
    void SpawnPowerups()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0)
        {
            SpawnPowerups();
            waveNumber ++;
            SpawnEnemyWave(waveNumber);
        }
    }
   private Vector3 GenerateSpawnPosition()
   {
    posX = Random.Range(-spawnPos, spawnPos);
    posZ = Random.Range(-spawnPos, spawnPos);

    Vector3 randomPos = new Vector3(posX,0,posZ);
    return randomPos;
   }
}
