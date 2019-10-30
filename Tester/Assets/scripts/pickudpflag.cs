using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupflag : MonoBehaviour {

    // Use this for initialization
    public int typeofteam = 0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" )
        {
            Debug.Log(other.gameObject.GetComponent<setup>().Team);

        }
        
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<setup>().Team == typeofteam)
        {
            Debug.Log("correct");
            var team = other.gameObject.GetComponent<setup>().Team;
         //   other.gameObject.GetComponent<capture>().captureflag(team, true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<setup>().Team == typeofteam)
        {
            var team = other.gameObject.GetComponent<setup>().Team;
        //    other.gameObject.GetComponent<capture>().nocaptureflag(team);

        }
    }
}
