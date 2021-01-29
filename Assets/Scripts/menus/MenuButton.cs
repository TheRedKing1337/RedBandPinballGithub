using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour //sets the highlighted object as the selected one to prevent 2 objects being sized up
{
    public void hoverOver()
    {
        EventSystem.current.SetSelectedGameObject(gameObject, null);
        AudioManager.Instance.Play("hoverOver");
    }
}
