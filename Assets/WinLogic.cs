using System.Collections.Generic;
using UnityEngine;

public class WinLogic : MonoBehaviour
{
    public enum GameState
    {
        PLAY,
        WON,
        LOST,
        MENU,
        PAUSE,
        LANDING
    }

    public List<WinObserver> winObservers = new List<WinObserver>();

    [SerializeField] private TimeCounter timeCounter = null;
    [SerializeField] private PlayerMovement playerMovement = null;
    [SerializeField] private CanvasGroup wonCanvasGroup = null;
    [SerializeField] private CanvasGroup lostCanvasGroup = null;
    [SerializeField] private Surface surface = null;

    public GameState gameState { get
        {
            return _gameState;
        }
        set
        {
            _gameState = value;
            UpdateObserver();
        }}
    private GameState _gameState;
    public void UpdateObserver()
    {
        foreach(var observer in winObservers)
        {
            observer.Update();
        }
    }
    public void WinGame()
    {
        wonCanvasGroup.alpha = 1;
        wonCanvasGroup.interactable = true;
        wonCanvasGroup.blocksRaycasts = true;

        gameState = GameState.WON;
        timeCounter.pause = true;
    }

    public void StartLanding()
    {
        gameState = GameState.LANDING;
    }

    public void LostGame()
    {
        lostCanvasGroup.alpha = 1;
        lostCanvasGroup.interactable = true;
        lostCanvasGroup.blocksRaycasts = true;

        gameState = GameState.LOST;
        timeCounter.pause = true;
    }

    private void Update()
    {
        if (_gameState == GameState.WON || _gameState == GameState.LOST)
            InputCheck();
    }

    private void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            surface.CreateSurface();

            playerMovement.SetPosition(playerMovement.startLanderPosition);
            playerMovement.SetRotation(Quaternion.identity);
            playerMovement.ResetMovingForce();

            if (gameState == GameState.WON)
            {
                wonCanvasGroup.alpha = 0;
                wonCanvasGroup.interactable = false;
                wonCanvasGroup.blocksRaycasts = false;
            }
            else if (gameState == GameState.LOST)
            {
                lostCanvasGroup.alpha = 0;
                lostCanvasGroup.interactable = false;
                lostCanvasGroup.blocksRaycasts = false;
            }

            gameState = GameState.PLAY;
            timeCounter.Reset();
        }
    }
}
