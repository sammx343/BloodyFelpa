using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera2 : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(3,12,-10);

    private void Update()
    {
        Vector3 position = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, position, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
