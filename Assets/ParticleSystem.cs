using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystem : MonoBehaviour
{
    public float offsetX_AD = 0.2f;
    public float offsetY_AD = 0.35f;
    public float offsetX_WS = 0.35f;
    public float offsetY_WS = 0.35f;
    private void Update()
    {
        if (Input.GetKey(KeyCode.A) && (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S)))
        {
            transform.localPosition = new(offsetX_AD, -offsetY_AD, 0f);

        }
        if (Input.GetKey(KeyCode.D) && (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S)))
        {
            transform.localPosition = new(-offsetX_AD, -offsetY_AD, 0f);
        }
        if (Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            transform.localPosition = new(offsetX_WS, -offsetY_WS, 0f);
        }
        if (Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            transform.localPosition = new(offsetX_WS, offsetY_WS, 0f);
        }
    }
}