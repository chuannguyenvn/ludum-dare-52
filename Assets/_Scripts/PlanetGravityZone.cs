using System;
using Shapes;
using UnityEngine;
using UnityEngine.Serialization;

public class PlanetGravityZone : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Disc disc;
    [SerializeField] private CircleCollider2D circleCollider2D;
    public float Magnitude;

    public void Init(float radius, float magnitude)
    {
        disc.Radius = radius;
        circleCollider2D.radius = radius;
        Magnitude = magnitude;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var force = (transform.position - other.transform.position).normalized * Magnitude;
        if (layerMask == (layerMask | 1 << other.gameObject.layer))
        {
            other.attachedRigidbody.AddForce(-force);
        }
        else
        {
            other.attachedRigidbody.AddForce(force);
        }
    }
}