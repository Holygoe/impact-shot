using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Transform rightShoulder;
    public Transform barrel;
    public ProjectileController projectilePrefab;
    public ProjectileOption projectileOption;
    
    private const float ShootRate = 0.1f;

    private float _armLength;
    private Camera _mainCamera;
    private float _shootDelayCounter;

    private void Start()
    {
        _armLength = (barrel.position - rightShoulder.position).magnitude;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (TryRaycast(out var hit))
        {
            var rightShoulderPosition = rightShoulder.position;
            var aimDirection = hit.point - rightShoulderPosition;
            barrel.position = aimDirection.normalized * _armLength + rightShoulderPosition;
            barrel.LookAt(hit.point);
        }

        if (_shootDelayCounter > 0)
        {
            _shootDelayCounter -= Time.deltaTime;
        }
        else if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            _shootDelayCounter = ShootRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!TryRaycast(out var hit)) return;
        var projectile = Instantiate(projectilePrefab, barrel.position, barrel.rotation);
        
        projectile.Initialize(barrel.forward, projectileOption);
    }

    private bool TryRaycast(out RaycastHit hit)
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray.origin, ray.direction, out hit);
    }
}
