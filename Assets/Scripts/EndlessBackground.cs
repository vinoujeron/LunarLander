using UnityEngine;

public class EndlessBackground : MonoBehaviour
{
    // Should be less that real radius of the background
    // to correct move background on new position
    [SerializeField] private float backgroundRadius = 0f;
    [SerializeField] private RectTransform bgRectTransform = null;

    [Header("Distance to ground")]
    [SerializeField] private DistanceCalculator distanceCalculator = null;
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private LayerMask groundLayer = 0;

    [Header("Delta")]
    [SerializeField] private PlayerMovement playerMovement = null;
    [SerializeField] private Vector2 deltaCoefficient = Vector2.one;


    private void Start()
    {
        distanceCalculator = new DistanceCalculator(playerTransform, groundLayer);
    }

    private void Update()
    {
        MoveBackground(playerMovement.GetDeltaSpeed());
    }

    public void MoveMenuBackground(Vector2 delta) 
    {
        Vector3 newPosition = bgRectTransform.position;
        if (newPosition.magnitude > backgroundRadius)
            newPosition *= -1;

        newPosition.x -= delta.x;
        newPosition.y -= delta.y;

        bgRectTransform.position = newPosition;
    }

    private void MoveBackground(Vector2 delta)
    {
        Vector3 newPosition = bgRectTransform.localPosition;
        if (newPosition.magnitude > backgroundRadius)
            newPosition *= -1;

        float distanceCoefficient = 0.001f + distanceCalculator.GetDistanceFromObjToGround() / 1000f;
        newPosition.x -= delta.x * deltaCoefficient.x * distanceCoefficient;
        newPosition.y -= delta.y * deltaCoefficient.y * distanceCoefficient;

        bgRectTransform.localPosition = newPosition;
    }
}
