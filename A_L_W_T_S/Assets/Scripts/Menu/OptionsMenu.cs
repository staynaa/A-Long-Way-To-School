using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider MasterSlider;


    public void setMusicVolume()
    {
        float volume = MasterSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume)*20);
    }
}
