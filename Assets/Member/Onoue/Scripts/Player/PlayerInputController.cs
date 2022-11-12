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
        _playerInput.actions["Fire"].started += OnFire;
        _playerInput.actions["Fusion"].started += OnFusion;
        _playerInput.actions["SetMaterial"].started += OnSet;

    }

    private void OnDisable()
    {
        _playerInput.actions["Jump"].started -= OnJump;
        _playerInput.actions["Attack"].started -= OnAttack;
        _playerInput.actions["Fire"].started -= OnFire;
        _playerInput.actions["Fusion"].started -= OnFusion;
        _playerInput.actions["SetMaterial"].started -= OnSet;
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
    private void OnFire(InputAction.CallbackContext obj)
    {
        _player.Fire();
    }
    private void OnFusion(InputAction.CallbackContext obj)
    {
        _player.Fusion();
    }
    private void OnSet(InputAction.CallbackContext obj)
    {
        SetMaterial(obj);
    }
    private void SetMaterial(InputAction.CallbackContext obj)
    {
        var get = obj.action.actionMap["SetMaterial"].ReadValue<Vector2>();
        if (get == Vector2.up)
        {
            _player.SetMaterialID(RawMaterialID.BombBean);
        }
        else if (get == Vector2.down)
        {

        }
        else if (get == Vector2.left)
        {
            _player.SetMaterialID(RawMaterialID.PowerPlant);
        }
        else if (get == Vector2.right)
        {

        }
    }
}