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

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayer;

    private void OnEnable()
    {
        GetComponent<ControlsReader>().AttackEvent += OnAttack;
        GetComponent<ControlsReader>().RoundAttackEvent += OnRoundAttack;
    }

    void Update()
    {
        AttackCooldown();
    }

    private void OnAttack()
    {
        Animator animator = GetComponent<Animator>();
        PlayerState.currentPlayerState = PlayerState.CurrentPlayerState.ATTACK;
        float realCd = animator.GetCurrentAnimatorStateInfo(0).length + 0.1f;

        if (attackIndex == minAttackIndex && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            GetComponent<AnimationChanger>().ChangeAnimation($"Attack{attackIndex}");
            ChangeAttackIndex();
            attackCdToCancelCombo = realCd;
            Attack();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 &&
        !animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName($"Attack{attackIndex - 1}"))
        {
            GetComponent<AnimationChanger>().ChangeAnimation($"Attack{attackIndex}");
            ChangeAttackIndex();
            attackCdToCancelCombo = realCd;
            Attack();
        }
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            IDamageable damageable = enemy.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(1);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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

    private void OnRoundAttack()
    {
        Animator animator = GetComponent<Animator>();
        PlayerState.currentPlayerState = PlayerState.CurrentPlayerState.ATTACK;
        float realCd = animator.GetCurrentAnimatorStateInfo(0).length + 0.1f;

        if (attackIndex == minAttackIndex)
        {
            GetComponent<AnimationChanger>().ChangeAnimation($"Attack4");
            attackCdToCancelCombo = realCd;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 &&
        !animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName($"Attack{attackIndex - 1}"))
        {
            GetComponent<AnimationChanger>().ChangeAnimation($"Attack4");
            attackCdToCancelCombo = realCd;
        }
    }

    private void OnDisable()
    {
        GetComponent<ControlsReader>().AttackEvent -= OnAttack;
        GetComponent<ControlsReader>().RoundAttackEvent -= OnRoundAttack;
    }
}
