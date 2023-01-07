using System;
using Shapes;
using UnityEngine;
using UnityEngine.Serialization;

public class ShipSuckingZone : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Disc disc;
    [SerializeField] private CircleCollider2D circleCollider2D;
    [SerializeField] private float radius;
    [SerializeField] private float magnitude;

    private void Start()
    {
        disc.Radius = radius;
        circleCollider2D.radius = radius;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var force = (transform.position - other.transform.position).normalized * magnitude;
        if (layerMask == (layerMask | 1 << other.gameObject.layer))
        {
            other.attachedRigidbody.AddForce(force);
            other.GetComponent<PlantBranch>()
                .Scale(Vector2.Distance(other.transform.position, transform.position) / radius);
        }
    }
}