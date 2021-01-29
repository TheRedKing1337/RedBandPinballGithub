using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject(name: "GameManager");
                go.AddComponent<AudioManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
        }
    }
    public void Play(string name)       //has the ability to play a random sound from a selection of similar ones, not used atm so maybe scrap for perf
    {
        Sound[] s = Array.FindAll(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("AudioClip: " + name + " not found");
            return;
        }
        if (s.Length > 1)
        {
            s[UnityEngine.Random.Range(0, s.Length)].source.Play();
        }
        else
        {
            s[0].source.Play();
        }
    }
    //private enum SoundNames   //was a start of a more optimized system
    //{
    //    flipperUp,
    //    flipperDown,
    //    bigBumper,
    //    wallBumper,
    //    loadPinball,
    //    plunger,
    //    death,
    //    doorClose,
    //    lightBleep,
    //    succes,
    //    oneTimeLaunch,
    //    whoosh
    //}
}

[System.Serializable]
public class Sound 
{
    public string name;

    public AudioClip clip;

    [HideInInspector]
    public AudioSource source;    
}

