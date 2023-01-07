using System;
using System.Collections;
using Shapes;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Planet : Scalable
{
    [SerializeField] private PlanetGravityZone planetGravityZone;
    [SerializeField] private float planetGravityRadius;
    [SerializeField] private float planetGravityMagnitude;
    private float initialScale;
    
    private void Awake()
    {
        planetGravityZone.Init(planetGravityRadius, planetGravityMagnitude);
        initialScale = transform.localScale.x;
    }

    protected override void Start()
    {
        base.Start();
        InitializePlants();
    }

    private void InitializePlants()
    {
        var count = Random.Range(8, 12);
        for (int i = 0; i < count; i++)
        {
            var planetRadius = transform.localScale.x / 2;
            var randomCenterAngle = 2 * Mathf.PI / count * (i + 1);
            var randomAngle = randomCenterAngle + Random.Range(-Mathf.PI / 4, Mathf.PI / 4);
            var rootPosition = transform.position + 
                new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * planetRadius;

            var plant = Instantiate(ResourceManager.Instance.Plant, transform);
            plant.transform.position = rootPosition;
            plant.transform.rotation = Quaternion.Euler(0, 0, randomAngle * Mathf.Rad2Deg);
        }
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