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

        localPlayer.Team = lobby.teampicked;
        localPlayer.TeamColor = lobby.playerColor;
        localPlayer.myname = lobby.playerName;
        localPlayer.lobbyNetworkID = lobby.GetComponent<NetworkIdentity>().netId.ToString();


    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("WOT DISCONNECTED");
    }

    private void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        Debug.Log("WOT OPSI");
    }

}
