using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SFXmusicVlomuw : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;

    public void SetMusicVolume()
    {
        float volume = Mathf.Log10(Mathf.Clamp(musicSlider.value, 0.0001f, 1)) * 20f;
        myMixer.SetFloat("sfx", volume); // pastikan "music" adalah nama parameter yang benar
    }
}