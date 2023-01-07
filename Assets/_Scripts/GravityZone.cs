using System;
using Shapes;
using UnityEngine;

public class GravityZone : MonoBehaviour
{
    [SerializeField] private Disc disc;
    [SerializeField] private CircleCollider2D circleCollider2D;

    public void SetRadius(float radius)
    {
        disc.Radius = radius;
        circleCollider2D.radius = radius;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        other.attachedRigidbody.AddForce(transform.position - other.transform.position);
    }
}