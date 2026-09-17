using UnityEngine;
using UnityEngine.UI;

public class MotorItem : MonoBehaviour
{
    public GameObject motorModel;
    public Button buyButton;
    public int price;
    private MotorManager manager;

    public void Initialize(MotorManager manager, bool active)
    {
        this.manager = manager;
        SetActive(active);
        buyButton.onClick.AddListener(Buy);
    }

    public void SetActive(bool active)
    {
        motorModel.SetActive(active);
        buyButton.gameObject.SetActive(active);
    }

    public void Buy()
    {
        Debug.Log("Bought motor for " + price);
        // Tambah logic pembelian di sini
        buyButton.interactable = false;
    }
}
