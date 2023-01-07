using System;
using Shapes;
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

    [SerializeField] private Line branchBody;

    public void Init(int depth)
    {
        this.depth = depth;
        CalculateHeight();
        transform.localScale = new Vector3(transform.localScale.x, height, 1);
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
        height = Mathf.Pow(1.1f, -depth);
    }

    public void TryGrowNewBranch(bool isInit = false)
    {
        if (!isInit && transform.root.GetComponent<PlantBranch>() == null) return;
        if (IsAllowedToGrowNewBranch()) GrowNewBranch();
        if (IsAllowedToGrowNewBranch()) GrowNewBranch();
    }

    private bool IsAllowedToGrowNewBranch()
    {
        var value = Mathf.Pow(1.25f, -depth);
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Saw"))
        {
            if (ParentBranch != null) ParentBranch.TryGrowNewBranch();
            transform.DetachChildren();
            Destroy(branchBody.gameObject);
            Destroy(gameObject);
        }
    }
}