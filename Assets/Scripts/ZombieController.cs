using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public Animator animator;

    public void Hit()
    {
        if (animator.enabled)
        {
            GameManager.SlowDown();
        }
        animator.enabled = false;
    }
}
