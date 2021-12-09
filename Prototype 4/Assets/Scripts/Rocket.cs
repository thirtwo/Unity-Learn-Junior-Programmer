using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Transform _enemy = null;

    void Update()
    {
        if(_enemy == null)
        {
            return;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _enemy.position, 0.2f);
        }
    }

    public void FollowEnemy(Transform enemy)
    {
        _enemy = enemy;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
