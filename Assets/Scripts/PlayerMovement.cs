using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerResourses playerResourses = null;
    [SerializeField] private Transform rotationObjectTransform = null;
    
    [Header("Lander settings")]
    [Range(0f, 180f)] [SerializeField] private float maxRotationAngle = 15f;
    [Range(0f, 1000f)] [SerializeField] private float thrustPower = 10f;
    [Range(0f, -1000f)] [SerializeField] private float rotatePushPower = 10f;
    [SerializeField] private Rigidbody2D _rigidbody2D = null;

    private void Update()
    {
        ManageInput();
    }

    private void ManageInput()
    {
        if (Input.GetKey(KeyCode.Space) && playerResourses.IsFuelLeft())
            ApplyThrust(thrustPower);
    }
    private void FixedUpdate()
    {
        ApplyRotationBoost(Input.GetAxis("Horizontal"));
    }

    private void ApplyThrust(float _thrustPower)
    {
        playerResourses.ConsumeFuel();
        _rigidbody2D.AddRelativeForce(new Vector2(0f, _thrustPower));
    }
    
    private void ApplyRotationBoost(float inputAxis)
    {
        _rigidbody2D.AddForce(new Vector2(inputAxis * rotatePushPower, 0f));
        rotationObjectTransform.rotation = Quaternion.Euler(0f, 0f, inputAxis * maxRotationAngle);
    }
}
