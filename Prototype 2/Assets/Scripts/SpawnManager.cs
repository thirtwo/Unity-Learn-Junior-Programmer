using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private int spawnPosX = 20;
    private int spawnPosZh = 20;
    private int spawnPosZl = 12;
    private int startDelay = 2;
    private float spawnInterval = 1.5f;
    private int spawnPosZMax = 13;
    private int spawnPosZMin = 0;
    private int spawnPosX2 = 20;
    private int score = 0;

    public static System.Action<int> scoreHandler;

    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
        scoreHandler += AddScore;
        scoreHandler += ShowScore;
    }



    void SpawnRandomAnimal()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnPosX, spawnPosX), 0, Random.Range(spawnPosZl, spawnPosZh));
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
        spawnPos = new Vector3(spawnPosX2, 0, Random.Range(spawnPosZMin, spawnPosZMax));
        animalIndex = Random.Range(0, animalPrefabs.Length);
        Instantiate(animalPrefabs[animalIndex], spawnPos, Quaternion.Euler(0, 270, 0));
    }

    private void ShowScore(int _score)
    {
        Debug.Log("Score is: " + score);
    }

    private void AddScore(int _score)
    {
        score += _score;
    }
}
