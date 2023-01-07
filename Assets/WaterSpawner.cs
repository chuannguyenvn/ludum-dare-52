using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaterSpawner : MonoBehaviour
{
    [SerializeField] private float initForceMultiplier;
    private IEnumerator Start()
    {
        while (true)
        {
            var waterObj = Instantiate(ResourceManager.Instance.Water, transform);
            var startPos = (Vector3)Random.insideUnitCircle.normalized * 20;
            waterObj.transform.position = startPos;
            waterObj.GetComponent<Rigidbody2D>().AddForce(-startPos * initForceMultiplier);

            yield return new WaitForSeconds(1f);
        }
    }
}