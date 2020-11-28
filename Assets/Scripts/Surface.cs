using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(EdgeCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class Surface : MonoBehaviour
{
    [Header("Delta X")]
    [Range(0.1f, 25f)] [SerializeField] private float deltaXLeft = 1f;    
    [Range(0.1f, 25f)] [SerializeField] private float deltaXRight = 5f;
    
    [Header("Delta Y")]
    [Range(-24f, 25f)] [SerializeField] private float deltaYTop = 0f;    
    [Range(-25f, 24f)] [SerializeField] private float deltaYDown = 10f;
    
    [Header("Start Surface Generation")]
    [Range(-5000f, 4999f)] [SerializeField] private float startSurfaceXLeft = 10f;
    [Range(-4999f, 5000f)] [SerializeField] private float startSurfaceXRight = 250f;

    [Header("Line")] 
    [Range(0f, 1f)] [SerializeField] private float lineWidth = 0.2f;
    
    [SerializeField] private LineRenderer lineRenderer = null;
    [SerializeField] private EdgeCollider2D edgeCollider2D = null;
    private List<SurfacePoint> _generatedPoints = new List<SurfacePoint>();
    
    public void CreateSurface()
    {
        GeneratePoints(startSurfaceXLeft, startSurfaceXRight);
        GenerateSurface();
    }

    private void GenerateSurface()
    {
        List<Vector2> points = new List<Vector2>();
        lineRenderer.positionCount = _generatedPoints.Count;
        lineRenderer.widthMultiplier = lineWidth;
        
        for (int i = 0; i < _generatedPoints.Count; ++i)
        {
            Vector2 point = _generatedPoints[i].GetVector2();
            points.Add(point);
            lineRenderer.SetPosition(i, point);
        }
        edgeCollider2D.points = points.ToArray();
    }

    private void GeneratePoints(float leftEndX, float rightEndX)
    {
        // Clear previous points
        _generatedPoints.Clear();

        // Start with generating leftMostPoint
        SurfacePoint newPoint = new SurfacePoint(leftEndX, Random.Range(deltaYDown, deltaYTop));
        _generatedPoints.Add(newPoint);
        
        while (newPoint.x < rightEndX)
        {
            float randomX = Random.Range(newPoint.x + deltaXLeft, newPoint.x + deltaXRight);
            float randomY = Random.Range(deltaYDown, deltaYTop);
            
            // Randomly choose case where would spawn point with y1 = y2
            newPoint = RandomGenerator.GetRandomBool(10) ? new SurfacePoint(randomX, newPoint.y)
                : new SurfacePoint(randomX, randomY);
            
            _generatedPoints.Add(newPoint);
        }
    } 
    

    // private void OnValidate()
    // {
    //     _generatedPoints.Clear();
    //     GeneratePoints(startSurfaceXLeft, startSurfaceXRight);
    //     GenerateSurface(startSurfaceXLeft, startSurfaceXRight);
    // }
}
