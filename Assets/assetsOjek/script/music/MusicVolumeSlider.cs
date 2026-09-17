using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;

    // Metode ini dipanggil ketika slider volume berubah
    public void SetMusicVolume()
    {
        // Mengambil nilai slider dan menghitung volume dengan logaritma
        float volume = Mathf.Log10(Mathf.Clamp(musicSlider.value, 0.0001f, 1)) * 20f;

        // Menyimpan volume ke PlayerPrefs
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);

        // Mengatur volume untuk AudioMixer
        myMixer.SetFloat("music", volume); // pastikan "music" adalah nama parameter yang benar
    }

    // Memuat pengaturan volume saat scene dimuat
    void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            // Memuat volume yang telah disimpan sebelumnya
            float savedVolume = PlayerPrefs.GetFloat("MusicVolume");
            musicSlider.value = savedVolume;

            // Memastikan volume slider yang telah disimpan diterapkan
            SetMusicVolume();
        }

        // Menjaga agar objek ini tetap ada saat pindah scene
        DontDestroyOnLoad(gameObject);
    }
}
