using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour {

    public GameObject Scoreboard;
    public bool isopened = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab) )
        {
            Scoreboard.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab) )
        {
            Scoreboard.SetActive(false);
        }
	}
}
