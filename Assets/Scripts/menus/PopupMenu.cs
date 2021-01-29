using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMenu : MonoBehaviour
{
    public Text message;
    public void SetMessage(string text)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        message.text = text;
    }
}
