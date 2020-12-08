using UnityEngine;
using UnityEngine.UI;

public class SetAltitudeText : MonoBehaviour
{
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private LayerMask groundLayer = 0;
    [SerializeField] private Text altitudeInfo = null;
    private DistanceCalculator distanceCalculator;
    private bool _isOnPause;

    private void Start()
    {
        distanceCalculator = new DistanceCalculator(playerTransform, groundLayer);
    }

    private void Update()
    {
        if (_isOnPause)
            return;

        CalculateDistance();
    }

    private void CalculateDistance()
    {
        distanceCalculator.SetGroundDistanceInfo(ref altitudeInfo);
    }

    public void OnGroundHit()
    {
        _isOnPause = true;
        altitudeInfo.text = "0";
    }

    public void OnGroundExit()
    {
        _isOnPause = false;
    }
}
