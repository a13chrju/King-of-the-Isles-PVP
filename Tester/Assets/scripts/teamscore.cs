using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class teamscore : NetworkBehaviour {

    // Use this for initialization
    public Text scoretxt;
    public Sprite red, gul, both, none;Image ImageFlags;

    [SyncVar]
    public int TeamScorered = 0;

    [SyncVar]
    public bool redflagpicked = false;

    [SyncVar]
    public int TeamScoreyell = 0;

    [SyncVar]
    public bool yelflagpicked = false;


    void Start () {
        InvokeRepeating("updateui", 0, 1.0f);
    }

    public void Update()
    {
        //updateui();
    }

    public void updateui()
    {
        CmdAdjustScordsadase();
    }

    public void flagpickedup(int team)
    {
        if (team == 1)
        {
           // redflagpicked = true;
            CmdflagputUp(1);
        }
        else
        {
            CmdflagputUp(2);
           // yelflagpicked = true;
        }
    }

    [Command]
    void CmdflagputUp(int team)
    {
        if (team == 1)
        {
            redflagpicked = true;
        }
        else
        {
            yelflagpicked = true;
        }
      
    }

    
    public void captureConverter(int team)
    {
        CmdflagputUp(team);
    }


    [ClientRpc]
    public void Rpcupdateflaghaspicture()
    {
        ImageFlags = GameObject.FindGameObjectWithTag("teamscore").GetComponentInChildren<Image>();

        if (redflagpicked == true && yelflagpicked == true)
        {
            Debug.Log("1fsafas");
            ImageFlags.sprite = both;
        }
        else if(redflagpicked == true && yelflagpicked == false)
        {
            Debug.Log("2fsafas");
            ImageFlags.sprite = red;
        }
        else if (redflagpicked == false && yelflagpicked == true)
        {
            Debug.Log("3fsafas");
            ImageFlags.sprite = gul;
        }
        else
        {
            Debug.Log("4fsafas");
            ImageFlags.sprite = none;
        }

    }

    public void downflagpickedup(int team)
    {
        if (team == 1)
        {
           // redflagpicked = false;
            Cmdflagputdown(1);
        }
        else
        {
            Cmdflagputdown(2);
        }
       // Rpcupdateflaghaspicture();
    }

    [Command]
    void Cmdflagputdown(int team)
    {
        if (team == 1)
        {
            redflagpicked = false;
        }
        else
        {
            yelflagpicked = false;
        }
    }

    [Command]
    void CmdAdjustScordsadase()
    {
       
        Rpcredshowflag();
        Rpcupdateflaghaspicture();
    }

    [ClientRpc]
    public void Rpcredshowflag()
    {
        scoretxt.text = TeamScorered + " / "+ TeamScoreyell;

    }


}

