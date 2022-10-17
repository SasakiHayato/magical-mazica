using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputController : MonoBehaviour
{
    PlayerInput _playerInput;
    //InputAction jumpAction;

    Player _player;
    private void Awake()
    {
        TryGetComponent(out _playerInput);
        TryGetComponent(out _player);
        //jumpAction = _playerInput.currentActionMap.FindAction("Jump");
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
        _player.PlayerMove(direction);
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