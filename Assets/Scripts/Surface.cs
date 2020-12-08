using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
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
    private List<Vector2> _generatedPoints = new List<Vector2>();


    // TEST
    [Header("TEMPORALY IN TEST")]
    [SerializeField] private MeshFilter meshFilter = null;
    // TEST
    
    public void CreateSurface()
    {
        GeneratePoints(startSurfaceXLeft, startSurfaceXRight);
        GenerateSurface();
    }

    /// <summary>
    /// Generate surface using LineRenderer and edgeCollider2D
    /// </summary>
    private void GenerateSurface()
    {
        List<Vector2> points = new List<Vector2>();
        lineRenderer.positionCount = _generatedPoints.Count;
        lineRenderer.widthMultiplier = lineWidth;

        for (int i = 0; i < _generatedPoints.Count; ++i)
        {
            Vector2 point = _generatedPoints[i];
            points.Add(point);
            lineRenderer.SetPosition(i, point);
        }
        edgeCollider2D.points = points.ToArray();

        _generatedPoints[0] = (new Vector2(_generatedPoints[0].x, _generatedPoints[0].y - 100));
        _generatedPoints.Add(new Vector2(_generatedPoints[_generatedPoints.Count - 1].x, _generatedPoints[_generatedPoints.Count - 1].y - 100));

        //// Triangulation of the surface below lineRenderer
        Triangulator tr = new Triangulator(_generatedPoints.ToArray());
        int[] indices = tr.Triangulate();
        Vector3[] vertices = new Vector3[_generatedPoints.Count];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(_generatedPoints[i].x, _generatedPoints[i].y, 0);
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter.mesh = mesh;
    }

    private void GeneratePoints(float leftEndX, float rightEndX)
    {
        // Clear previous points
        _generatedPoints.Clear();

        // Start with generating leftMostPoint
        Vector2 newPoint = new Vector2(leftEndX, Random.Range(deltaYDown, deltaYTop));
        _generatedPoints.Add(newPoint);
        
        while (newPoint.x < rightEndX)
        {
            float randomX = Random.Range(newPoint.x + deltaXLeft, newPoint.x + deltaXRight);
            float randomY = Random.Range(deltaYDown, deltaYTop);
            
            // Randomly choose case where would spawn point with y1 = y2
            newPoint = RandomGenerator.GetRandomBool(10) ? new Vector2(randomX, newPoint.y)
                : new Vector2(randomX, randomY);
            
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
