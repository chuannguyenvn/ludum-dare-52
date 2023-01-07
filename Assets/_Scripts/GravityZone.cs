using System;
using Shapes;
using UnityEngine;

public class GravityZone : MonoBehaviour
{
    [SerializeField] private Disc disc;
    [SerializeField] private CircleCollider2D circleCollider2D;

    private float magnitude;

    public void Init(float radius, float magnitude)
    {
        disc.Radius = radius;
        circleCollider2D.radius = radius;
        this.magnitude = magnitude;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        other.attachedRigidbody.AddForce((transform.position - other.transform.position).normalized *
                                         magnitude);
    }
}