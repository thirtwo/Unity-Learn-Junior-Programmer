using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 30;
    private float lowerBound = -10;
    
    void Update()
    {
        if(transform.position.z > topBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.z < lowerBound)
        {
            PlayerController.UseLiveHandler?.Invoke();
            Destroy(gameObject);
        }
    }
}
