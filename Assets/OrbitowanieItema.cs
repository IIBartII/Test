using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class OrbitowanieItema : MonoBehaviour
{
    public Transform center;
    public float rSpeed;
    void Update()
    {
        Quaternion currentRotation = center.transform.rotation;
        Quaternion desiredRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, currentRotation.eulerAngles.z + rSpeed);
        center.transform.rotation = desiredRotation;
    }
}