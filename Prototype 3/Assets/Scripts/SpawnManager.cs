using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25,0,0);
    private float startDelay = 1;
    private float spawnRate = 3;
    private PlayerController playerControllerScript;
    public delegate void DashHandler();
    public static DashHandler StartDash;
    public static DashHandler EndDash;
    
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, spawnRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
        {
            int obstacleIndex = Random.Range(0, 3);
            Instantiate(obstaclePrefab[obstacleIndex], spawnPos, obstaclePrefab[obstacleIndex].transform.rotation);
        }
    }
}
