using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController instance;
    public List<Animator> animators = new();
    private void Awake()
    {
        instance = this;
    }

    public void AnimationStart()
    {
        foreach (Animator animator in animators)
        {
            animator.enabled = true;
        }

        animators = new List<Animator>();
    }

}
