using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour //this class controls the menu transitions
{
    public GameObject[] menuArray; //0 is main, 1 is level select, 2 is profile, 3 is highscores, 4 is settings, 5 is login screen
    public GameObject[] firstSelected; //first selected button for each menu
    public int activeMenu;
    private bool transitioning;

    private float transSpeed = 0.55f;    

    public AudioSource musicSource;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && activeMenu != 0)
        {
            ChangeMenu(0);
        }
    }
    //figure out anims
    public IEnumerator ShowMenu(int menu)
    {
        activeMenu = menu;
        if (firstSelected[activeMenu] != null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelected[activeMenu], null);
        }
        //StartAnim
        menuArray[menu].GetComponent<Animator>().enabled = true;
        menuArray[menu].GetComponent<Animator>().Play("Show" + menuArray[menu].name);
        AudioManager.Instance.Play("transition");

        yield return new WaitForSeconds(transSpeed);
        menuArray[menu].GetComponent<Animator>().enabled = false;

        yield break;
    }

    IEnumerator HideMenu(int menu)
    {
        //StartAnim
        menuArray[menu].GetComponent<Animator>().enabled = true;
        menuArray[menu].GetComponent<Animator>().Play("Hide"+menuArray[menu].name);
        AudioManager.Instance.Play("transition");

        yield return new WaitForSeconds(transSpeed);
        menuArray[menu].GetComponent<Animator>().enabled = false;

        yield break;
    }

    IEnumerator TransitionMenu(int menu)
    {
        transitioning = true;
        StartCoroutine(HideMenu(activeMenu));
        yield return new WaitForSeconds(transSpeed);
        StartCoroutine(ShowMenu(menu));
        yield return new WaitForSeconds(transSpeed);
        transitioning = false;

        yield break;
    }

    public void ChangeMenu(int menu)
    {
        if (!transitioning)
        {
            StartCoroutine(TransitionMenu(menu));
        }
    }
    IEnumerator TransitionToLevel(string level)     //transitions to the selected level, moves menu and silences music
    {
        transitioning = true;
        StartCoroutine(HideMenu(activeMenu));
        //lower music volume
        float timer = Time.time;
        while (timer + transSpeed > Time.time)
        {
            musicSource.volume *= 0.9f;
            yield return null;
        }
        SceneManager.LoadScene(level);

        yield break;
    }
    public void LoadLevel(string level)
    {
        if (!transitioning)
        {
            StartCoroutine(TransitionToLevel(level));
        }
    }
}