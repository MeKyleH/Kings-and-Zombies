using UnityEngine;
using UnityEngine.UI;
using Prototype.NetworkLobby;

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
    [SerializeField]
    GameObject[] characterArr;

    private LobbyManager lobbyManager;
    private bool playerselected = false;

    void Start()
    {
        lobbyManager = GameObject.FindObjectOfType<LobbyManager>();
        if(!lobbyManager)
        {
            Debug.Log(name + " couldn't find lobbyManager");
        }
    }

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
        PlayerPrefsManager.SetChosenPlayer(0);
        lobbyManager.gamePlayerPrefab = characterArr[0];
    }

    public void ChooseCharacter2()
    {
        playerselected = true;
        PlayerPrefsManager.SetChosenPlayer(1);
        lobbyManager.gamePlayerPrefab = characterArr[1];

    }
}
