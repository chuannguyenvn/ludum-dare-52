using System;
using System.Collections;
using DG.Tweening;
using Shapes;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlantBranch : Scalable
{
    public PlantBranch ParentBranch;
    private int depth;
    private float height;

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
        TryGrowNewBranch(true);
    }

    private void Update()
    {
        counterAngularVelocity =
            Mathf.DeltaAngle(transform.localRotation.eulerAngles.z, initialRotation);

        var swayVelocity = Mathf.Sin(Time.time * 2) * 10f;
        rigidbody2D.angularVelocity = counterAngularVelocity * 5;
        if (ParentBranch != null) rigidbody2D.angularVelocity += swayVelocity;
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

    public void GrowNewBranch()
    {
        StartCoroutine(GrowNewBranch_CO());
    }
    public IEnumerator GrowNewBranch_CO()
    {
        var branchObj = Instantiate(ResourceManager.Instance.PlantBranch, transform);

        var newBranchYPos = Random.Range(height / 2, height);
        branchObj.transform.localPosition = new Vector3(0, newBranchYPos, 0);

        var randomRotation = Random.Range(0, 2) == 0 ? -30 : 30;
        branchObj.initialRotation = randomRotation;
        branchObj.CalculateHeight();
        branchObj.HingeJoint2D.connectedBody = rigidbody2D;
        branchObj.ParentBranch = this;
        branchObj.transform.SetParent(transform.parent);
        
        branchObj.branchBody.End = Vector3.zero;
        while (branchObj.branchBody.End.magnitude < 0.99f)
        {
            branchObj.branchBody.End += Vector3.up * Time.deltaTime;
            yield return null;
        }

        branchObj.Init(depth + 1);
    }

    public void DestroyBranch()
    {
        if (ParentBranch != null) ParentBranch.TryGrowNewBranch();
        transform.DetachChildren();
        Destroy(branchBody.gameObject);
        Destroy(gameObject);
    }
}