using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreBoardItem : MonoBehaviour {

    public Text PlayerName;
    public Text IsAlive;
    public Text Lives;
    public Image TeamColor;
    // Use this for initialization
    void Start () {
		
	}
	
    public void setup(string IsAlive, Color color, int lives, string PlayerName = "no Name")
    {
        this.PlayerName.text = PlayerName;
        //  this.IsAlive.text = IsAlive;
        this.Lives.text = lives.ToString();
        this.TeamColor.color = color;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
