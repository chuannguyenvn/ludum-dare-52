using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launchpad : MonoBehaviour
{
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

        var worldMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition).NewZ(0);
        var startPosition = transform.position;
        var velocity = (worldMousePos - startPosition) * launchForceMultiplier;

        if (Input.GetMouseButton(0) && launchCooldownTimer < 0)
        {
            ShootMachine(velocity);
        }
        else
        {
            projector.SimulateTrajectory(ResourceManager.Instance.Machine, startPosition, velocity);
        }
    }

    private void ShootMachine(Vector2 velocity)
    {
        var machineObj = Instantiate(ResourceManager.Instance.Machine,
            transform.position,
            Quaternion.identity);

        machineObj.SetVelocity(velocity);

        launchCooldownTimer = launchCooldown;
    }
}