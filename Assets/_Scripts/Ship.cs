using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float movementSpeedMultiplier = 1f;

    [SerializeField] private Projector projector;
    private Camera mainCamera;

    [SerializeField] private float launchForceMultiplier = 10f;

    [SerializeField] private float launchCooldown = 1f;
    private float launchCooldownTimer;

    private void Start()
    {
        mainCamera = Camera.main;
        launchCooldownTimer = launchCooldown;
    }

    private void Update()
    {
        launchCooldownTimer -= Time.deltaTime;

        HandleAiming();
        HandleMovement();
    }

    private void HandleAiming()
    {
        var worldMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition).NewZ(0);
        var startPosition = transform.position;
        var velocity = (worldMousePos - startPosition).normalized * launchForceMultiplier;

        if (Input.GetMouseButton(0) && launchCooldownTimer < 0)
        {
            ShootMachine(velocity);
        }
        else
        {
            projector.SimulateTrajectory(ResourceManager.Instance.saw, startPosition, velocity);
        }
    }

    private void HandleMovement()
    {
        var xOffset = Input.GetAxis("Horizontal");
        var yOffset = Input.GetAxis("Vertical");

        rigidbody2D.velocity = new Vector2(xOffset, yOffset) * movementSpeedMultiplier;
    }

    private void ShootMachine(Vector2 velocity)
    {
        var machineObj = Instantiate(ResourceManager.Instance.saw,
            transform.position,
            Quaternion.identity);

        machineObj.SetVelocity(velocity);

        launchCooldownTimer = launchCooldown;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Branch")) Destroy(col.gameObject);
    }
}