using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MotorShop : MonoBehaviour
{
    public int playerCoins = 5000;
    public TMP_Text coinsText;

    [System.Serializable]
    public class Motor
    {
        public string motorName;
        public int price;
        public Button buyButton;
        public bool isBought = false;
    }

    public Motor[] motors;

    void Start()
    {
        UpdateUI();
        foreach (Motor motor in motors)
        {
            motor.buyButton.onClick.AddListener(() => BuyMotor(motor));
        }
    }

    void BuyMotor(Motor motor)
    {
        if (playerCoins >= motor.price && !motor.isBought)
        {
            playerCoins -= motor.price;
            motor.isBought = true;
            motor.buyButton.interactable = false;
            Debug.Log(motor.motorName + " telah dibeli!");
            UpdateUI();
        }
        else if (motor.isBought)
        {
            Debug.Log(motor.motorName + " sudah kamu miliki!");
        }
        else
        {
            Debug.Log("Uangmu kurang!");
        }
    }

    void UpdateUI()
    {
        coinsText.text = "Coins: " + playerCoins;
    }
}
