using UnityEngine;

public class SurfacePoint
{
    public SurfacePoint (float x, float y) {
        _x = x;
        _y = y;
    }

    public float x
    {
        get => _x;
    }
    public float y
    {
        get => _y;
    }

    public Vector2 GetVector2()
    {
        return new Vector2(x, y);
    }
    
    public bool visible { get; set; }
    private float _x;
    private float _y;
}
