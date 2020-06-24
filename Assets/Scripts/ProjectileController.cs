using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Rigidbody rb;
    public ProjectileOption option;
    
    private const float Velocity = 10;
        
    private void Start()
    {
        Destroy(gameObject, 5);
    }

    public void Initialize(Vector3 direction, ProjectileOption projectileOption)
    {
        rb.velocity = direction * Velocity;
        option = projectileOption;
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
        }
        Destroy(gameObject);
    }
}
