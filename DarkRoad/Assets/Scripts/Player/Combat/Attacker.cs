using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private int attackIndex = 1;
    [SerializeField] private int minAttackIndex = 1;
    [SerializeField] private int maxAttackIndex = 3;
    [SerializeField] private float CdToCancelCombo = 0.75f;
    private float attackCdToCancelCombo;

    void Start()
    {

    }

    private void OnEnable()
    {
        GetComponent<ControlsReader>().AttackEvent += OnAttack;
    }

    void Update()
    {
        AttackCooldown();
    }

    private void OnAttack()
    {
        Animator animator = GetComponent<Animator>();
        PlayerState.currentPlayerState = PlayerState.CurrentPlayerState.ATTACK;
        float realCd = animator.GetCurrentAnimatorStateInfo(0).length + 0.2f;

        if (attackIndex == minAttackIndex && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            GetComponent<AnimationChanger>().ChangeAnimation($"Attack{attackIndex}");
            ChangeAttackIndex();
            attackCdToCancelCombo = realCd;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 &&
        !animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName($"Attack{attackIndex - 1}"))
        {
            GetComponent<AnimationChanger>().ChangeAnimation($"Attack{attackIndex}");
            ChangeAttackIndex();
            attackCdToCancelCombo = realCd;
        }
    }

    private void ChangeAttackIndex()
    {
        if (attackIndex < maxAttackIndex)
        {
            attackIndex++;
        }
        else
        {
            attackIndex = minAttackIndex;
        }
    }

    private void AttackCooldown()
    {
        if (attackCdToCancelCombo > 0)
        {
            attackCdToCancelCombo -= Time.deltaTime;
        }
        else
        {
            PlayerState.currentPlayerState = PlayerState.CurrentPlayerState.IDLE;
            attackIndex = minAttackIndex;
        }
    }

    private void OnDisable()
    {
        GetComponent<ControlsReader>().AttackEvent -= OnAttack;
    }
}
