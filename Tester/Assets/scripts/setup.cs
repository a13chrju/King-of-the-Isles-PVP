using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class setup : NetworkBehaviour
{
    public Text canvasText;
    public Material myMat;

    [SyncVar]
    public string myname = "";

    [SyncVar]
    public int Team = 2; //1 = Red team; 2 = yellow team

    [SyncVar]
    public Color TeamColor;

    public override void OnStartClient()
    {
        if (Team == 1)
        {

          //  Material newMat = Resources.Load("red", typeof(Material)) as Material;
         //   this.gameObject.GetComponentInChildren<Renderer>().material = newMat;
         //   myMat = newMat;
        }
        else
        {
         //   Material newMat = Resources.Load("yellow", typeof(Material)) as Material;
         //   this.gameObject.GetComponentInChildren<Renderer>().material = newMat;
          //  myMat = newMat;
        }
     //   GetComponentInChildren<Renderer>().material = myMat;

       
    }
    public Behaviour[] disableComponents;
    // Use this for initialization
    void Start()
    {
        
        if (!isLocalPlayer)
        {
            for (int i = 0; i < disableComponents.Length; i++)
            {
                disableComponents[i].enabled = false;


            }

        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;   // keep confined to center of screen
        }


        string _ID = "Player " + GetComponent<NetworkIdentity>().netId;
        transform.name = _ID;
        canvasText.text = myname;

        TeamManager.PlayerJoins(this.GetComponent<PlayerHealth>(), _ID, Team);

        if (isServer)
        {
            if (Team == 1)
            {
                RpcColorRed(1);
            }
            else
            {
                RpcColorRed(2);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //  Cursor.visible = false;
        if (Input.GetKey(KeyCode.H))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        //transform.LookAt(transform.position + cameraobj.transform.rotation * Vector3.back, cameraobj.transform.rotation * Vector3.up);
       
    }

    [ClientRpc]
    public void RpcColorRed(int value)
    {
     /*   if (value == 1)
        {
          
            Material newMat = Resources.Load("red", typeof(Material)) as Material;
            this.gameObject.GetComponentInChildren<Renderer>().material = newMat;
            myMat = newMat;
        }
        else
        {
            Material newMat = Resources.Load("yellow", typeof(Material)) as Material;
            this.gameObject.GetComponentInChildren<Renderer>().material = newMat;
            myMat = newMat;
        }
       */
    }
}
