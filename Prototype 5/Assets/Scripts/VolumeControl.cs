using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    private AudioSource audioSource;
    //private Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //slider = GetComponent<Slider>();
        //slider.onValueChanged.AddListener(controlVolume);
    }

    // Update is called once per frame
    public void controlVolume(float value)
    {
        audioSource.volume = value;
    }
}
