using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SupportFunc;

public class rampBoost : MonoBehaviour
{
    public float boostForce;
    void OnTriggerEnter(Collider col)
    {
        Vector3 normal = AngleFunc.VectorFromAngle(transform.rotation.eulerAngles.y);

        col.attachedRigidbody.velocity = Vector3.zero;
        col.attachedRigidbody.AddForce(normal*boostForce);

        DragonGameManager.Instance.AddScore("ramp");
        AudioManager.Instance.Play("whoosh");
    }
}
