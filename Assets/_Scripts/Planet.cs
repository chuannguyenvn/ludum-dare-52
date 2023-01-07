using System;
using System.Collections;
using Shapes;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Planet : MonoBehaviour
{
    [SerializeField] private GravityZone gravityZone;
    [SerializeField] private float planetGravityRadius;
    [SerializeField] private float planetGravityMagnitude;

    private void Awake()
    {
        gravityZone.Init(planetGravityRadius, planetGravityMagnitude);
    }

    private void Start()
    {
        InitializePlants();
    }

    private void InitializePlants()
    {
        for (int i = 0; i < Random.Range(5, 10); i++)
        {
            var planetRadius = transform.localScale.x / 2;
            var randomAngle = Random.Range(0, 2 * Mathf.PI);
            var rootPosition = transform.position + 
                new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * planetRadius;

            var plant = Instantiate(ResourceManager.Instance.Plant);
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