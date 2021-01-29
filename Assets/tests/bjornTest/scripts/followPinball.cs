using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPinball : MonoBehaviour
{
    public float zMaxUp;
    public float zMaxDown;

    public Transform pinball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, (Mathf.Clamp(pinball.position.z, zMaxDown, zMaxUp)));
    }
}
