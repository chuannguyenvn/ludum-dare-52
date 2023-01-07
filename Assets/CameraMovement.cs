using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    
    private void Update()
    {
        transform.position =
            Vector3.Lerp(transform.position, (playerTransform.position / 2).NewZ(-10), 0.1f);
    }
}
