using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private string volumeParameter;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider audioSlider;

    // Start is called before the first frame update
    void Start()
    {
        audioSlider.value = PlayerPrefs.GetFloat(volumeParameter, audioSlider.value);
    }

    public void SetVolume(float sliderValue)
    {
        // Set audioMixer volume
        // Note: audioMixer volume scale is logarithmic but slider scale is linear -> transform slider scale
        audioMixer.SetFloat(volumeParameter, Mathf.Log10(sliderValue) * 30f);
    }

    private void OnDisable()
    {
        //Save to PlayerPrefs
        PlayerPrefs.SetFloat(volumeParameter, audioSlider.value);
    }
}
