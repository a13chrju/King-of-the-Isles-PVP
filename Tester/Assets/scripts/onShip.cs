using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onShip : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent = this.transform;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent = null;

        }
    }
}
