using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kogel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(3.612f, .2f, 0);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
