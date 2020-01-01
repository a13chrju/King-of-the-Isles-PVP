using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayAgain : NetworkBehaviour {
    private GameObject LBM;
    // Use this for initialization
    void Start () {
       // LBM = GameObject.Find("LobbyManager");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Comma))
        {
         //   restart();
            //SceneManager.LoadScene("loooby");
        }
		
	}

  
}
