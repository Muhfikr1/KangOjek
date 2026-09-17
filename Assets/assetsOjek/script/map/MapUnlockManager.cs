using UnityEngine;
using UnityEngine.UI;

public class MapUnlockManager : MonoBehaviour
{
    public Button[] mapButtons; // Assign di Inspector
    private bool[] isUnlocked; // Sekarang private agar diinisialisasi dengan benar

    void Awake()
    {
        if (mapButtons == null || mapButtons.Length == 0)
        {
            Debug.LogError("Map buttons belum di-assign di Inspector!");
            return;
        }

        isUnlocked = new bool[mapButtons.Length]; // Inisialisasi sesuai jumlah tombol
    }

    void Start()
    {
        LoadMapStatus();

        for (int i = 0; i < mapButtons.Length; i++)
        {
            if (!isUnlocked[i])
            {
                mapButtons[i].interactable = false; // Disable tombol jika belum terbuka
                mapButtons[i].GetComponentInChildren<Text>().text = "Locked";
            }
            else
            {
                mapButtons[i].interactable = true;
                mapButtons[i].GetComponentInChildren<Text>().text = "Unlocked";
            }
        }
    }

    public void UnlockMap(int index)
    {
        if (index < 0 || index >= isUnlocked.Length)
        {
            Debug.LogError("Index map tidak valid!");
            return;
        }

        isUnlocked[index] = true;
        PlayerPrefs.SetInt("Map" + index, 1); // Simpan status ke PlayerPrefs
        PlayerPrefs.Save();

        mapButtons[index].interactable = true;
        mapButtons[index].GetComponentInChildren<Text>().text = "Unlocked";
    }

    void LoadMapStatus()
    {
        for (int i = 0; i < isUnlocked.Length; i++)
        {
            isUnlocked[i] = PlayerPrefs.GetInt("Map" + i, 0) == 1;
        }
    }
}
