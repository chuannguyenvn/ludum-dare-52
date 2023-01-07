using System;
using UnityEngine;

public abstract class Scalable : MonoBehaviour
{
    private float initialScale;

    protected virtual void Start()
    {
        initialScale = transform.localScale.x;
    }

    public void Scale(float scale)
    {
        scale = Mathf.Clamp01(scale);
        transform.localScale = Vector3.one * scale * initialScale;
    }
}