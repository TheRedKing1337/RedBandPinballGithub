using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeLauncher : MonoBehaviour
{
    private GameObject launcher;
    private Transform gate;
    private Rigidbody pinball;

    public float launchForce;
    public bool isOpen = true;

    void Awake()
    {
        launcher = transform.GetChild(1).gameObject;
        gate = transform.GetChild(0);
    }

    //functie om te lanceren als hij op de launcher is, wss een spring joint gebruiken
    void OnTriggerEnter(Collider col)
    {
        pinball = col.gameObject.GetComponent<Rigidbody>();
        pinball.velocity = Vector3.zero;
        StartCoroutine(Launch());
    }
    private IEnumerator Launch()
    {
        //move launcher back over time
        Vector3 oriPos = launcher.transform.localPosition;
        while(launcher.transform.localPosition.z > -1)
        {
            launcher.transform.Translate(0, 0, -1 * Time.deltaTime);
            yield return null;
        }

        //give pinball force
        pinball.AddForce(0, 0, launchForce);

        AudioManager.Instance.Play("oneTimeLaunch");
        DragonGameManager.Instance.AddScore("launcher");

        //move launcher to origional pos        
        launcher.transform.localPosition = oriPos;

        yield return new WaitForSeconds(0.25f);

        //close gate
        gate.Rotate(0,-65,0);
        isOpen = false;
        yield break;
    }

    //functie om gate te openen vanuit GameManager
    public void OpenGate()
    {
        isOpen = true;
        gate.Rotate(0, 65, 0);
    }
}
