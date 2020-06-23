using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public Transform rightShoulder;
    public Transform barrel;
    public Rigidbody projectilePrefab;
    public float shootRate = 0.1f;

    private float armLength;
    private Camera mainCamera;
    private float shootDelayCounter;
    private float projectileForce = 50000;

    private void Start()
    {
        armLength = (barrel.position - rightShoulder.position).magnitude;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (TryRaycast(out var hit))
        {
            var rightShoulderPosition = rightShoulder.position;
            var aimDirection = hit.point - rightShoulderPosition;
            barrel.position = aimDirection.normalized * armLength + rightShoulderPosition;
            barrel.LookAt(hit.point);
        }

        if (shootDelayCounter > 0)
        {
            shootDelayCounter -= Time.deltaTime;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            shootDelayCounter = shootRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!TryRaycast(out var hit)) return;
        var projectile = Instantiate(projectilePrefab, barrel.position, barrel.rotation);
        projectile.AddForce(barrel.forward * projectileForce);
    }

    private bool TryRaycast(out RaycastHit hit)
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray.origin, ray.direction, out hit);
    }
}
