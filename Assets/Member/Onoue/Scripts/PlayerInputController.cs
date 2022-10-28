using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputController : MonoBehaviour
{
    PlayerInput _playerInput;
    Player _player;
    private void Awake()
    {
        TryGetComponent(out _playerInput);
        TryGetComponent(out _player);
    }
    void OnEnable()
    {
        _playerInput.actions["Jump"].started += OnJump;
        _playerInput.actions["Attack"].started += OnAttack;
    }

    private void OnDisable()
    {
        _playerInput.actions["Jump"].started -= OnJump;
        _playerInput.actions["Attack"].started -= OnAttack;
    }
    private void FixedUpdate()
    {
        var direction = _playerInput.actions["Move"].ReadValue<Vector2>();
        //_player.PlayerMove(direction);
        _player.Direction = direction;
    }
    private void Setup()
    {

    }
    private void OnJump(InputAction.CallbackContext obj)
    {
        _player.Jump();
    }

    private void OnAttack(InputAction.CallbackContext obj)
    {
        _player.Attack();
    }
}