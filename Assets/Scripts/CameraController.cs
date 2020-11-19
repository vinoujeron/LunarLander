using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform followObjectTransform = null;
    [SerializeField] private Camera mainCamera = null;

    [Header("Offset")]
    [Range(-100f, 100f)] [SerializeField] private float xOffset = 0f;
    [Range(-100f, 100f)] [SerializeField] private float yOffset = 0f;
    
    private void Update()
    {
        SetCameraPosition(followObjectTransform.position);
    }

    private void SetCameraPosition(Vector3 newPosition)
    {
        Vector3 position = newPosition;
        mainCamera.transform.position =
            new Vector3((position.x + xOffset), (position.y + yOffset), -10f);
    }
}
