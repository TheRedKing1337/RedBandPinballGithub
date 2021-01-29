using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonGameManager : MonoBehaviour
{
    private static DragonGameManager _instance;

    public int score;
    public int pinballsLeft;

    //level state vars
    private int fieldMult = 1;
    private int objState = -1;
    private int alternateLauncher = 0;
    private int activePinballs;

    private bool isDead;

    //boardObjects
    public ToggleLight[] lightStates;
    public IndicatorLight[] iLights;
    public launchPinball[] launchers;
    public OneTimeLauncher[] singleLaunchers;
    public ToggleDoor[] toggleDoors;

    //UI 
    public Text scoreText;
    public Text messageText;
    public Text multText;
    public Text liveText;
    public Text objText;

    private string oldType;
    private int combo = 1;
    private bool isShowingMessage;

    public IngameMenu ingameMenu;

    public GameObject ingameScreen;
    public GameObject gameOverScreen;

    //the score info
    private Dictionary<string, int> scoreTypes = new Dictionary<string, int>();


    public static DragonGameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject(name: "GameManager");
                go.AddComponent<DragonGameManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;

        LevelInfo.LoadInfo();
        //TEMP
        StartCoroutine(ServerFunc.Login("testAccount", "test"));
        StartCoroutine(ServerFunc.GetHighscores(0));
        //</TEMP>

        scoreTypes.Add("gate", 100);
        scoreTypes.Add("light", 125);
        scoreTypes.Add("launcher", 100);
        scoreTypes.Add("bumper", 75);
        scoreTypes.Add("ramp", 150);
        scoreTypes.Add("objective", 5000);
    }
    void Start(){
        for (int i = 0; i < singleLaunchers.Length; i++)
        {
            pinballsLeft--;
            activePinballs++;
            launchers[i].NewPinball();
        }
        UpdateLives();
        StartCoroutine(AnimatingIndicators());
    }




    //boardObject functions-----------------------------------------    
    private IEnumerator AnimatingIndicators()
    {
        while(!isDead)
        {
            int temp = objState + 5;       
            iLights[temp].EnableLight();
            yield return new WaitForSeconds(0.5f);
            iLights[temp].DisableLight();
            yield return new WaitForSeconds(0.5f);
        }
        yield break;
    }
    public void AddScore(string type) //should replace with a type, then use dictionary to show name of type and score
    {
        score += scoreTypes[type] * fieldMult;
        //show message on ingame UI
        UpdateMessage(scoreTypes[type], type, false);
    }

    public void Flipper(bool isLeft)    //cycles the lights, called when flipper is used
    {
        bool tempStor;
        bool tempStorB;
        if(isLeft) //cycle to the left
        {
            tempStor = lightStates[0].state;
            if (lightStates[0].state != lightStates[1].state) { lightStates[0].ToggleState(); }
            if (lightStates[1].state != lightStates[2].state) { lightStates[1].ToggleState(); }
            if (lightStates[2].state != lightStates[3].state) { lightStates[2].ToggleState(); }
            if (lightStates[3].state != tempStor) { lightStates[3].ToggleState(); }

            tempStorB = lightStates[4].state;
            if (lightStates[4].state != lightStates[5].state) { lightStates[4].ToggleState(); }
            if (lightStates[5].state != lightStates[6].state) { lightStates[5].ToggleState(); }
            if (lightStates[6].state != tempStorB) { lightStates[6].ToggleState(); }

            for (int i = 0; i < toggleDoors.Length; i++)
            {
                toggleDoors[i].Toggle(true);
            }
        } 
        else    //cycle to the right
        {
            tempStor = lightStates[3].state;
            if (lightStates[3].state != lightStates[2].state) { lightStates[3].ToggleState(); }
            if (lightStates[2].state != lightStates[1].state) { lightStates[2].ToggleState(); };
            if (lightStates[1].state != lightStates[0].state) { lightStates[1].ToggleState(); };
            if (lightStates[0].state != tempStor) { lightStates[0].ToggleState(); }

            tempStorB = lightStates[6].state;
            if (lightStates[6].state != lightStates[5].state) { lightStates[6].ToggleState(); }
            if (lightStates[5].state != lightStates[4].state) { lightStates[5].ToggleState(); }
            if (lightStates[4].state != tempStorB) { lightStates[4].ToggleState(); }

            for (int i = 0; i < toggleDoors.Length; i++)
            {
                toggleDoors[i].Toggle(false);
            }
        }
    }
    
    public void CheckLights()   //checks if all lights of a section are enabled, if so add effect and reset them
    {
        if(lightStates[0].state && lightStates[1].state && lightStates[2].state && lightStates[3].state)
        {
            //all lights of array 1 are active
            fieldMult++;
            UpdateMult();
            UpdateMessage(0, "Obtained all top lights,         +1 Multiplier!", true);
            //animate lights
            for (int i=0;i<4;i++)
            {
                lightStates[i].UpgradeLights();
            }
            //maybe play special sound for unlocking all
            //AudioManager.Instance.Play("dragonBreath");//this is temp
        }
        else if (lightStates[4].state && lightStates[5].state && lightStates[6].state)
        {
            //all lights of array 2 are active
            UpdateMessage(0, "Obtained all Dinnerhal lights,         Opened a side gate!", true);
            for (int i=0;i<singleLaunchers.Length;i++)
            {
                if (!singleLaunchers[i].isOpen)
                {
                    singleLaunchers[i].OpenGate();
                    break;
                }
            }

            for (int i = 4; i < 7; i++)
            {
                lightStates[i].UpgradeLights();
            }
            //maybe play special sound for unlocking all
        }
    }
    public void GotObjective(int index)
    {        
        if(index == objState+1)
        {
            objState++;
            AudioManager.Instance.Play("succes");
            AddScore("objective");
            //enable next indicator light          
            iLights[index].EnableLight();
            //UI message
            switch (index)
            {
                case 0: //got Sword
                    UpdateMessage(0, "Obtained sword,                     +5000 points                           Now get shield", true);
                    objText.text = "Enter someplace to obtain shield";
                    
                    break;
                case 1: //got Shield
                    UpdateMessage(0, "Obtained shield,                    +5000 points                           Now get Armour", true);
                    objText.text = "Enter someplace to obtain armour";
                    break;
                case 2: //got Armour
                    UpdateMessage(0, "Obtained armour,                    +5000 points                           Now get enchanted", true);
                    objText.text = "Enter someplace to enchant weapons";
                    break;
                case 3: //got Enchanted     
                    UpdateMessage(0, "Obtained enchanted,                 +5000 points                            Now kill dragon", true);
                    objText.text = "Enter the tower ramp to kill the dragon";
                    break;
                case 4: //got Endgame

                    break;
            }          
        }
    }



    //pinball functions-----------------------------------------
    public void LostPinball()
    {
        activePinballs--;

        if(pinballsLeft > 0)
        {
            //Place pinball in chute, cycles through all available chutes
            launchers[alternateLauncher].NewPinball();
            alternateLauncher++;
            if(alternateLauncher == launchers.Length)    
            {
                alternateLauncher = 0;
            }
            pinballsLeft--;
            activePinballs++;
        } else if(activePinballs == 0)
        {
            StartCoroutine(GameOver());
        }
        UpdateLives();
    }
    private IEnumerator GameOver()
    {
        //play animations/sounds:
        Debug.Log("GameOver");

        int temp = GlobalVar.TestHighscore(0,score);

        if(temp == 2)   //got global highscore + personal highscore
        {

        } else if(temp ==1) //got personal highscore only
        {

        } else  //no highscores
        {

        }

        //show highscore screen
        ingameScreen.SetActive(false);
        gameOverScreen.SetActive(true);
        ingameMenu.LoadInfo(score);

        yield break;
    }


    //UI Functions
    void UpdateMessage(int score, string type, bool isPrio)
    {
        //Update totalScore
        scoreText.text = this.score.ToString();

        if (!isPrio && !isShowingMessage)
        {
            //Update message string
            if (oldType == type)
            {
                combo++;
            }
            else
            {
                combo = 1;
            }
            messageText.text = combo + "x  " + type + " " + (score * fieldMult * combo);
        } else if (isPrio) //shows a special message
        {
            messageText.text = type.ToString();
            StartCoroutine(ShowMessage());
        }


        oldType = type;
    }  
    void UpdateMult()
    {
        multText.text = "Mult: " + fieldMult + "x";
    }  
    void UpdateLives()
    {
        liveText.text = pinballsLeft.ToString();
    }
    private IEnumerator ShowMessage()
    {
        isShowingMessage = true;
        yield return new WaitForSeconds(2);
        isShowingMessage = false;
        yield break;
    }
}
