using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : MonoBehaviour
{
    
    void Awake()
    {
        UseGravity(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseGravity(true);
        }
    }

    void UseGravity(bool use)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Rigidbody>().useGravity = use;
        }
    }
}
