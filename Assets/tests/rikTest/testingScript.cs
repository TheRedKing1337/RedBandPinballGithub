using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingScript : MonoBehaviour
{
    public Animator door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            AudioManager.Instance.Play("flipperUp");
            door.SetFloat("open", 1);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            AudioManager.Instance.Play("flipperDown");
            door.SetFloat("open", 0);
        }
    }
}
