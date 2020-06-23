using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCollider : MonoBehaviour
{
    public ZombieController zombieController;

    public void Hit()
    {
        zombieController.Hit();
    }
}
