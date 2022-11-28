using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaged : MonoBehaviour
{
    void Start()
    {
        GetComponent<AnimationChanger>().ChangeAnimation("WheelBot-Move");
    }

    void Update()
    {
        Animator animator = GetComponent<Animator>();

        if (GetComponent<Health>().beingDamaged)
        {
            GetComponent<AnimationChanger>().ChangeAnimation("Damaged");
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Damaged") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            GetComponent<Health>().beingDamaged = false;
            GetComponent<AnimationChanger>().ChangeAnimation("WheelBot-Move");
        }
    }
}
