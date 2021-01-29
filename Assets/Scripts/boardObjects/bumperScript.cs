using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SupportFunc;

public class bumperScript : MonoBehaviour
{
    public float bumperForce;
    private GameObject pinball;

    void OnCollisionEnter(Collision col)
    {
        pinball = col.gameObject;
        Vector3 normal = AngleFunc.NormalToObject(transform.position, pinball.transform.position);
        pinball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        pinball.GetComponent<Rigidbody>().AddForce(normal * bumperForce);

        DragonGameManager.Instance.AddScore("bumper");
        AudioManager.Instance.Play("bigBumper");
    }
}