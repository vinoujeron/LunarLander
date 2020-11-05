using UnityEngine;

public class PlayerResourses : MonoBehaviour
{
    [Range(0f, 300f)] [SerializeField] private float fuelConsumptionPerSecond = 0f;

    [Range(0f, 5000f)] [SerializeField] private float fuelCapacity = 0f;

    public bool IsFuelLeft()
    {
        return fuelCapacity - fuelConsumptionPerSecond * Time.deltaTime >= 0;
    }
    public void ConsumeFuel()
    {
        if (fuelCapacity - fuelConsumptionPerSecond * Time.deltaTime >= 0) 
            fuelCapacity -= fuelConsumptionPerSecond * Time.deltaTime;
    }
}
