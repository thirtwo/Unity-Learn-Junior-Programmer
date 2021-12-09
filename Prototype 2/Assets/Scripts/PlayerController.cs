using UnityEngine;
using System;
public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    private float _verticalInput;
    public float speed = 10f;
    public float xRange = 10f;
    public float zRange = 13f;
    public GameObject projectilePrefab;
    [SerializeField] private int lives = 3;//default;

    public static Action UseLiveHandler;

    private void Start()
    {
        UseLiveHandler += UseLives;
    }
    void Update()
    {

        GetInputs();
        Movement();
        ClampPlayer();
        HitPizza();


    }

    void GetInputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    void Movement()
    {
        transform.Translate(Vector3.right * (horizontalInput * Time.deltaTime * speed));
        transform.Translate(Vector3.forward * (_verticalInput * Time.deltaTime * speed));
    }

    void HitPizza()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Instantiate(projectilePrefab, transform.position, transform.rotation);
            //Bu yöntemle sadece tek açıya gönderir. Oluştururken oluşturanın açısına bakmaz Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }

    void ClampPlayer()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
    }

    private void UseLives()
    {
        if(lives > 0)
        {
            lives--;
            Debug.Log("Lives: "+ lives);
        }
        else
        {
            Debug.Log("Game Over");
        }
    }
}
