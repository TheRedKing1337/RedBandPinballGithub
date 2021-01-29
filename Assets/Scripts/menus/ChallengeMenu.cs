using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeMenu : MonoBehaviour
{
    public Image[] challengePanels;
    public Toggle[] challengeChecks;

    public Text panelTitle;


    public void UpdateChallenges(bool player, string name)
    {
        for (int i = 0; i < GlobalVar.playerChallenges.Length; i++)
        {
            if (player)
            {
                if (GlobalVar.playerChallenges[i])
                {
                    HasChallenge(challengeChecks[i], challengePanels[i]);
                }
                else
                {
                    HasntChallenge(challengeChecks[i], challengePanels[i]);
                }
            }
            else
            {
                if (GlobalVar.profileChallenges[i])
                {
                    HasChallenge(challengeChecks[i], challengePanels[i]);
                }
                else
                {
                    HasntChallenge(challengeChecks[i], challengePanels[i]);
                }
            }
        }
        panelTitle.text = name + "'s Challenges";
    }
    void HasChallenge(Toggle check, Image panel)
    {
        check.isOn = true;
        panel.color = Color.green;
    }
    void HasntChallenge(Toggle check, Image panel)
    {
        check.isOn = false;
        panel.color = Color.red;
    }
}
