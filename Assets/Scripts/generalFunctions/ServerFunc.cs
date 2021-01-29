using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; //include networking which includes UnityWebRequest/WWWForm

public static class ServerFunc //ServerFunc contains all the functions that communicate with the server, after getting the data it sends it to GlobalVar for storing/decoding
{
    //offline URLs
    private static string loadProfileURL = "http://www.testsite.com/RedBandPinball/loadProfile.php";
    private static string loginURL = "http://www.testsite.com/RedBandPinball/login.php";
    private static string createProfileURL = "http://www.testsite.com/RedBandPinball/createProfile.php";
    private static string updateInfoURL = "http://www.testsite.com/RedBandPinball/updateInfo.php";
    private static string updateChallengeURL = "http://www.testsite.com/RedBandPinball/updateChallenge.php";
    private static string setPersonalHighscoresURL = "http://www.testsite.com/RedBandPinball/setPersonalHighscores.php";
    private static string getHighscoresURL = "http://www.testsite.com/RedBandPinball/getHighscores.php";
    private static string setHighscoresURL = "http://www.testsite.com/RedBandPinball/setHighscores.php";

    //online URLs
    //private static string loadProfileURL = "https://riktestsite.stateu.org/loadProfile.php";
    //private static string loginURL = "https://riktestsite.stateu.org/login.php";
    //private static string createProfileURL = "https://riktestsite.stateu.org/createProfile.php";
    //private static string updateInfoURL = "https://riktestsite.stateu.org/updateInfo.php";
    //private static string updateChallengeURL = "https://riktestsite.stateu.org/updateChallenge.php";
    //private static string setPersonalHighscoresURL = "https://riktestsite.stateu.org/setPersonalHighscores.php";
    //private static string getHighscoresURL = "https://riktestsite.stateu.org/getHighscores.php";
    //private static string setHighscoresURL = "https://riktestsite.stateu.org/setHighscores.php";

    //profile related functions
    public static IEnumerator LoadProfile(int ID) //gets profile data from the ID and saves it as player if player is true, to profile if not 
    {
        //setup request form
        WWWForm form = new WWWForm();
        form.AddField("ID", ID);

        //send request for data with ID and stores it in a string
        string data = "";

        using (UnityWebRequest profileData = UnityWebRequest.Post(loadProfileURL, form))
        {
            yield return profileData.SendWebRequest();
            data = profileData.downloadHandler.text;
        }
    
        //check for error messages, if so break
        if(ContainsErrors(data))
        {
            Debug.Log("cancelled loading, error encountered"); 
            yield break;
        }

        //store data globalVar
        GlobalVar.SaveData(data, false);

        //Debug.Log("Succesfully obtained profile with ID "+ ID + " from the database and passed it into GlobalVar");

        yield break;
    }
    public static IEnumerator Login(string name, string password)
    {
        //setup request form
        WWWForm form = new WWWForm();
        form.AddField("plrName", "'" + name + "'");
        form.AddField("password", password);

        //send request for data with ID and stores it in a string
        string data = "";

        using (UnityWebRequest profileData = UnityWebRequest.Post(loginURL, form))
        {
            yield return profileData.SendWebRequest();
            data = profileData.downloadHandler.text;
        }

        //check for error messages, if so break
        if (ContainsErrors(data))
        {
            Debug.Log("cancelled loading, error encountered");
            yield break;
        }

        //store data globalVar
        GlobalVar.SaveData(data, true);

        yield break;
    }
    public static IEnumerator CreateProfile(string email, string name, string password)
    {
        //send to server, test if name already exists, if it does send error back if not create profile
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("name", name);
        form.AddField("password", password);

        string data = "";

        using (UnityWebRequest profileData = UnityWebRequest.Post(createProfileURL, form))
        {
            yield return profileData.SendWebRequest();
            data = profileData.downloadHandler.text;
        }

        if (ContainsErrors(data))
        {
            Debug.Log("cancelled loading, error encountered");
            yield break;
        }

        //store data globalVar
        GlobalVar.SaveData(data, true);

        yield return null;      //wait one frame for GlobalVar to store data

        yield break;
    }
    //one for updating name/icon, have it in one function even if you only change 1 for simplicity
    public static IEnumerator UpdateInfo(string name, string icon) 
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", GlobalVar.playerID);
        form.AddField("plrName",name);
        form.AddField("plrIcon", icon);

        string data = "";

        using (UnityWebRequest profileData = UnityWebRequest.Post(updateInfoURL, form))
        {
            yield return profileData.SendWebRequest();
            data = profileData.downloadHandler.text;
        }

        if (ContainsErrors(data))
        {
            Debug.Log("cancelled loading, error encountered");
            yield break;
        }

        GlobalVar.UpdateInfo(data);

        yield break;
    }

    //one for updating a given challenge, this also updates the rank
    public static IEnumerator UpdateChallenges(string challenges)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", GlobalVar.playerID);
        form.AddField("challenges", "'" + challenges + "'");

        string data = "";

        using (UnityWebRequest profileData = UnityWebRequest.Post(updateChallengeURL, form))
        {
            yield return profileData.SendWebRequest();
            data = profileData.downloadHandler.text;
        }

        if (ContainsErrors(data))
        {
            Debug.Log("cancelled loading, error encountered");
            yield break;
        }
        yield break;
    }

    //one for updating a given highscore table
    public static IEnumerator SetPersonalHighscores(int ID, string scores)
    {
        string data = "";

        WWWForm form = new WWWForm();
        form.AddField("scores", scores);
        form.AddField("plrID", GlobalVar.playerID);

        using (UnityWebRequest highscoreData = UnityWebRequest.Post(setPersonalHighscoresURL, form))
        {
            yield return highscoreData.SendWebRequest();
            data = highscoreData.downloadHandler.text;
        }

        //checks for error
        if (ContainsErrors(data))
        {
            Debug.Log("cancelled loading, error encountered");
            yield break;
        }

        yield break;
    }

    //highscore functions
    public static IEnumerator GetHighscores(int ID) //sets the highscores array in globalVar for the selected table ID
    {
        //request the highscores string from the server
        string data = "";

        WWWForm form = new WWWForm();
        form.AddField("ID", ID);

        using (UnityWebRequest highscoreData = UnityWebRequest.Post(getHighscoresURL, form))
        {
            yield return highscoreData.SendWebRequest();
            data = highscoreData.downloadHandler.text;
        }

        //checks for error
        if (ContainsErrors(data))
        {
            Debug.Log("cancelled loading, error encountered");
            yield break;
        }

        //sends the data to globalVar for processing and storing
        GlobalVar.SetGeneralHighscores(data, ID);

        yield break;
    }
    public static IEnumerator SetHighscores(int ID, string scores, string plrIDs)
    {
        //send data to server, put in 0-9 if ID=0 10-19 if 1 etc
        //the script that calls this will have calculated the right order and formatting for the string

        WWWForm form = new WWWForm();
        form.AddField("ID", ID);
        form.AddField("scores", scores);
        form.AddField("plrIDs", plrIDs);

        string data = "";

        using (UnityWebRequest highscoreData = UnityWebRequest.Post(setHighscoresURL, form))
        {
            yield return highscoreData.SendWebRequest();
            data = highscoreData.downloadHandler.text;
        }

        //checks for error
        if (ContainsErrors(data))
        {
            Debug.Log("cancelled loading, error encountered");
            yield break;
        }

        yield break;
    }






    //misc functions
    static bool ContainsErrors(string input) //if string is empty or contains and error message return true
    {                                        //may need way for showing error popup outside of console, a custom popup button
        if(input == "succes")
        {
            Debug.Log("Succesfully sent update to database");
            return false;
        }
        if (input == "" || input == null)
        {
            Debug.Log("Wasnt able to connect to server");
            return true;
        }
        else if (input.Contains("error") || input.Contains("<br"))
        {
            Debug.Log("You encountered an error: \n"+input);
            GameObject popupMenu = GameObject.Find("PopupMenu");
            popupMenu.GetComponent<PopupMenu>().SetMessage(input);
            return true;
        }
        else
        {
            return false;
        }
    }
}
