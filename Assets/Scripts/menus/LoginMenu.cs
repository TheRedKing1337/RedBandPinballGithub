using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{
    public ProfileMenu profileMenu;

    public Text panelTitle;

    public GameObject loginMenu;
    public GameObject registerMenu;

    public InputField nameField;
    public InputField passwordField;

    public InputField emailField;

    public Image loginButton;
    public Image registerButton;

    private bool loginActive = true;

    public void TryLogin()
    {
        AudioManager.Instance.Play("select");
        StartCoroutine(Login());
    }
    IEnumerator Login()
    {
        yield return StartCoroutine(ServerFunc.Login(nameField.text, passwordField.text));

        if (GlobalVar.playerID != 0) //if logged in
        {
            profileMenu.LoadProfile(9999);
        }
    }

    public void TryRegister()    //function to Try create new account, the ServerFunc will generate a popup if name already exists
    {
        AudioManager.Instance.Play("select");
        StartCoroutine(Register());
    }
    IEnumerator Register()
    {
        yield return StartCoroutine(ServerFunc.CreateProfile(emailField.text, nameField.text, passwordField.text));

        if (GlobalVar.playerID != 0) //if logged in
        {
            profileMenu.LoadProfile(9999);
        }

        yield break;
    }

    public void SwitchMenu()   //function to switch between login menu and create account menu    it will keep name/password inputField always active, only switch out buttons, emailField and Title of Panel
    {
        AudioManager.Instance.Play("select");
        loginActive = !loginActive;
        loginMenu.SetActive(loginActive);
        registerMenu.SetActive(!loginActive);

        if(loginActive){
            panelTitle.text = "Login";
        } else {
            panelTitle.text = "Register";
        }
    }

    void FixedUpdate()         //update login button to prevent empty logins
    {
        if (loginActive)
        {
            if (nameField.text == "" || passwordField.text == "")
            {
                loginButton.color = Color.red;
                loginButton.GetComponent<Button>().enabled = false;
            }
            else
            {
                loginButton.color = Color.green;
                loginButton.GetComponent<Button>().enabled = true;
            }
        } else {
            if (nameField.text == "" || passwordField.text == "" || emailField.text == "")
            {
                registerButton.color = Color.red;
                registerButton.GetComponent<Button>().enabled = false;
            }
            else
            {
                registerButton.color = Color.green;
                registerButton.GetComponent<Button>().enabled = true;
            }
        }
    }
}
