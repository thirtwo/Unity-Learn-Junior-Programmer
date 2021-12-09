using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float speed = 5f;
    private float turnSpeed = 25f;
    private float horizontalInput;
    private float forwardInput;
    [SerializeField] private GameObject[] cameras;
    [Range(1,2)][SerializeField] private int playerID = 1; //default;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerID == 1)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal2");
            forwardInput = Input.GetAxis("Vertical2");
        }
        //power for vehicle
        // transform.Translate(0, 0, 1); ilkel
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        //transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);  arabayı çevirmeden dönüş
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
        ChangeCamera();
    }

    private void ChangeCamera()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            cameras[0].SetActive(!cameras[0].activeSelf);
            cameras[1].SetActive(!cameras[1].activeSelf);
        }
    }
}
