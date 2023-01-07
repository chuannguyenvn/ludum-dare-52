using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private PlantBranch rootBranch;
    
    private void Start()
    {
        rootBranch.Init(0);
    }
}
