using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerSpinner : MonoBehaviour
{
    public float propellerSpeed = 3600f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * propellerSpeed * propellerSpeed * propellerSpeed * propellerSpeed);
    }
}
