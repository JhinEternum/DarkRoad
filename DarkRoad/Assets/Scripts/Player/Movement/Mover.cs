using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float externalForces = 1;
    [SerializeField] private bool lookingLeft = false;

    void LateUpdate()
    {
        ControlsReader controls = GetComponent<ControlsReader>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (controls.MovementValue.x == 0 && CanMove())
        {
            GetComponent<AnimationChanger>().ChangeAnimation("Idle");
            rb.velocity = Vector2.zero;
        }
        else if (controls.MovementValue.x != 0)
        {
            float clampedMovementValue = controls.MovementValue.x > 0 ? Mathf.Ceil(controls.MovementValue.x) : Mathf.Floor(controls.MovementValue.x);

            if (CanMove())
            {
                GetComponent<AnimationChanger>().ChangeAnimation("Run");
            }

            rb.velocity = new Vector2(clampedMovementValue * speed * externalForces, 0);

            FlipCharacter();
        }
    }

    private void FlipCharacter()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (!lookingLeft && GetComponent<ControlsReader>().MovementValue.x < 0)
        {
            rb.transform.localScale = new Vector3(-rb.transform.localScale.x, rb.transform.localScale.y, rb.transform.localScale.z);
            lookingLeft = true;
        }
        else if (lookingLeft && GetComponent<ControlsReader>().MovementValue.x > 0)
        {
            rb.transform.localScale = new Vector3(-rb.transform.localScale.x, rb.transform.localScale.y, rb.transform.localScale.z);
            lookingLeft = false;
        }
    }

    private bool CanMove()
    {
        if (PlayerState.currentPlayerState == PlayerState.CurrentPlayerState.ATTACK ||
        PlayerState.currentPlayerState == PlayerState.CurrentPlayerState.JUMP)
        {
            return false;
        }

        return true;
    }
}
