using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        settingsPanel.SetActive(false); // Sembunyikan panel settings saat mulai
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MenuMap"); // Ganti dengan nama scene utama
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Keluar dari permainan...");
        Application.Quit();
    }
}
