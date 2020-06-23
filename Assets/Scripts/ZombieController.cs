using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public Animator animator;

    public void Hit()
    {
        if (animator.enabled)
        {
            StartCoroutine(GameManager.SlowDown());
        }
        animator.enabled = false;
    }
}
