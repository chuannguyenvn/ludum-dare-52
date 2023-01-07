using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
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

        HandleMovement();
    }

    private void HandleMovement()
    {
        var xOffset = Input.GetAxis("Horizontal");
        var yOffset = Input.GetAxis("Vertical");

        rigidbody2D.velocity = new Vector2(xOffset, yOffset) * movementSpeedMultiplier;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(col.gameObject);
    }
}