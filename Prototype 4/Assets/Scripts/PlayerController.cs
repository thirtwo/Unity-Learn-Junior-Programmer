using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    Normal,
    Rockets,
    Smash
}
public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 10f;
    private GameObject focalPoint;
    public bool hasPowerup = false;
    public float powerupStrength = 15.0f;
    public GameObject powerupIndicator;
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private float smashAnimSpeed = 3f;
    [SerializeField] private float smashRange = 4f;
    [SerializeField] private float smashPower = 2f;
    private bool onSmashAnim;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.4f, 0);
        powerupIndicator.transform.parent = transform;
    }

    void Update()
    {
        if (onSmashAnim)
        {
            return;
        }
        float forwardInput = Input.GetAxis("Vertical");
        if (hasPowerup)
        {
            playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed * powerupStrength);
        }
        else
        {
            playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            StartCoroutine(PowerupCountdownRoutine(other.GetComponent<PowerUp>().powerUpType));
            Destroy(other.gameObject);
            powerupIndicator.SetActive(true);
        }

    }

    IEnumerator PowerupCountdownRoutine(PowerUpType powerUp)
    {
        PowerUp(powerUp);
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);

        }
    }
    private void PowerUp(PowerUpType powerUp)
    {
        if (powerUp == PowerUpType.Normal)
        {
            hasPowerup = true;
        }
        else if (PowerUpType.Rockets == powerUp)
        {
            HomingRocket();
        }
        else if (PowerUpType.Smash == powerUp)
        {
            Smash();
        }
    }
    private Enemy[] enemies;
    private void HomingRocket()
    {
        enemies = FindObjectsOfType<Enemy>();
        StartCoroutine(FireRockets(enemies.Length, enemies));
    }
    private IEnumerator FireRockets(int enemyLength, Enemy[] enemy)
    {
        int missileCount = enemyLength;
        while (missileCount > 0)
        {
            GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            if (enemy[--missileCount] != null)
                missile.GetComponent<Rocket>().FollowEnemy(enemy[missileCount].transform);

            yield return new WaitForSeconds(0.1f);
        }
        powerupIndicator.SetActive(false);
    }
    private void Smash()
    {
        onSmashAnim = true;
        StartCoroutine(SmashAnim());
    }

    private IEnumerator SmashAnim()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + new Vector3(0, 10, 0);
        float distance = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float timer = (Time.time - startTime) * smashAnimSpeed;
        float fractionOfJourney = timer / distance;
        while (fractionOfJourney < 1)
        {
            timer = (Time.time - startTime) * smashAnimSpeed;
            fractionOfJourney = timer / distance;
            transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }
        startPos = transform.position;
        endPos = transform.position + new Vector3(0, -10, 0);
        distance = Vector3.Distance(startPos, endPos);
        startTime = Time.time;
        timer = (Time.time - startTime) * smashAnimSpeed;
        fractionOfJourney = timer / distance;
        while (fractionOfJourney < 1)
        {
            timer = (Time.time - startTime) * smashAnimSpeed;
            fractionOfJourney = timer / distance;
            transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }
        SmashEnemies();
        onSmashAnim = false;
    }
    private void SmashEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        enemies = EnemiesInRange(enemies,smashRange);
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].enemyRb.AddExplosionForce(smashPower,transform.position,smashRange,0f,ForceMode.Impulse);
        }
    }

    private Enemy[] EnemiesInRange(Enemy[] enemies, float range)
    {
        List<Enemy> enemiesList = new List<Enemy>(enemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector3.Distance(transform.position, enemies[i].transform.position) > range)
            {
                enemiesList.Remove(enemies[i]);
            }
        }
        return enemiesList.ToArray();
    }
}
