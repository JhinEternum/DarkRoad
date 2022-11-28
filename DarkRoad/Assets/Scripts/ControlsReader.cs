using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class ControlsReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }
    public event Action AttackEvent;
    public event Action RoundAttackEvent;
    public event Action JumpEvent;
    public event Action ReleasedJumpEvent;
    public event Action DashEvent;

    private Controls controls;

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (context.interaction is HoldInteraction)
        {
            RoundAttackEvent?.Invoke();
        }
        else if (context.interaction is PressInteraction)
        {
            AttackEvent?.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpEvent?.Invoke();
        }
        else if (context.canceled)
        {
            ReleasedJumpEvent?.Invoke();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        DashEvent?.Invoke();
    }
}
