using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreMenu : MonoBehaviour
{
    public Text tableTitle;
    public Text tableNames;
    public Text tableScores;

    public int activeTable = 0;

    void Start()
    {
        StartCoroutine(SetHighscoreTable(0));
    }

    public IEnumerator SetHighscoreTable(int ID) //updates the highscore table to the given ID
    {
        activeTable = ID;
        //load new highscores from database
        yield return StartCoroutine(ServerFunc.GetHighscores(ID));

        //sets the string and updates the UI
        string tNames = "";
        string tScores = "";

        for(int i=0;i<10;i++)
        {
            tNames += GlobalVar.highscoresNames[ID, i] + "\n";
            tScores += GlobalVar.highscores[ID, i] + "\n";
        }

        tableTitle.text = "Global Highscores on " + LevelInfo.tables[ID].name;
        tableNames.text = tNames;
        tableScores.text = tScores;       
    }
    public void RefreshTable() // a refresh button available on the table
    {
        AudioManager.Instance.Play("select");
        StartCoroutine(SetHighscoreTable(activeTable));
    }
    public void LoadProfile(int ID) //has to be changed in the menu rework
    {
        AudioManager.Instance.Play("select");
        ProfileMenu PM = GameObject.Find("ProfileMenu").GetComponent<ProfileMenu>();
        int IDToLoad = GlobalVar.highscoresIDs[activeTable, ID];
     
        PM.LoadProfile(IDToLoad);
    }
}