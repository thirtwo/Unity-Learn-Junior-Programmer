using UnityEngine.UI;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Slider slider;
    public void ChangeVolume()
    {
        audioSource.volume = slider.value;
    }
}
