using System;
using UnityEngine;

public class AnimationSync : MonoBehaviour
{
    private void Start()
    {
        AnimationController.instance.animators.Add(GetComponent<Animator>());
    }
}
