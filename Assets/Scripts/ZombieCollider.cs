using UnityEngine;

public class ZombieCollider : MonoBehaviour
{
    public ZombieController zombieController;

    public void Hit()
    {
        zombieController.Hit();
    }
}
