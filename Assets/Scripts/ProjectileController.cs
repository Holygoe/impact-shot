using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Rigidbody rb;
    public ProjectileOption option;
    private const float velocity = 10;
        
    private void Start()
    {
        Destroy(gameObject, 5);
    }

    public void Initialize(Vector3 direction, ProjectileOption option)
    {
        rb.velocity = direction * velocity;
        this.option = option;
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherRigidbody = other.GetComponent<Rigidbody>();
        var thisRigidbody = GetComponent<Rigidbody>();
        if (otherRigidbody && thisRigidbody)
        {
            var zombieCollider = otherRigidbody.GetComponent<ZombieCollider>();
            if (zombieCollider)
            {
                zombieCollider.Hit();
            }
            otherRigidbody.AddForce(thisRigidbody.velocity.normalized * option.force);
            //GameManager.SlowDown();

        }
        Destroy(gameObject);
    }
}
