using System;
using DG.Tweening;
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
        scale = DOVirtual.EasedValue(0, 1, scale, Ease.OutSine);
        transform.localScale = Vector3.one * scale * initialScale;
    }
}