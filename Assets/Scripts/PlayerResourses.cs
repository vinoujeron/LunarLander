using UnityEngine;

public class PlayerResourses : MonoBehaviour
{
    [Range(0f, 300f)] [SerializeField] private float fuelConsumptionPerSecond = 0f;

    [Range(0f, 5000f)] [SerializeField] private float fuelCapacity;
    
    public PlayerResoursesObservable playerResoursesObservable;

    private void Awake()
    {
        playerResoursesObservable = new PlayerResoursesObservable();
    }

    public void ConsumeFuel()
    {
        if (fuelCapacity - fuelConsumptionPerSecond * Time.deltaTime >= 0)
            fuelCapacity -= fuelConsumptionPerSecond * Time.deltaTime;
        else
            playerResoursesObservable.Update();
    }

    public void ConsumeFuelFixedDelta()
    {
        if (fuelCapacity - fuelConsumptionPerSecond * Time.fixedDeltaTime >= 0) 
            fuelCapacity -= fuelConsumptionPerSecond * Time.fixedDeltaTime;
        else
            playerResoursesObservable.Update();
    }
}