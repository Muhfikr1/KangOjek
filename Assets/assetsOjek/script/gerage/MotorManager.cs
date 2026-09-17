using UnityEngine;

public class MotorManager : MonoBehaviour
{
    private const string PurchasedKey = "MotorPurchased_";
    private const string SelectedMotorKey = "SelectedMotor";

    public static bool IsMotorUnlocked(int motorID)
    {
        if (motorID == 0) return true; // Motor default gratis
        return PlayerPrefs.GetInt(PurchasedKey + motorID, 0) == 1;
    }

    public static void UnlockMotor(int motorID)
    {
        PlayerPrefs.SetInt(PurchasedKey + motorID, 1);
        PlayerPrefs.Save();
    }

    public static void SelectMotor(int motorID)
    {
        if (IsMotorUnlocked(motorID))
        {
            PlayerPrefs.SetInt(SelectedMotorKey, motorID);
            PlayerPrefs.Save();
        }
    }

    public static int GetSelectedMotor()
    {
        return PlayerPrefs.GetInt(SelectedMotorKey, 0);
    }
}
