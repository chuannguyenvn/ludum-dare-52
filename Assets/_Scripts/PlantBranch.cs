using System;
using Shapes;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlantBranch : MonoBehaviour
{
    public PlantBranch ParentBranch;
    private int depth;
    private float height;
    private Vector3 initialScale;

    public HingeJoint2D HingeJoint2D;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float counterAngularVelocity;

    private float initialRotation;

    [SerializeField] private Line branchBody;

    [SerializeField] private float heightConstant;
    [SerializeField] private float heightPow;
    [SerializeField] private float newBranchProbabilityPow;

    public void Init(int depth)
    {
        this.depth = depth;
        CalculateHeight();
        initialScale = transform.localScale = new Vector3(transform.localScale.x, height, 1);
        TryGrowNewBranch(true);
        initialRotation = transform.localRotation.eulerAngles.z;
    }

    private void Update()
    {
        counterAngularVelocity =
            Mathf.DeltaAngle(transform.localRotation.eulerAngles.z, initialRotation);
        rigidbody2D.angularVelocity = counterAngularVelocity * 5;
    }

    private void CalculateHeight()
    {
        height = Mathf.Pow(heightPow, -depth) * heightConstant;
    }

    public void TryGrowNewBranch(bool isInit = false)
    {
        if (!isInit && transform.root.GetComponent<PlantBranch>() == null) return;
        if (IsAllowedToGrowNewBranch()) GrowNewBranch();
        if (IsAllowedToGrowNewBranch()) GrowNewBranch();
    }

    private bool IsAllowedToGrowNewBranch()
    {
        var value = Mathf.Pow(newBranchProbabilityPow, -depth);
        return Random.Range(0f, 1f) < value;
    }

    private void GrowNewBranch()
    {
        var branchObj = Instantiate(ResourceManager.Instance.PlantBranch, transform);

        var newBranchYPos = Random.Range(height / 2, height);
        branchObj.transform.localPosition = new Vector3(0, newBranchYPos, 0);

        var randomRotation = Random.Range(0, 2) == 0 ? -30 : 30;
        branchObj.transform.localRotation = quaternion.Euler(0, 0, randomRotation);

        branchObj.HingeJoint2D.connectedBody = rigidbody2D;
        branchObj.ParentBranch = this;
        branchObj.Init(depth + 1);
    }

    public void Scale(float scale)
    {
        scale = Mathf.Pow(Mathf.Clamp01(scale), 2);
        transform.localScale = initialScale * scale;
    }

    public void DestroyBranch()
    {
        if (ParentBranch != null) ParentBranch.TryGrowNewBranch();
        transform.DetachChildren();
        Destroy(branchBody.gameObject);
        Destroy(gameObject);
    }
}