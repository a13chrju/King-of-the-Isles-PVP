using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class jump : NetworkBehaviour {

	// Use this for initialization
    public GameObject player;
	void Start () {
 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FireNow()
    {
      
           // Debug.Log("shotfireball");
           // this.GetComponentInParent<fireballs>().FireBallShot();
       
    
    }

    public void JumpNow()
    {
       
            //Debug.Log("ssssss");
           // this.GetComponentInParent<move>().Jump();
        
       
    }

    public void IceNow()
    {
     
           // Debug.Log("ice shield");
           // this.GetComponentInParent<builder>().buildWall();
        
     
    }
}
