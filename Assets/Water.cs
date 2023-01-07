using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Scalable
{
    [SerializeField] private CircleCollider2D circleCollider2D;
    [SerializeField] private float shrinkSpeed;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Branch"))
        {
            circleCollider2D.isTrigger = true;
            col.gameObject.GetComponent<PlantBranch>().GrowNewBranch();
            StartCoroutine(Shrink_CO());
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Branch"))
        {
            col.gameObject.GetComponent<PlantBranch>().GrowNewBranch();
        }
    }

    private IEnumerator Shrink_CO()
    {
        while (transform.localScale.x > 0.1f)
        {
            transform.localScale -= Time.deltaTime * shrinkSpeed * Vector3.one;
            yield return null;
        }
        
        Destroy(gameObject);
    }
}