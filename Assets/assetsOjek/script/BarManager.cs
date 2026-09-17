using UnityEngine;
using System.Collections.Generic;

public class BarManager : MonoBehaviour
{
    public static BarManager instance;
    private List<GameObject> allBars = new List<GameObject>();
    public GameObject defaultBar; // Bar yang aktif pertama kali (misal: Bar Monas)

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (defaultBar != null)
        {
            ShowBar(defaultBar); // Aktifkan bar default saat game dimulai
        }
    }

    public void RegisterBar(GameObject bar)
    {
        if (!allBars.Contains(bar))
        {
            allBars.Add(bar);
            bar.SetActive(false); // Semua bar disembunyikan dulu
        }
    }

    public void ShowBar(GameObject selectedBar)
    {
        foreach (GameObject bar in allBars)
        {
            if (bar != selectedBar)
                bar.SetActive(false); // Sembunyikan bar lain
        }
        selectedBar.SetActive(true); // Pastikan bar yang diklik muncul
    }
}
