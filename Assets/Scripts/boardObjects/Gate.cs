using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private bool isOpen;
    void OnTriggerEnter(Collider col)
    {

        //call gameController with score of opening a door, should replace with a type, then use dictionary to show name and score
        DragonGameManager.Instance.AddScore("gate");
        //play sound
        AudioManager.Instance.Play("wallBumper");
    }

    //a function that plays a sound when closed
    //void OnTriggerExit(Collider col)
    //{
    //    isOpen = true;
    //}
    //void FixedUpdate()
    //{
    //    if(isOpen && (transform.GetChild(1).rotation.z < 1))
    //    {
    //        Debug.Log("close");
    //        Debug.Log(transform.GetChild(1).rotation.z);
    //        AudioManager.Instance.Play("doorClose");
    //        isOpen = false;
    //    }
    //}
}
