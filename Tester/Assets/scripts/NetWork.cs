using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetWork : NetworkManager {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnClientDisconnect(NetworkConnection connection)
    {
       
        //Change the text to show the connection loss on the client side
        Debug.Log(connection);

       //TeamManager.
    }
}
