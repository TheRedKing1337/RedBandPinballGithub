using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinball : MonoBehaviour
{
    public Vector3 oldNormal;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject.Find("Main Camera").GetComponent<CameraScript>().AddPinball(transform);
    }
    void FixedUpdate()
    {
        oldNormal = rb.velocity;
    }
    void OnDestroy()
    {
        GameObject.Find("Main Camera").GetComponent<CameraScript>().RemovePinball(transform);
    }
}
