using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private WinLogic winLogic = null;
    [SerializeField] private PlayerResourses playerResourses = null;
    
    [Header("Sprites")]
    [SerializeField] private GameObject upSprite = null;
    [SerializeField] private GameObject leftSprite = null;
    [SerializeField] private GameObject rightSprite = null;

    private PlayerResoursesObserver _playerResoursesObserver;
    private WinObserver _winObserver;
    private bool _enabled = true;

    private void Start()
    {
        _playerResoursesObserver = new PlayerResoursesObserver(playerResourses.playerResoursesObservable);  
        _playerResoursesObserver.SetOnUpdateAction(() => 
        {
            if (_playerResoursesObserver.obsorvableValue <= 0)
            {
                leftSprite.SetActive(false);
                rightSprite.SetActive(false);
                upSprite.SetActive(false);
                _enabled = false;
            }
        });

        _winObserver = new WinObserver(winLogic);
        _winObserver.SetOnUpdateAction(() =>
        {
            var state = winLogic.gameState;
            if (state == WinLogic.GameState.LOST
            || state == WinLogic.GameState.WON
            || state == WinLogic.GameState.LANDING)
            {
                leftSprite.SetActive(false);
                rightSprite.SetActive(false);
                upSprite.SetActive(false);
                _enabled = false;
            }
            else if (state == WinLogic.GameState.PLAY)
            {
                leftSprite.SetActive(false);
                rightSprite.SetActive(false);
                upSprite.SetActive(false);
                _enabled = true;
            }
            else
                _enabled = true;
        });
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (!_enabled)
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
