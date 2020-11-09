using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    [SerializeField] private PlayerResourses playerResourses = null;
    
    [Header("Sprites")]
    [SerializeField] private GameObject upSprite = null;
    [SerializeField] private GameObject leftSprite = null;
    [SerializeField] private GameObject rightSprite = null;

    private PlayerResoursesObserver _playerResoursesObserver;
    private bool _isNoFuel = false;
    private void Start()
    {
        _playerResoursesObserver = new PlayerResoursesObserver(playerResourses.playerResoursesObservable);  
        _playerResoursesObserver.SetOnUpdateAction(() => 
        { 
            leftSprite.SetActive(false);
            rightSprite.SetActive(false);
            upSprite.SetActive(false);
            _isNoFuel = !_isNoFuel;
        });
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (_isNoFuel)
            return;
        
        if (Input.GetKeyUp(KeyCode.A))
            leftSprite.SetActive(false);
        if (Input.GetKeyUp(KeyCode.D))
            rightSprite.SetActive(false);
        if (Input.GetKeyUp(KeyCode.Space))
            upSprite.SetActive(false);

        if (Input.GetKeyDown(KeyCode.A))
            leftSprite.SetActive(true);
        if (Input.GetKeyDown(KeyCode.D))
            rightSprite.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space))
            upSprite.SetActive(true);
    }
}
