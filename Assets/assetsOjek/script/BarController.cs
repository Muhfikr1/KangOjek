using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    public GameObject targetBar; // Assign panel/bar yang ingin ditampilkan

    private void Start()
    {
        if (targetBar != null)
        {
            BarManager.instance.RegisterBar(targetBar); // Daftarkan bar ke BarManager
        }
    }

    public void OnButtonClick()
    {
        if (targetBar != null)
        {
            BarManager.instance.ShowBar(targetBar); // Panggil BarManager untuk mengontrol tampilan bar
        }
    }
}
