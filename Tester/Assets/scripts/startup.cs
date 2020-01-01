using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class startup : NetworkBehaviour {

    // Use this for initialization
    void Start()
    {
      
    }

    public IEnumerator Example()
    {

        yield return new WaitForSeconds(2);
        
            Debug.Log(TeamManager.AllPlayerLength());
            TeamManager.startGame();


    }

    public override void OnStartClient()
    {
        StartCoroutine(Example());

    }
    // Update is called once per frame
    void Update () {
		
	}
}
