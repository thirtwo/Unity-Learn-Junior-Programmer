using UnityEngine;
using UnityEngine.UI;

public class DetectCollision : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private Slider slider;
    private int _maxHealth;

    private void Start()
    {
        if (slider == null)
        {
            return;
        }
        slider.maxValue = health;
        _maxHealth = health;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            health--;
            slider.value = _maxHealth - health;
            if (health <= 0)
            {
                SpawnManager.scoreHandler?.Invoke(1);
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
