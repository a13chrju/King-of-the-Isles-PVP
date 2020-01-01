using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System;

public class TeamManager : MonoBehaviour
{
    public static Dictionary<string, Health> alivePlayers = new Dictionary<string, Health>();
    public static Dictionary<string, Health> PlayersYellow = new Dictionary<string, Health>();
    public static Dictionary<string, Health> PlayersRed = new Dictionary<string, Health>();
    public static string captureKing;
    public static bool kingexist = false;
    public static string tickKing = ""; //finally count down as a king

    //score
    public static int yellowpoints = 0;
    public static int redpoints = 0;

    public static List<GameObject> allPoints = new List<GameObject>();
    public static GameObject visibleg = new GameObject();
    public static bool hasspawned = false;
    public static int antal = 1;
    public static bool redflagup = false;
    public static bool yellowflagup = false;


    public static string serverNetID;
    public static string dead ="";
    public enum Team { Red = 1  , Blue = 2};
    public static GameObject Popup;

    public static string playerWinnerTagName = "";
    public static int indexVal = -1;
    public static Camera localCamera;
    // Use this for initialization

    public static bool cursorLocked = true;
    void Start () {
     
    }

    public static void setCamera(Camera inputcamera)
    {
        localCamera = inputcamera;
    }

    public static void clearAll()
    {
        TeamManager.cursorLocked = true;
        alivePlayers = new Dictionary<string, Health>();
    }

    public static Health[] returnPlayers()
    {
        var intexremove = -1;

        for (var i = 0; i < alivePlayers.Count; i++)
        {
            if (alivePlayers.ElementAt(i).Value == null)
            {
                intexremove = i;
            }

        }

        if (intexremove > 0)
        {
            alivePlayers.Remove(alivePlayers.ElementAt(intexremove).Key);
        }



        Debug.Log(alivePlayers);
        return alivePlayers.Values.OrderBy(x => x.GetComponent<setup>().Team).ToArray();
    }

    public static void PlayerRemove(string netID)
    {
       
        alivePlayers.Remove(netID);
        AllPlayerLength();
    }

    public static bool someOneisAlive()
    {
        Health[] all = alivePlayers.Values.OrderBy(x => x.GetComponent<setup>().Team).ToArray();
        var some1isalive = false;
        var playersalive = 0;
        foreach(var item in all)
        {
            if (item.GetComponent<Health>().isalive == true)
            {
                playersalive++;
            }
        }

        if (playersalive == 1)
        {
            some1isalive = true;
        }

        return some1isalive;
    }

    public class Winner
    {
        public string Name;
        public int Team;
        public bool WinnerYes;
        public Winner(string name, int team, bool winner)
        {
            Name = name;
            Team = team;
            WinnerYes = winner;
        }
    }

    public static string getWinnerFinal()
    {
        Health[] all = alivePlayers.Values.Where(x => x.GetComponent<Health>().isalive).ToArray();
        // var some1isalive = false;
        var winner = "";
       
        if (all.Count() == 1)
        {
            winner = all.FirstOrDefault().transform.name;
        }

        return winner;
    }



    public static void deadplayer(string name)
    {
        dead = name;
    }
    public static int GetAllPlayerLength()
    {
        Debug.Log( "ALL: " + alivePlayers.Count.ToString());

        foreach (var item in alivePlayers.Values)
        {
            Debug.Log(item + "has: " + item.GetComponent<Health>().Lives.ToString());


        }
        return alivePlayers.Values.Where(x => x.GetComponent<Health>().Lives != 0).Count();
    }

    public static string GetWinner()
    {
        return alivePlayers.Values.Where(x => x.GetComponent<Health>().isalive).First().transform.name;
    }
    public static string AllPlayerLength()
    {
        return "ALL: " +alivePlayers.Count.ToString() + ";RED: " + PlayersRed.Count.ToString()+ "BLUE: " + PlayersYellow.Count.ToString();

       
    }

    public static void startGame()
    {
        GameObject CanvasBox = (GameObject)GameObject.FindGameObjectWithTag("opening");
        CanvasBox.GetComponent<Animator>().SetTrigger("runit");

    }

    public static void youwin()
    {
        GameObject CanvasBox = (GameObject)GameObject.FindGameObjectWithTag("youwin");
        CanvasBox.GetComponent<Animator>().SetTrigger("runit");

    }

    public static void youlose()
    {
        GameObject CanvasBox = (GameObject)GameObject.FindGameObjectWithTag("youlose");
        CanvasBox.GetComponent<Animator>().SetTrigger("runit");

    }


    public static string getWinner()
    {
        var keyList = new List<Health>(alivePlayers.Values);
        Debug.Log(alivePlayers.Count.ToString());


        var name = "";
        if (alivePlayers.Count == 1 )
        {
            name = keyList[0].transform.name;
        }
        return name;
    }
    public static int alive()
    {
        var alive = alivePlayers.Where(x => x.Value.isalive == true).Count();
        return alive;
    }
    //1 = Red team; 2 = yellow team
    public static string switchCamera()
    {
        string netID = "";

        try
        {
            var Allplayers = alivePlayers.Where(x => x.Value.isalive == true).ToArray();
            var index  = alivePlayers.Where(x => x.Value.isalive == true).Count();
            if (indexVal >= (index - 1))
            {
                indexVal = -1;
            }
            indexVal++;
            Debug.Log("indexsskey:" + index);
            Debug.Log(Allplayers[indexVal].Key);
            netID = Allplayers[indexVal].Key;
        }
        catch (Exception e)
        {
           
        }





        return netID;

    }

    //1 = Red team; 2 = yellow team
    public static void PlayerJoins(Health player, int Team, string lobbyNetworkID, string netID )
    {
        alivePlayers.Add(lobbyNetworkID, player);

        if (Team == 2)
        {
            PlayersYellow.Add(lobbyNetworkID, player);

        }
        else
        {
            PlayersRed.Add(lobbyNetworkID, player);

        }
    }

    public static void scorered()
    {
        redpoints += 1;
    }

    public static void scoreyellow()
    {
        yellowpoints += 1;
    }

    public static bool AllCheck()
    {
        if (alivePlayers.Count == 0)
        {
            return true;
        }
        return false;
    }

    public static bool isheltal()
    {
        if (alivePlayers.Count % 2 == 0) //om det är ett heltal
        {
            return true;
        }
        return false;
    }

    public static string getteamscore()
    {
        return "Yellow: "+yellowpoints.ToString() + " / Red: "+ redpoints.ToString();
    }


    //no references
    public static Health getplayerby_NETID(string NetID)
    {
        return alivePlayers[NetID];
    }
    // Update is called once per frame
    void Update () {
		
	}
}

