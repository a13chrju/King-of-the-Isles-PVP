using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class changemytext : NetworkBehaviour {



    
    [SyncVar]
    public string mytext;

    void Start()
    {

        StartCoroutine(hideCanvasTxt());
    }

    IEnumerator InitCoroutine()
    {
        yield return new WaitForEndOfFrame();

        // Do your code here to assign game objects
        hideCanvasTxt();
    }

    // Update is called once per frame
    void Update () {
   
    }




    private IEnumerator hideCanvasTxt()
    {

        yield return new WaitForSeconds(3f);
        NetworkServer.Destroy(this.gameObject);

    }

}
