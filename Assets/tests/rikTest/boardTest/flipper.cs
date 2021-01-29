using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipper : MonoBehaviour
{
    public GameObject leftFlipper;
    public float flipStrength = 10000;
    Rigidbody rb;

    bool isPressed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
        rb.maxAngularVelocity = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isPressed = true;
            Flip();
        } else if (Input.GetKeyUp(KeyCode.Z)){
            isPressed = false;
        }
    }
    void FixedUpdate(){
        //if (!(Input.GetKey(KeyCode.Z)))
        //{
        //    rb.AddTorque(transform.up * flipStrength * -100);
        //}
        //Vector3 f = transform.up * flipStrength * (isPressed ? 1.0f : -1.0f);
        //Vector3 pos = transform.position + transform.right * 40000.0f * (isPressed ? 1.0f : -1.0f);
        //rb.AddForceAtPosition(f, pos);
    }

    IEnumerator flipLeft()
    {
        //rb.AddTorque(transform.up * flipStrength * 100 , ForceMode.Impulse);
        //for (int i = 0; i < 4; i++)
        //{
        //    //rb.AddTorque(transform.up * flipStrength * 100000);
        //    yield return null;
        //}

        //Debug.Log("flipLeft");
        //for (int i = 0; i < 20; i++)
        //{
        //    transform.Rotate(new Vector3(0, 2, 0));
        //    yield return null;
        //}
        //for (int i = 0; i < 10; i++)
        //{
        //    transform.Rotate(new Vector3(0, -4, 0));
        //    yield return null;
        //}
        yield break;
    }

    void Flip()
    {
        rb.AddTorque(transform.up * flipStrength);
    }
}

//dom V
//on hit flipper see if pressed, set parent gamebody with offset, 
//when flipper reaches max point release perpendicular from flipper 
//with force = distance from center * time + velocity it had when it hit the flipper on flipper and unparent
