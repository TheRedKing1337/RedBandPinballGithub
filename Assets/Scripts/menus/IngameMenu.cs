using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameMenu : MonoBehaviour
{
    public Image playerPhoto;
    public Text playerName;
    public Text playerRank;
    public Image playerRankIcon;

    public Text playerHighscores;
    public Text newScore;

    public void LoadInfo(int score)
    {
        UpdatePlayerInfo();
        UpdatePlayerHighscores(score);
        transform.GetChild(0).gameObject.GetComponent<HighscoreMenu>().RefreshTable();
    }

    //set the player info panel
    void UpdatePlayerInfo()
    {
        playerPhoto.sprite = Resources.Load<Sprite>("icons/" + GlobalVar.playerIcon);
        playerName.text = GlobalVar.playerName;
        playerRank.text = GlobalVar.playerRank.ToString();

        string path = GetRankIcon(GlobalVar.playerRank);
        playerRankIcon.sprite = Resources.Load<Sprite>(path);
    }

    //set the player highscores menu
    void UpdatePlayerHighscores(int score)
    {
        string s = "";
        for (int i = 0; i < 5; i++)
        {
            s += GlobalVar.playerHighscores[0, i] + "\n";
            if(score == GlobalVar.playerHighscores[0, i])
            {
                //highlight this score somehow
            }
        }
        playerHighscores.text = s;

        newScore.text = score.ToString();
    }

    string GetRankIcon(int rank) //returns the icon path for the given rank
    {
        string r = "icons/rank";
        if (rank < 25)
        {
            r += "Pleb";
        }
        else if (rank > 419)
        {
            r += "Gold";
        }
        return r;
    }
}
