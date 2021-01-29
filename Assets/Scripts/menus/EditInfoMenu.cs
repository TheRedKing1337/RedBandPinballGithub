using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditInfoMenu : MonoBehaviour
{
    public InputField nameField;
    public Toggle[] imageOptions;

    private string[] images;
    private int selectedImage;    

    void Awake()    //for clarity written out like this
    {
        images = new string[6];
        images[0] = "default";
        images[1] = "redBand";
        images[2] = "sky";
        images[3] = "TBD4";
        images[4] = "TBD5";
        images[5] = "TBD6";
    }
    public void SetUI()
    {
        nameField.text = GlobalVar.playerName;

        //set the right image to be selected
        int imageIndex = GetImageIndex(GlobalVar.playerIcon);
        selectedImage = imageIndex;
        imageOptions[imageIndex].Select();
    }
    public void SubmitChoices()
    {
        //submit values
        StartCoroutine(ServerFunc.UpdateInfo(nameField.text, images[selectedImage]));

        //return to profile
        ReturnMenu();
    }
    public void ReturnMenu()
    {
        GameObject.Find("ProfileMenu").GetComponent<ProfileMenu>().LoadProfile(9999);
    }
    public void SelectImage(int index) //called by any toggle when clicked, sets the selected toggle
    {
        selectedImage = index;
    }

    private int GetImageIndex(string imageName) //returns the index of image array from its name
    {
        int a = 0;
        for(int i=0;i<images.Length;i++)
        {
            if(images[i] == imageName)
            {
                a = i;
                break;
            }
        }
        return a;
    }
}
