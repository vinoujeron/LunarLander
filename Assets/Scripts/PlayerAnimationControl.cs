using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    [SerializeField] private GameObject upSprite = null;
    [SerializeField] private GameObject leftSprite = null;
    [SerializeField] private GameObject rightSprite = null;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
            leftSprite.SetActive(true);
        if (Input.GetKeyDown(KeyCode.D))
            rightSprite.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space))
            upSprite.SetActive(true);
        
        if (Input.GetKeyUp(KeyCode.A))
            leftSprite.SetActive(false);
        if (Input.GetKeyUp(KeyCode.D))
            rightSprite.SetActive(false);
        if (Input.GetKeyUp(KeyCode.Space))
            upSprite.SetActive(false);
    }
}
