using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChanger : MonoBehaviour
{
    [SerializeField] private string currentAnimation;

    public void ChangeAnimation(string newAnimation)
    {
        int stateId = Animator.StringToHash(newAnimation);
        bool hasState = GetComponent<Animator>().HasState(0, stateId);

        if (!hasState)
        {
            Debug.LogWarning($"The '{newAnimation}' does not exist as an animation! Please, check the animation name!");
            return;
        }

        if (newAnimation == currentAnimation)
        {
            Debug.Log("Same animation");
            return;
        }

        GetComponent<Animator>().Play(newAnimation);

        currentAnimation = newAnimation;
    }
}
