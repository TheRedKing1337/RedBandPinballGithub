using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileMenu : MonoBehaviour
{
    private GameObject loginObject;
    private GameObject profileObject;

    private GameObject highscoreObject;
    private GameObject challengeObject;
    private GameObject highscoreSelectedObject;
    private GameObject challengeSelectedObject;

    private Image pPhoto;
    private Text pName;
    private Text pRank;
    private Image pRankIcon;

    private GameObject homeButton;
    private GameObject editButton;

    private Text highscoreTitle; //find a way to use these 2 for 2 separate texts
    private Text challengeTitle;

    private Text playerHighscoresTitle;
    private Text playerHighscores;    
    private HighscoreMenu globalHighscores;

    public ChallengeMenu challengeMenu;

    private bool isPlayer = false; //wether the current profile is the player's or not

    void Start()
    {
        loginObject = transform.GetChild(0).gameObject;
        profileObject = transform.GetChild(1).gameObject;

        Transform pO = transform.GetChild(1);

        //gets the main panels
        highscoreObject = pO.GetChild(0).gameObject;
        challengeObject = pO.GetChild(1).gameObject;


        //gets the infoPanel elements
        Transform infoPanel = pO.GetChild(2);
        pPhoto = infoPanel.GetChild(0).gameObject.GetComponent<Image>();
        pName = infoPanel.GetChild(1).gameObject.GetComponent<Text>();
        pRank = infoPanel.GetChild(2).gameObject.GetComponent<Text>();
        pRankIcon = infoPanel.GetChild(3).gameObject.GetComponent<Image>();

        homeButton = infoPanel.GetChild(4).gameObject;
        editButton = infoPanel.GetChild(5).gameObject;

        //gets the optionPanel elements
        Transform optionPanel = pO.GetChild(3);
        highscoreTitle = optionPanel.GetChild(2).gameObject.GetComponent<Text>();
        challengeTitle = optionPanel.GetChild(3).gameObject.GetComponent<Text>();

        highscoreSelectedObject = optionPanel.GetChild(0).gameObject;
        challengeSelectedObject = optionPanel.GetChild(1).gameObject;

        //gets the highscorePanel elements
        Transform highscorePanel = pO.GetChild(0);
        playerHighscoresTitle = highscorePanel.GetChild(1).GetChild(0).gameObject.GetComponent<Text>();
        playerHighscores = highscorePanel.GetChild(1).GetChild(1).gameObject.GetComponent<Text>();
        globalHighscores = highscorePanel.GetChild(2).gameObject.GetComponent<HighscoreMenu>();

        //testing
        //StartCoroutine(LoadPlayerProfile(1));
    }
    public void LoadProfile(int ID)
    {
        StartCoroutine(LoadPlayerProfile(ID));
    }
    IEnumerator LoadPlayerProfile(int ID)    //The initiate function of a profilemenu, will load data from database if needed and controls the menu transition 
    {
        if(ID == 9999)                                                      //as default set to current playerID called by menu button
        {
            if(GlobalVar.playerID == 0)
            {
                GameObject.Find("Canvas").GetComponent<MainMenu>().ChangeMenu(5); //switch to login screen if not logged in
                yield break;
                } else {
                ID = GlobalVar.playerID;
            }
        }
        GameObject.Find("Canvas").GetComponent<MainMenu>().ChangeMenu(2);   //start transition to profile menu and log time
        float timer = Time.time;

        if (ID == GlobalVar.playerID)                                       //set internal var
        {
            isPlayer = true;            
        } else { isPlayer = false; }

        if (ID != GlobalVar.playerID)                                       //if request other profile, player data will already be in GlobalVar
        {
            if (ID != 0)
            {
                yield return StartCoroutine(ServerFunc.LoadProfile(ID));    //get profile data from server
            }
        }
        
        while(timer + 0.55f > Time.time)                                        //wait for atleast 1 second since function started so that the menu doesnt visibly change
        {
            yield return null;
        }

        if (ID == GlobalVar.playerID)                                       //then update the menus with the loaded vars from GlobalVar
        {
            LoadOwnProfile();
        }
        else { LoadOtherProfile(); }

        UpdateHighscores(0);                                                //and updates the highscore tables

        yield break;
    }

    public void LoadOwnProfile() //loads the left part of screen for your own profile
    {
        pPhoto.sprite = Resources.Load<Sprite>("icons/" + GlobalVar.playerIcon);
        pName.text = GlobalVar.playerName;
        pRank.text = "Rank: " + GlobalVar.playerRank;

        string path = GetRankIcon(GlobalVar.playerRank);
        pRankIcon.sprite = Resources.Load<Sprite>(path);

        highscoreTitle.text = GlobalVar.playerName + "'s Highscores";
        challengeTitle.text = GlobalVar.playerName + "'s Challenges";

        //show edit info button
        editButton.SetActive(true);
        //hide home button
        homeButton.SetActive(false);

        //update challenge screen
        challengeMenu.UpdateChallenges(true, GlobalVar.playerName);
    }
    public void LoadOtherProfile() //loads the left part of screen for someone else's profile
    {
        pPhoto.sprite = Resources.Load<Sprite>("icons/" + GlobalVar.profileIcon);
        pName.text = GlobalVar.profileName;
        pRank.text = GlobalVar.profileRank.ToString();

        string path = GetRankIcon(GlobalVar.profileRank);
        pRankIcon.sprite = Resources.Load<Sprite>(path);

        highscoreTitle.text = GlobalVar.profileName + "'s Highscores";
        challengeTitle.text = GlobalVar.profileName + "'s Challenges";

        //hide edit info button
        editButton.SetActive(false);
        //show home button
        homeButton.SetActive(true);

        //update challenge screen
        challengeMenu.UpdateChallenges(false, GlobalVar.profileName);
    }
    public void SetScreen(bool a) //switches between the highscore screen and the challenge one
    {
        AudioManager.Instance.Play("select");
        highscoreObject.SetActive(a);
        challengeObject.SetActive(!a);
        highscoreSelectedObject.SetActive(!a);
        challengeSelectedObject.SetActive(a);
    }

    public void UpdateHighscores(int ID) //updates the highscore tables to the provided ID
    {
        if (globalHighscores.activeTable != ID) //if not already on right table get data from server and update list
        {
            StartCoroutine(globalHighscores.SetHighscoreTable(ID));
        }

        playerHighscoresTitle.text = ((isPlayer) ? "Your" : GlobalVar.profileName + "'s") + " Highscores on " + LevelInfo.tables[ID].name;

        string s = "";
        for(int i=0;i<5;i++)
        {
            s += GlobalVar.playerHighscores[ID, i] + "\n";
        }
        playerHighscores.text = s;
    }
    public void Home()                    //function to go to own profile if logged in
    {
        if (GlobalVar.playerID != 0)
        {
            StartCoroutine(LoadPlayerProfile(GlobalVar.playerID));
        } else 
        {                                //switch to login screen
            GameObject.Find("Canvas").GetComponent<MainMenu>().ChangeMenu(5);
        }
    }
    public void ShowInfoEdit()            //function to change profile info, shows a popup that allows you to change name and select a profile picture
    {
        GameObject.Find("EditInfoMenu").GetComponent<EditInfoMenu>().SetUI();
        GameObject.Find("Canvas").GetComponent<MainMenu>().ChangeMenu(6);
    }
    public void ChangeInfo(string name, string icon)    //inputfield for name, something else for the icon?
    {
        StartCoroutine(ServerFunc.UpdateInfo(name, icon));
    }
    public void LogOut()
    {
        //reset globalVar values

        //go to login screen
        GameObject.Find("Canvas").GetComponent<MainMenu>().ChangeMenu(5);
    }




    string GetRankIcon(int rank) //returns the icon path for the given rank
    {
        string r = "icons/rank";
        if(rank < 25)
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