using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVar //GlobalVar stores values from the server to use in UI and other functions, it also contains some functions to decode data sent in from ServerFunc
{
    public static bool isStartup = true;

    //User login vars
    public static int playerID;
    public static string playerName = "default";
    public static int playerRank = 0;
    public static string playerIcon = "default";
    public static bool[] playerChallenges = {false,false,false,false,false, false, false, false, false };
    public static int[,] playerHighscores = { {5,4,3,2,1 }, { 5, 4, 3, 2, 1 }, { 5, 4, 3, 2, 1 } };

    //store loaded profile here, separate from the active login data
    public static int profileID;
    public static string profileName;
    public static int profileRank;
    public static string profileIcon = "default";
    public static bool[] profileChallenges = { false, false, false, false, false, false, false, false, false };
    public static int[,] profileHighscores = { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };

    //the general highscores array here, 10 highscores per table, dit ziet er heel slordig uit er moet een betere manier zijn
    public static int[,] highscoresIDs = { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
    public static int[,] highscores = { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
    public static string[,] highscoresNames = { { "none", "none", "none", "none", "none", "none", "none", "none", "none", "none" }, { "none", "none", "none", "none", "none", "none", "none", "none", "none", "none" }, { "none", "none", "none", "none", "none", "none", "none", "none", "none", "none" } };

    public static void SaveData(string rawData, bool player)    //takes in raw profile data string, splits it and saves in correct place
    {
        string[] data = rawData.Split('|');                     //splits the incoming string into an array, ex: TheRedKing1337|0|default|False/True/False/False/False|0/0/0/0/0/0/0/0/0/0/0/0/0/0/0|1

        if (player)
        {
            playerName = data[0];
            playerRank = int.Parse(data[1]);
            playerIcon = data[2];
            SetChallenges(data[3], true);
            SetHighscores(data[4], true);
            playerID = int.Parse(data[5]);
        }
        else
        {
            profileName = data[0];
            profileRank = int.Parse(data[1]);
            profileIcon = data[2];
            SetChallenges(data[3], false);
            SetHighscores(data[4], false);
        }
    }
    
    static void SetChallenges(string input, bool player)
    {
        string[] data = input.Split('/');
        for(int i=0;i<playerChallenges.Length;i++)
        {
            if(player)
            {
                playerChallenges[i] = bool.Parse(data[i]);
            } else {
                profileChallenges[i] = bool.Parse(data[i]);
            }
        }
    }
    static void SetHighscores(string input, bool player)
    {
        string[] data = input.Split('/');
        for (int i = 0; i < 15; i++)
        {
            int tableI = Mathf.FloorToInt(i / 5);
            if (player)
            {
                playerHighscores[tableI, i - (tableI * 5)] = int.Parse(data[i]);
            }
            else
            {
                profileHighscores[tableI, i - (tableI * 5)] = int.Parse(data[i]);
            }
        }
    }
    public static void SetGeneralHighscores(string rawData, int ID)
    {
        string[] data = rawData.Split('|');             //splits the data into 10 datasets

        for(int i=0;i<10;i++)                           //loop through them
        {
            string[] finalData = data[i].Split('/');    //splits the datasets into: the ID, the score and the name

            highscoresIDs[ID, i] = int.Parse(finalData[0]);
            highscores[ID, i] = int.Parse(finalData[1]);
            highscoresNames[ID, i] = finalData[2];
        }
    }
    public static void UpdateInfo(string rawData)
    {
        string[] data = rawData.Split('|');

        playerName = data[0];
        playerIcon = data[1];
    }




    public static void ObtainChallenge(int ID)
    {
        playerChallenges[ID] = true;

        string challenges = "";

        for(int i=0;i<playerChallenges.Length;i++)
        {
            challenges += playerChallenges[i].ToString() + "/";
        }
        Debug.Log(challenges);

        StaticCoroutine.Start(ServerFunc.UpdateChallenges(challenges));
    }
    public static int TestHighscore(int tableID, int score)
    {
        //tests if the obtained score is a highscore, if so update the database, this function has can only be called after testing if player is logged in
        int temp =0;
        //test for personal highscore
        for(int i=0;i<5;i++)
        {
            if (score > playerHighscores[tableID, i])
            {
                //new highscore is score with ID i
                UpdatePlayerHighscores(tableID,score,i);
                Debug.Log("Obtained a new personal highscore on table: " + LevelInfo.tables[tableID].name + " position: " + (1 + i) + " with score: " + score + "\n");
                temp++;
                break;
            }
        }
        //test for global highscore, doesnt need to update the scores since the highscore board will do that at the end of game     
        for (int i = 0; i < 10; i++)
        {
            if (score > highscores[tableID, i])
            {
                //new highscore is score with ID i
                UpdateGlobalHighscores(tableID, score, i);
                Debug.Log("Obtained a new global highscore on table: " + LevelInfo.tables[tableID].name + " position: " + (1+i)  + " with score: " + score + "\n");
                temp++;
                break;
            }
        }
        return temp;
    }
    public static void UpdatePlayerHighscores(int tableID, int score, int ID)
    {
        //move all places below the new highscore one down
        for (int i = 0; i < 4 - ID; i++)
        {
            playerHighscores[tableID, 4 - i] = playerHighscores[tableID, 3 - i];
        }
        //sets the score in the obtained position
        playerHighscores[tableID, ID] = score;

        //updates the database 
        string scoreString = "'";
        for(int i=0;i<3;i++)
        {
            for (int t = 0; t < 5; t++)
            {
                scoreString += playerHighscores[i, t] + "/";
            } 
        }
        scoreString += "'";

        StaticCoroutine.Start(ServerFunc.SetPersonalHighscores(tableID,scoreString));
    }
    public static void UpdateGlobalHighscores(int tableID, int score, int ID)
    {
        for (int i = 0; i < 9 - ID; i++)
        {
            highscores[tableID, 9 - i] = highscores[tableID, 8 - i];
            highscoresIDs[tableID, 9 - i] = highscoresIDs[tableID, 8 - i];
            highscoresNames[tableID, 9 - i] = highscoresNames[tableID, 8 - i];
        }
        //sets the score in the obtained position
        highscores[tableID,ID] = score;
        highscoresIDs[tableID, ID] = playerID;
        highscoresNames[tableID,ID] = playerName;

        //updates the database 
        string scoreString = "";
        string plrIDs = "";
        for (int i = 0; i < 10; i++)
        {
            scoreString += highscores[tableID, i] +"|";
            plrIDs += highscoresIDs[tableID, i] + "|";
        }

        StaticCoroutine.Start(ServerFunc.SetHighscores(tableID, scoreString, plrIDs));
    }
}