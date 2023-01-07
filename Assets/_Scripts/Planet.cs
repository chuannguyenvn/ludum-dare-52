using System;
using Shapes;
using UnityEngine;
using UnityEngine.Serialization;

public class Planet : MonoBehaviour
{
    [SerializeField] private GravityZone gravityZone;
    [SerializeField] private float planetGravityRadius;
    [SerializeField] private float planetGravityMagnitude;

    private void Awake()
    {
        gravityZone.Init(planetGravityRadius, planetGravityMagnitude);
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