using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class fireCool : NetworkBehaviour {
    public Image fire_icon;
    public float cooldown;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (isLocalPlayer)
        {
            CmdCool();
        }
     
    }

    public void shot(){
        cooldown = Time.time + 3f;
    }

    [Command]
    public void CmdCool()
    {
        //Apply it to all other clients
        RpcCool();
    }

    [ClientRpc]
    public void RpcCool()
    {
        if (fire_icon.fillAmount > 0.99)
        {
            fire_icon.fillAmount = 0;
        }

        if (Time.time < cooldown)
        {

            fire_icon.fillAmount += 1 / 3f * Time.deltaTime;
        }


    }
}
