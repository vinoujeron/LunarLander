using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    [SerializeField] private SetAltitudeText setAltitudeText = null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            setAltitudeText.OnGroundHit();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            setAltitudeText.OnGroundExit();
        }
    }
}
