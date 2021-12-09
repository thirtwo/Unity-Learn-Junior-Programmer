using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public PlayerController playerController;
    public Transform startingPoint;
    
    Vector3 startPos;
    Vector3 endPos;
    [SerializeField] private float lerpSpeed;
    private void Start()
    {
        playerController.gameOver = true;
        StartCoroutine(PlayAnimation());
        
    }

    private IEnumerator PlayAnimation()
    {
        startPos = playerController.transform.position;
        endPos = startingPoint.position;
        float startTime = Time.time;
        float distance = Vector3.Distance(startPos, endPos);
        float timer = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney =  timer / distance; //geçen zaman bölü yol 

        while(fractionOfJourney < 1)
        {

            timer = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = timer / distance;
            playerController.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }
        playerController.gameOver = false;

    }
}
