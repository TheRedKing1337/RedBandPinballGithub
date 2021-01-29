using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableInfoMenu : MonoBehaviour
{
    public Image preview;
    public Text info;

    public HighscoreMenu hsMenu;

    void Start()
    {
        UpdateInfo(0);
        
    }

    void UpdateInfo(int ID)
    {
        preview.sprite = Resources.Load<Sprite>("preview/" + LevelInfo.tables[ID].filePath);
        info.text = LevelInfo.tables[ID].info;
    }

    public void SelectLevel(int ID)
    {
        AudioManager.Instance.Play("select");
        UpdateInfo(ID);
        int menu = GameObject.Find("Canvas").GetComponent<MainMenu>().activeMenu;
        if (menu == 2)      //if profile menu 
        {
            GameObject.Find("ProfileMenu").GetComponent<ProfileMenu>().UpdateHighscores(ID);
        }
        else if (menu == 3)  //if highscore menu
        {
            StartCoroutine(hsMenu.SetHighscoreTable(ID));
        }
    }
}
