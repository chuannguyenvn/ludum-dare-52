using System;
using Shapes;
using UnityEngine;
using UnityEngine.Serialization;

public class BlackHoleEffectiveZone : MonoBehaviour
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
        other.attachedRigidbody.AddForce(force);
        var scale = Vector2.Distance(other.transform.position, transform.position) / radius;
        
        if (other.gameObject.GetComponent<Scalable>() is Scalable scalable) scalable.Scale(scale);

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Scalable>() is Scalable scalable) scalable.Scale(1);
    }
}