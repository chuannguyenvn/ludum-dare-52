﻿using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlantBranch : MonoBehaviour
{
    public PlantBranch ParentBranch;
    private int depth;
    private float height;

    public HingeJoint2D HingeJoint2D;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float counterAngularVelocity;

    private float initialRotation;

    public void Init(int depth)
    {
        this.depth = depth;
        CalculateHeight();
        transform.localScale = new Vector3(transform.localScale.x, height, 1);
        if (IsAllowedToGrowNewBranch()) GrowNewBranch();
        if (IsAllowedToGrowNewBranch()) GrowNewBranch();
        initialRotation = transform.localRotation.eulerAngles.z;
    }

    private void Update()
    {
        counterAngularVelocity =
            Mathf.DeltaAngle(transform.localRotation.eulerAngles.z, initialRotation);
        rigidbody2D.angularVelocity = counterAngularVelocity * 3;
    }

    private void CalculateHeight()
    {
        height = Mathf.Pow(1.25f, -depth);
    }

    private bool IsAllowedToGrowNewBranch()
    {
        var value = Mathf.Pow(2, -depth);
        return Random.Range(0f, 1f) < value;
    }

    public void GrowNewBranch()
    {
        var branchObj = Instantiate(ResourceManager.Instance.PlantBranch, transform);

        var newBranchYPos = Random.Range(height / 2, height);
        branchObj.transform.localPosition = new Vector3(0, newBranchYPos, 0);

        var randomRotation = Random.Range(0, 2) == 0 ? -30 : 30;
        branchObj.transform.localRotation = quaternion.Euler(0, 0, randomRotation);
        
        branchObj.HingeJoint2D.connectedBody = rigidbody2D;
        branchObj.Init(depth + 1);
    }
}