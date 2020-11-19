using UnityEngine;

public class PlayerResourses : MonoBehaviour
{
    [Range(0f, 300f)] [SerializeField] private float fuelConsumptionPerSecond = 0f;

    [Range(0f, 5000f)] [SerializeField] private float fuelCapacity;
    
    public PlayerResoursesObservable playerResoursesObservable;

    private void Awake()
    {
        playerResoursesObservable = new PlayerResoursesObservable(fuelCapacity);
    }

    public void ConsumeFuel()
    {
        if (fuelCapacity - fuelConsumptionPerSecond * Time.deltaTime >= 0)
            fuelCapacity -= fuelConsumptionPerSecond * Time.deltaTime;
        else
            fuelCapacity = 0;

        playerResoursesObservable.Update(fuelCapacity);
    }

    public void ConsumeFuelFixedDelta()
    {
        if (fuelCapacity - fuelConsumptionPerSecond * Time.fixedDeltaTime >= 0)
            fuelCapacity -= fuelConsumptionPerSecond * Time.fixedDeltaTime;
        else
            fuelCapacity = 0;

        playerResoursesObservable.Update(fuelCapacity);
    }
}