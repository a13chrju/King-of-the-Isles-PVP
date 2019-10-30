using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class facecamera : NetworkBehaviour {
    public Camera cameraobj;
    public bool isset = true;
	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {


        Debug.Log(TeamManager.localCamera);
        if (isset == true && (!isLocalPlayer))
        {
            
            cameraobj = TeamManager.localCamera;
            transform.LookAt(transform.position + cameraobj.transform.rotation * Vector3.back, cameraobj.transform.rotation * Vector3.up);
        }

        

    }
}
