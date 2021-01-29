using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SupportFunc;

public class wallBumper : MonoBehaviour
{
    public int launchForce;
    public bool isMultiAngle;

    Vector3 contactNormal;
    Vector3 pinballNormal;
    Vector3 outNormal;


    //void OnCollisionEnter(Collision col)
    //{
    //    point = col.GetContact(0).point;
    //    //get angle
    //    contactNormal = col.GetContact(0).normal * -1;
    //    pinballNormal = col.rigidbody.velocity.normalized;

    //    outNormal = Vector3.Reflect(pinballNormal, contactNormal);

    //    //remove velocity
    //    col.rigidbody.velocity = Vector3.zero;

    //    //add velocity
    //    col.rigidbody.AddForce(outNormal * launchForce);
    //}

    void OnTriggerEnter(Collider col)
    {
        if (isMultiAngle)
        {
            //get normals from rotation
            contactNormal = AngleFunc.VectorFromAngle(transform.rotation.eulerAngles.y);
            //pinballNormal = col.attachedRigidbody.velocity.normalized;
            pinballNormal = col.gameObject.GetComponent<Pinball>().oldNormal.normalized;

            outNormal = Vector3.Reflect(pinballNormal, contactNormal);

            outNormal.Set(Mathf.Clamp(outNormal.x,contactNormal.x-0.6f, contactNormal.x + 0.6f),0, Mathf.Clamp(outNormal.z, contactNormal.z - 0.6f, contactNormal.z + 0.6f));
        } else 
        {
            outNormal = AngleFunc.VectorFromAngle(transform.rotation.eulerAngles.y);
        }

        //remove velocity
        col.attachedRigidbody.velocity = Vector3.zero;

        //add velocity, play sound and play animation
        col.attachedRigidbody.AddForce(outNormal * launchForce);
        AudioManager.Instance.Play("wallBumper");
        //anim TBA
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + contactNormal * 10);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + pinballNormal * -10);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + outNormal * 10);
    }
    //void OnCollisionEnter(Collision col)
    //{
    //    Vector3 normal = col.rigidbody.velocity.normalized;

    //    col.rigidbody.velocity = Vector3.zero;

    //    col.rigidbody.AddForce(normal * launchForce);
    //}
}
