using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Audio
{
    static AudioSource a;
    public static void PlaySound(int index)
    {
        a = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        if(index == 0)
        {
            a.Play();
        }
    }
}
