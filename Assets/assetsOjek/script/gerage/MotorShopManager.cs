using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MotorShopManager : MonoBehaviour
{
    [SerializeField] private Image[] priceImages; // Gambar harga
    [SerializeField] private Sprite[] priceSprites; // Sprite untuk harga (harus sesuai urutan harga)
    [SerializeField] private int[] motorPrices = { 0, 1000, 2000, 3000 }; // Harga tiap motor
    [SerializeField] private GameObject[] buyButtons;
    [SerializeField] private GameObject[] selectButtons;
    [SerializeField] private TextMeshProUGUI coinText;

    private void Start()
    {
        UpdateShopUI();
    }

    private void UpdateShopUI()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        coinText.text = totalCoins.ToString(); // Display only the total coins

        for (int i = 0; i < motorPrices.Length; i++)
        {
            if (MotorManager.IsMotorUnlocked(i))
            {
                buyButtons[i].SetActive(false);
                selectButtons[i].SetActive(true);
                if (priceImages[i] != null)
                    priceImages[i].enabled = false; // Sembunyikan gambar harga jika sudah unlock
            }
            else
            {
                buyButtons[i].SetActive(true);
                selectButtons[i].SetActive(false);
                if (priceImages[i] != null && priceSprites.Length > i)
                {
                    priceImages[i].sprite = priceSprites[i];
                    priceImages[i].enabled = true;
                }
            }
        }
    }
    public void BuyMotor(int motorID)
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        int price = motorPrices[motorID];

        if (totalCoins >= price)
        {
            totalCoins -= price;
            PlayerPrefs.SetInt("TotalCoins", totalCoins);
            MotorManager.UnlockMotor(motorID);
            UpdateShopUI();
        }
        else
        {
            Debug.Log("Koin tidak cukup!");
        }
    }

    public void SelectMotor(int motorID)
    {
        MotorManager.SelectMotor(motorID);
        Debug.Log("Motor " + motorID + " dipilih!");
    }
}
