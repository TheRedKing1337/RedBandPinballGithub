using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoardSelect : MonoBehaviour
{
    //scroll view vars
    int position;
    public Text[] levelNames;
    public Image[] levelIcons;
    public Image[] levelButtons;

    //info panel vars
    public TableInfoMenu info;

    //general vars
    string selectedLevel;

    void Start()
    {
        selectedLevel = LevelInfo.tables[0].filePath;
        UpdateList();
    }

    //loads the correct info for the list
    void UpdateList()
    {
        //sets each menu option to the level vars
        for (int i = 0; i < 3; i++)
        {
            levelNames[i].text = LevelInfo.tables[i].name.ToUpper();
            levelIcons[i].sprite = Resources.Load<Sprite>("icons/" + LevelInfo.tables[i].icon);
            levelButtons[i].color = LevelInfo.tables[i].color;
        }
    }
    public void SelectLevel(int index)
    {
        AudioManager.Instance.Play("select");
        selectedLevel = LevelInfo.tables[index].filePath;
        info.SelectLevel(index);
    }

    public void LoadSelected()
    {
        AudioManager.Instance.Play("select");
        GameObject.Find("Canvas").GetComponent<MainMenu>().LoadLevel(selectedLevel);
    }
}
