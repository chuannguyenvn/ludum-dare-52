using System;
using UnityEngine;

public class PlantBranch : MonoBehaviour
{
    public PlantBranch ParentBranch;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float counterAngularVelocity;
    
    private float initialRotation;

    private void Start()
    {
        initialRotation = transform.localRotation.eulerAngles.z;
    }

    private void Update()
    {
        counterAngularVelocity = Mathf.DeltaAngle(transform.localRotation.eulerAngles.z, initialRotation);
        rigidbody2D.angularVelocity = counterAngularVelocity * 3;
    }
}