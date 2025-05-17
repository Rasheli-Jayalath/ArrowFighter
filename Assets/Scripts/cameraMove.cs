using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.001f;

    public Vector3 offset;
    void FixedUpdate()
    {
        Vector3 desiredposition = target.position + offset;
        Vector3 smoothedposition = Vector3.Lerp(transform.position, desiredposition, smoothSpeed);
        transform.position = smoothedposition;
    }
}
