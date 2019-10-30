using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class destroyafter : NetworkBehaviour {
    public float tdsaime;
    public float lifespan;
	// Use this for initialization
	void Start () {
        if (isServer)
        {
            tdsaime = Time.time + lifespan;
        }
      
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > tdsaime)
        {
            if (isServer)
            {
                NetworkServer.Destroy(this.gameObject);
                Destroy(this);
                return;
            }
        }
	}
}
