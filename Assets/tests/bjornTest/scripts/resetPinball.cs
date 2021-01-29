using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPinball : MonoBehaviour
{
    private Vector3 startPos;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        startPos = GameObject.Find("Pinball").transform.position;
        rb = GameObject.Find("Pinball").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ResetPinballPos();
    }

    void ResetPinballPos()
    {
        if (Input.GetKey("r"))
        {
            transform.position = startPos;
            transform.rotation = Quaternion.identity;
            rb.velocity = new Vector3 (0, 0, 0);

            AudioManager.Instance.Play("loadPinball");
        }
    }
}
