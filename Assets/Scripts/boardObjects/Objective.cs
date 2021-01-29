using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public int objNum;
    void OnTriggerEnter()
    {
        DragonGameManager.Instance.GotObjective(objNum);
    }
}
