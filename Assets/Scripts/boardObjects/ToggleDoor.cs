using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDoor : MonoBehaviour
{
    public float leftPos;
    public float rightPos;

    public void Toggle(bool isLeft)
    {
        if (!isLeft)
        {
            transform.rotation = Quaternion.Euler(0, leftPos, 0);
        }else {
            transform.rotation = Quaternion.Euler(0, rightPos, 0);
        }
    }
}
