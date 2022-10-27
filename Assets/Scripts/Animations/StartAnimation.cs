using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("offset", Random.Range(0f, animator.GetCurrentAnimatorClipInfo(0).Length));
    }
}
