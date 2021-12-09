using UnityEngine;

public class CarAI : MonoBehaviour
{

    [Range(0.5f, 10)] [SerializeField] private float speed = 1f; //default
    
    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }
}
