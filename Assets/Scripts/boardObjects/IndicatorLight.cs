using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorLight : MonoBehaviour
{
    public void EnableLight()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }
    public void DisableLight()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
    }
}
