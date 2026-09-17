using UnityEngine;

public class TooglePanel : MonoBehaviour

{
    public GameObject musicPanel;
    public GameObject buttonsPanel;

    public void ShowButtons()
    {
        musicPanel.SetActive(false);
        buttonsPanel.SetActive(true);
    }

    public void ShowMusic()
    {
        musicPanel.SetActive(true);
        buttonsPanel.SetActive(false);
    }
}