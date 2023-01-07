using System;
using UnityEngine;

public class Machine : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;

    public void SetVelocity(Vector3 velocity)
    {
        rigidbody2D.AddForce(velocity);
    }
}