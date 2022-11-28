using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float healthPoints;
    private bool isDead = false;
    public bool beingDamaged;

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
        beingDamaged = true;

        if (healthPoints <= 0)
        {
            IsDead();
        }
    }

    public void IsDead()
    {
        if (isDead) return;

        isDead = true;
        transform.GetComponent<AnimationChanger>().ChangeAnimation("Dead");
    }
}
