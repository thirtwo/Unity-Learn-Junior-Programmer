using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int collideCounter = 0;
    private void OnMouseDown()
    {
        StartCoroutine(TurnCo());
    }

    private IEnumerator TurnCo()
    {
        float startTime = Time.time;
        float countTime = 0.2f;
        while (collideCounter <= 0 && Time.time < startTime+countTime)
        {
            transform.Rotate(Vector3.right);
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Obstacle>() != null)
        {
             collideCounter++;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetComponent<Obstacle>())
        {
            collideCounter--;
        }
    }

}
