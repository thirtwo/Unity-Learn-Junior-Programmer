using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 20f;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        SpawnManager.StartDash += IncreaseSpeed;
        SpawnManager.EndDash += DecreaseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (transform.position.x < -10 && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseSpeed()
    {
        speed = 40;
    }
    public void DecreaseSpeed()
    {
        speed = 20;
    }
    private void OnDestroy()
    {
        SpawnManager.StartDash -= IncreaseSpeed;
        SpawnManager.EndDash -= DecreaseSpeed;
    }
}
