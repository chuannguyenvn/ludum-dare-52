using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawEffectiveArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Branch")) col.GetComponent<PlantBranch>().DestroyBranch();
    }
}