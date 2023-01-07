using System;
using System.Collections;
using UnityEngine;

public class Machine : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float effectiveDuration = 5f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(effectiveDuration);
        yield return Shrink();
    }

    private IEnumerator Shrink()
    {
        while (transform.localScale.x > 0.01f)
        {
            transform.localScale -= Vector3.one * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    public void SetVelocity(Vector2 velocity)
    {
        rigidbody2D.velocity = velocity;
    }
}