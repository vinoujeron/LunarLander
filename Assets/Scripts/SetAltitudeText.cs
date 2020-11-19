using UnityEngine;
using UnityEngine.UI;

public class SetAltitudeText : MonoBehaviour
{
    [SerializeField] private Transform t_player = null;
    [SerializeField] private LayerMask groundLayer = 0;
    [SerializeField] private Text altitudeInfo = null;
    private CalculateDistance distanceCalculator;
    private bool _isOnPause;

    private void Start()
    {
        distanceCalculator = new CalculateDistance(t_player, groundLayer);
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
