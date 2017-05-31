using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainPanel;
    [SerializeField]
    GameObject characterSelectPanel;
    [SerializeField]
    Button character1;
    [SerializeField]
    Button character2;

    private bool playerselected = false;

    public void FinishCharacterSelection()
    {
        if(playerselected)
        {
            characterSelectPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
    }

    public void ChooseCharacter1()
    {
        playerselected = true;
        PlayerPrefsManager.SetChosenPlayer(1);
    }

    public void ChooseCharacter2()
    {
        playerselected = true;
        PlayerPrefsManager.SetChosenPlayer(2);
    }
}
