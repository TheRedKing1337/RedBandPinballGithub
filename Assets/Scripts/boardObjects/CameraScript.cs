using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public bool followPinball;
    public Vector3 staticPos;

    private float lowestPinball;
    private List<Transform> pinballs = new List<Transform>();

    //called when pinball is created/destroyed
    public void AddPinball(Transform pinball)
    {
        pinballs.Add(pinball);
    }   
    public void RemovePinball(Transform pinball)
    {
        pinballs.Remove(pinball);
    }
    void Update()
    {
        if (followPinball)
        {
            lowestPinball = 100;
            for (int i = 0; i < pinballs.Count; i++)
            {
                if (pinballs[i].position.z - 10 < lowestPinball)
                {
                    lowestPinball = pinballs[i].position.z - 10;
                }
            }
            lowestPinball = Mathf.Clamp(lowestPinball, -35, -4);

            transform.position = new Vector3(transform.position.x, 38, lowestPinball);
        } else {
            transform.position = staticPos;
        }
    }
}
