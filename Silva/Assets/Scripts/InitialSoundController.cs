using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class InitialSoundController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private List<Slider> sliders;   
    [SerializeField] private List<string> volumeParameters;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<sliders.Count; i++)
        {
            float audioVolume = PlayerPrefs.GetFloat(volumeParameters[i], sliders[i].value);
            audioMixer.SetFloat(volumeParameters[i], Mathf.Log10(audioVolume) * 30f);
        }
    }
}
