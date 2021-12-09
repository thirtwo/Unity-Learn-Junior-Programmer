using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;
    private int _randomCounter;

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(enemyCount == 0)
        {
            SpawnEnemyWave(waveNumber);
            SpawnPowerup(waveNumber/2+1);
            waveNumber++;
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
    void SpawnEnemyWave(int enemysToSpawn)
    {
        
        for (int i = 0; i < enemysToSpawn; i++)
        {
            _randomCounter = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[_randomCounter], GenerateSpawnPosition(), enemyPrefabs[_randomCounter].transform.rotation);

        }
    }
    void SpawnPowerup(int powerupToSpawn)
    {
        for (int i = 0; i < powerupToSpawn; i++)
        {
            int randomNumber = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomNumber], GenerateSpawnPosition(), powerupPrefabs[randomNumber].transform.rotation);
        }
    }
   
}
