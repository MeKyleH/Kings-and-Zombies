using UnityEngine;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class LobbyToSceneHook : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer,
        GameObject gamePlayer)
    {
        //Syncs variables from the lobby to the game
        //This change triggers the hooks in Player.cs
        LobbyPlayer lPlayer = lobbyPlayer.GetComponent<LobbyPlayer>();
        Player gPlayer = gamePlayer.GetComponent<Player>();

        gPlayer.playerName = lPlayer.playerName;
        gPlayer.playerColor = lPlayer.playerColor;
    }
}