﻿using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Scripts/FPS Input")]
public class FPSInput : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const float Gravity = -9.8f;
    private const float BaseSpeed = 6.0f;

    [SerializeField]
    private float _speed = BaseSpeed;
    
    private CharacterController _characterController;

    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SpeedChanged, OnSpeedChaged);
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SpeedChanged, OnSpeedChaged);
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float deltaX = Input.GetAxis(Horizontal) * _speed;
        float deltaZ = Input.GetAxis(Vertical) * _speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, _speed);

        movement.y = Gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);

        _characterController.Move(movement);
    }

    private void OnSpeedChaged(float value)
    {
        _speed = BaseSpeed * value;
    }
}
