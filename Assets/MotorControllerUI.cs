using UnityEngine;

public class MotorControllerUI : MonoBehaviour
{
    private DriverCar driverCar;

    public void OnPressForward()
    {
        driverCar.StartMoveForward();
    }

    public void OnReleaseForward()
    {
        driverCar.StopMoveForward();
    }

    public void OnPressBackward()
    {
        driverCar.StartMoveBackward();
    }

    public void OnReleaseBackward()
    {
        driverCar.StopMoveBackward();
    }

    public void SetDriver(DriverCar driver)
    {
        driverCar = driver;
    }
}
