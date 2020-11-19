using UnityEngine;
using UnityEngine.UI;

public class SetHSpeed : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Text valueText = null;
    [SerializeField] private GameObject arrow = null;

    private Vector3 _rightArrowScale;
    private Vector3 _leftArrowScale;
    private void Start()
    {
        // Caching scales of the arrow
        _rightArrowScale = arrow.transform.localScale;
        _leftArrowScale = new Vector3(_rightArrowScale.x * -1, _rightArrowScale.y, _rightArrowScale.z);
    }

    private void Update()
    {
        float v = rb.velocity.x;
        if (v > 0 && !arrow.activeSelf)
            arrow.SetActive(true);
        else if (v == 0 & arrow.activeSelf)
            arrow.SetActive(false);
        else if (v > 0.1f)
            arrow.transform.localScale = _rightArrowScale;
        else if (v < -0.1f)
            arrow.transform.localScale = _leftArrowScale;
        
        valueText.text = Mathf.Abs(v).ToString("N0");
    }
}
