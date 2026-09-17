using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public string url = "https://example.com"; // Ganti dengan URL kamu

    public void OpenWebsite()
    {
        Application.OpenURL(url);
    }
}
