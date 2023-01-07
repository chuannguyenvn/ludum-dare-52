using System;
using Shapes;
using UnityEngine;
using UnityEngine.Serialization;

public class Planet : MonoBehaviour
{
    [SerializeField] private GravityZone gravityZone;
    [SerializeField] private float planetGravityRadius;

    private void Start()
    {
        gravityZone.SetRadius(planetGravityRadius);
    }

    public void Hide()
    {
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = false;
        }
        
        foreach (var shapeRenderer in GetComponentsInChildren<ShapeRenderer>())
        {
            shapeRenderer.enabled = false;
        }
    }
}