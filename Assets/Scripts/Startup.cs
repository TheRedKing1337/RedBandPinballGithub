using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Startup : MonoBehaviour
{
    public MainMenu mainMenu;
    public GameObject introMenu;
    void Awake()
    {
        LevelInfo.LoadInfo();

        if (GlobalVar.isStartup)
        {
            StartCoroutine(StartupAnim());
        } else {
            RegularAnim();
        }
    }
    void Update()
    {
        if(GlobalVar.isStartup)
        {
            if(Input.GetMouseButtonDown(0)) //this allows you to skip the intro
            {
                StopAllCoroutines();
                EndStartupAnim();
            }
        }
    }
    IEnumerator StartupAnim()
    {   
        yield return new WaitForSeconds(1);

        Image logo = introMenu.transform.GetChild(0).GetComponent<Image>();
        Text text = introMenu.transform.GetChild(1).GetComponent<Text>();


        float timer = Time.time + 1;
        while (timer > Time.time) 
        {
            float value = timer - Time.time;
            logo.color = new Vector4(1,1,1,value);
            text.color = new Vector4(1, 1, 0, value);
            yield return null;
        }

        EndStartupAnim();
        yield break;
    }
    void EndStartupAnim()
    {
        introMenu.SetActive(false);
        StartCoroutine(mainMenu.ShowMenu(5));
        GlobalVar.isStartup = false;
    }

    void RegularAnim()
    {
        introMenu.SetActive(false);
        StartCoroutine(mainMenu.ShowMenu(0));
    }
}
