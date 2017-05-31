using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    [SerializeField]
    GameObject inGamePanel;
    [SerializeField]
    GameObject characterSelectPanel;
    [SerializeField]
    Button character1;
    [SerializeField]
    Button character2;
    [SerializeField]
    Image characterSprite;

    private bool playerselected = false;

    public void FinishCharacterSelection()
    {
        if(playerselected)
        {
            characterSelectPanel.SetActive(false);
            inGamePanel.SetActive(true);
        }
    }

    public void ChooseCharacter1()
    {
        playerselected = true;
        characterSprite.GetComponent<Image>().sprite = character1.GetComponent<Image>().sprite;
    }

    public void ChooseCharacter2()
    {
        playerselected = true;
        characterSprite.GetComponent<Image>().sprite = character2.GetComponent<Image>().sprite;
    }
}
