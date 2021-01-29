using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ToggleLight : MonoBehaviour
{
    public bool state;
    private bool animating;

    void OnTriggerEnter()
    {
        if (!animating)
        {
            ToggleState();
            AudioManager.Instance.Play("lightBleep");
            DragonGameManager.Instance.CheckLights();
            DragonGameManager.Instance.AddScore("light");
        }
    }

    public void UpgradeLights()
    {
        StartCoroutine(AnimateLights());
    }
    private IEnumerator AnimateLights()
    {
        ToggleState();
        animating = true;
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        //flash light
        for (int i = 0; i < 3; i++)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("lightTrue");
            transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("lightFalse");
            transform.GetChild(0).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
        animating = false;
        yield break;
    }

    public void ToggleState()
    {
        state = !state;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("light" + state.ToString());
        transform.GetChild(0).gameObject.SetActive(state);
    }
}
