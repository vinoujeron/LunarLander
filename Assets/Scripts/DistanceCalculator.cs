using UnityEngine;
using UnityEngine.UI;

public class DistanceCalculator
{
    private Transform firstTransform;
    private Transform secondTransform;
    [SerializeField] private float maxRaycastDistance = 10000f;
    private LayerMask distanceObjectLayer = 0;

    public DistanceCalculator(Transform t_firstObject, LayerMask distanceObjectLayer)
    {
        this.distanceObjectLayer = distanceObjectLayer;
        this.firstTransform = t_firstObject;
    }
    
    public DistanceCalculator(Transform t_firstObject, Transform t_secondObject, LayerMask distanceObjectLayer)
    {
        this.distanceObjectLayer = distanceObjectLayer;
        this.firstTransform = t_firstObject;
        this.secondTransform = t_secondObject;
    }

    public void SetGroundDistanceInfo(ref Text text)
    {
        text.text = Mathf.FloorToInt(GetDistanceFromObjToGround()).ToString();
    }

    public float GetDistanceFromObjToGround()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(firstTransform.position, Vector2.down, maxRaycastDistance, distanceObjectLayer);
        return Mathf.Abs(firstTransform.position.y - raycastHit2D.point.y);
    }
}
