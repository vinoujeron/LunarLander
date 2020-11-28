using System.Collections;
using UnityEngine;

public class LandingCheck : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Rigidbody2D playerRigidbody = null;
    [SerializeField] private WinLogic winLogic = null;

    [Header("Pivots")]
    [SerializeField] private Transform leftPivotTransform = null;
    [SerializeField] private Transform rightPivotTransform = null;

    [Header("Landing values")]
    [Range(0f, 1f)] [SerializeField] private float pivotsDifference = 0f;
    [Range(0f, 100f)] [SerializeField] private float maxHSpeed = 0f;
    [Range(0f, 100f)] [SerializeField] private float maxVSpeed = 0f;

    private WinObserver _winObserver;
    private bool _enabled = true;

    private void Start()
    {
        _winObserver = new WinObserver(winLogic);
        _winObserver.SetOnUpdateAction(() =>
        {
            if (winLogic.gameState == WinLogic.GameState.PLAY)
                _enabled = true;
            else
                _enabled = false;
        });
    }
    public void Check()
    {
        if (_enabled == false)
            return;

        if (CheckSpeed())
            StartLandingProcedure();
        else
            winLogic.LostGame();
    }

    private void StartLandingProcedure()
    {
        winLogic.StartLanding();
        StartCoroutine(LandingCoroutine(2f));
    }
    private bool CheckSpeed()
    {
        //Debug.Log($"HSpeed: {Mathf.Abs(playerRigidbody.velocity.x)}, VSpeed: {Mathf.Abs(playerRigidbody.velocity.y)}");
        return Mathf.Abs(playerRigidbody.velocity.x) < maxVSpeed
            && Mathf.Abs(playerRigidbody.velocity.y) < maxHSpeed;
    }

    private IEnumerator LandingCoroutine(float landingTime)
    {
        while(landingTime > 0)
        {
            yield return new WaitForSeconds(0.25f);
            landingTime -= 0.25f;
        }

        if (Mathf.Abs(leftPivotTransform.position.y - rightPivotTransform.position.y) < pivotsDifference)
            winLogic.WinGame();
        else 
            winLogic.LostGame();
    }
}
