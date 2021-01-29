using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launchPinball : MonoBehaviour
{
    private float timer;
    private bool hasPinball;
    private Rigidbody pinball;

    public float multipliedForce; //Kracht die vermenigvuldigd wordt met de tijd
    public float minForce; //Minamale aantal kracht
    public float maxForce; //Maximale aantal kracht

    public GameObject pinballPrefab;

    // Update is called once per frame
    void Update()
    {
        launch();
    }

    void launch()
    {
        if (Input.GetKeyDown("space"))
        {
            timer = Time.time;
        }
        else if (Input.GetKeyUp("space"))
        {
            if (hasPinball)
            {
                pinball.AddForce(0, 0, Mathf.Clamp(multipliedForce * (Time.time - timer), minForce, maxForce));
                AudioManager.Instance.Play("plunger");
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        hasPinball = true;
        pinball = col.gameObject.GetComponent<Rigidbody>();
        //Debug.Log("hasPinball");
    }
    void OnCollisionExit()
    {
        hasPinball = false;
        //Debug.Log("lostPinball");
    }

    public void NewPinball()
    {
        AudioManager.Instance.Play("loadPinball");
        Vector3 EntryPos = transform.position + (Vector3.forward*5);
        GameObject pinball = Instantiate(pinballPrefab, EntryPos, Quaternion.identity);
    }
 }
