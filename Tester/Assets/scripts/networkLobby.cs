using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class networkLobby : LobbyHook {

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        setup localPlayer = gamePlayer.GetComponent<setup>();
        if (lobby.playerColor == Color.red)
        {
            Debug.Log("got em bois");
            localPlayer.Team = 1;
        }
        else
        {
            localPlayer.Team = 2;
        }
       
        localPlayer.TeamColor = lobby.playerColor;
        localPlayer.myname = lobby.playerName;
        
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
