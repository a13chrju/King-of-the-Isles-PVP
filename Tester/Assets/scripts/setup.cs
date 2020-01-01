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
    public int Lives = 3;

    [SyncVar]
    public Color TeamColor;

    [SyncVar]
    public string lobbyNetworkID;

    public override void OnStartClient()
    {
        //Color.red, Color.yellow, Color.blue, Color.black, Color.green, Color.white
        Debug.Log("skin" + Team.ToString());
        Material newMat = Resources.Load("skin" + Team.ToString()) as Material;
        this.gameObject.GetComponentInChildren<Renderer>().material = newMat;
        myMat = newMat;

        GetComponentInChildren<Renderer>().material = myMat;

        TeamManager.cursorLocked = true;
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
            Cursor.visible = false;
            TeamManager.cursorLocked = true;
            Cursor.lockState = CursorLockMode.Locked;
        }


        string _ID = "Lala " + GetComponent<NetworkIdentity>().netId;
        string _LNID = "DPlayer " + lobbyNetworkID;
        transform.name = _LNID;

        if (!isLocalPlayer)
        {
            canvasText.text = myname;
        }

        TeamManager.PlayerJoins(this.GetComponent<Health>(), Team, _LNID, _ID);

        if (isServer)
        {
            RpcColorRed();
            TeamManager.serverNetID = _LNID;
        }
    }


    // Update is called once per frame
    void LateUpdate()
    {
        if (isLocalPlayer)
        {
            if (TeamManager.cursorLocked == false && Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("AAAAAAAAAAAAA");
                Cursor.lockState = CursorLockMode.None;
                TeamManager.cursorLocked = true;
                Cursor.visible = false;

                Cursor.lockState = CursorLockMode.Locked;
            }
            else if (TeamManager.cursorLocked && Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("OOOOOOOOOOOOOO");
                Cursor.lockState = CursorLockMode.None;
                TeamManager.cursorLocked = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

      
        //transform.LookAt(transform.position + cameraobj.transform.rotation * Vector3.back, cameraobj.transform.rotation * Vector3.up);

    }

    [ClientRpc]
    public void RpcColorRed()
    {
        Material newMat = Resources.Load("skin" + Team.ToString()) as Material;
        this.gameObject.GetComponentInChildren<Renderer>().material = newMat;
        myMat = newMat;

    }

    private void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("aServer is stopped from PlayerPrefab");
    }

    public virtual void OnClientDisconnect(NetworkConnection conn)
    {
        Debug.Log("Server is stopped from PlayerPrefab");
    }
}
