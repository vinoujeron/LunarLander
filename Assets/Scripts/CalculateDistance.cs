using UnityEngine;
using UnityEngine.UI;

public class CalculateDistance
{
    private Transform t_firstObject;
    private Transform t_secondObject;
    [SerializeField] private float maxRaycastDistance = 10000f;
    private LayerMask distanceObjectLayer = 0;

    public CalculateDistance(Transform t_firstObject, LayerMask distanceObjectLayer)
    {
        this.distanceObjectLayer = distanceObjectLayer;
        this.t_firstObject = t_firstObject;
    }
    
    public CalculateDistance(Transform t_firstObject, Transform t_secondObject, LayerMask distanceObjectLayer)
    {
        this.distanceObjectLayer = distanceObjectLayer;
        this.t_firstObject = t_firstObject;
        this.t_secondObject = t_secondObject;
    }

    public void SetGroundDistanceInfo(ref Text text)
    {
        text.text = Mathf.FloorToInt(GetDistanceFromObjToGround(t_firstObject)).ToString();
    }

    private float GetDistanceFromObjToGround(Transform obj)
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(obj.position, Vector2.down, maxRaycastDistance, distanceObjectLayer);
        return Mathf.Abs(obj.position.y - raycastHit2D.point.y);
    }
}
