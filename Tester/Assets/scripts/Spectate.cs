using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Spectate : NetworkBehaviour {
    public Text canvas_txt; public GameObject deathCam;
    public bool continueChecking = true;
    int counter = 0;
    // Use this for initialization
   /* void Start() {
        canvas_txt = GameObject.FindGameObjectWithTag("awesome").GetComponentInChildren<Text>();
        deathCam = GameObject.FindGameObjectWithTag("deathcam");
  

    }*/

    public override void OnStartClient()
    {
        canvas_txt = GameObject.FindGameObjectWithTag("awesome").GetComponentInChildren<Text>();
        deathCam = GameObject.FindGameObjectWithTag("deathcam");

    }


    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {

        if (this.GetComponent<Health>().isalive == false && isLocalPlayer && continueChecking)
        {
            
            int alive = TeamManager.alive();

            if (alive <= 0 )
            {
                deathCam.GetComponentInChildren<Camera>().enabled = true;
                this.GetComponentInChildren<Camera>().enabled = false;
                continueChecking = false;
            }

        }

        if (this.GetComponent<Health>().isalive == false && isLocalPlayer && Input.GetKeyDown(KeyCode.Mouse0))
        {
            string PlayerNetID = TeamManager.switchCamera();
            canvas_txt.text = "Spectating\nLeft Click to Switch";
            Debug.Log(PlayerNetID);
            var players = GameObject.FindGameObjectsWithTag("Player");

            foreach (var item in players)
            {
                if (item.transform.name == PlayerNetID)
                {
                    deathCam.GetComponentInChildren<Camera>().enabled = false;
                    this.GetComponentInChildren<Camera>().enabled = false;
                    item.GetComponentInChildren<Camera>().enabled = true;
                }
                else
                {
                    if (item.GetComponentInChildren<Camera>() != null)
                    {
                        item.GetComponentInChildren<Camera>().enabled = false;
                    }
                  
                }

            }
        }


    }
}
