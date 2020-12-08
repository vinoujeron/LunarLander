using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(WinLogic))]
[RequireComponent(typeof(PlayerResourses))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private LandingCheck landingCheck = null;
    [SerializeField] private WinLogic winLogic = null;
    [SerializeField] private PlayerResourses playerResourses = null;

    [Header("Lander settings")]
    public Vector3 startLanderPosition;
    [Range(0f, 180f)] [SerializeField] private float rotationPower = 0f;
    [Range(0f, 1000f)] [SerializeField] private float thrustPower = 10f;
    [Range(0f, -1000f)] [SerializeField] private float rotatePushPower = 10f;
    [SerializeField] private Rigidbody2D _rigidbody2D = null;

    private PlayerResoursesObserver _playerResoursesObserver;
    private WinObserver _winObserver;
    private bool _enabled = true;

    public void SetPosition(Vector3 newPosition)
    {
        gameObject.transform.position = newPosition;
    }

    public void SetRotation(Quaternion newRotation)
    {
        gameObject.transform.rotation = newRotation;
    }

    public void ResetMovingForce()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0f;
    }

    public Vector3 GetDeltaSpeed()
    {
        return _rigidbody2D.velocity;
    }

    private void Start()
    {
        _playerResoursesObserver = new PlayerResoursesObserver(playerResourses.playerResoursesObservable);
        _playerResoursesObserver.SetOnUpdateAction(() => {
            if (_playerResoursesObserver.obsorvableValue <= 0)
                _enabled = false;
        });

        _winObserver = new WinObserver(winLogic);
        _winObserver.SetOnUpdateAction(() =>
        {
            var state = winLogic.gameState;
            if (state == WinLogic.GameState.WON
            || state == WinLogic.GameState.LOST
            || state == WinLogic.GameState.LANDING)
            {
                _enabled = false;
            }
            else
                _enabled = true;
        });
    }

    private void Update()
    {
        ManageInput();
    }

    private void ManageInput()
    {
        if (!_enabled)
            return;

        if (Input.GetKey(KeyCode.Space))
            ApplyThrust(thrustPower);
    }
    private void FixedUpdate()
    {
        if (!_enabled)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        if (Math.Abs(horizontal) > 0)
            ApplyRotationBoost(horizontal);
    }

    private void ApplyThrust(float _thrustPower)
    {
        playerResourses.ConsumeFuel();
        _rigidbody2D.AddRelativeForce(new Vector2(0f, _thrustPower));
    }
    
    private void ApplyRotationBoost(float inputAxis)
    {
        playerResourses.ConsumeFuelFixedDelta();
        _rigidbody2D.AddRelativeForce(new Vector2(inputAxis * rotatePushPower, 0f));
        _rigidbody2D.AddTorque(inputAxis * rotationPower);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            landingCheck.Check();
        }
    }
}
