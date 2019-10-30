using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TeamManager : MonoBehaviour
{
    public static Dictionary<string, PlayerHealth> alivePlayers = new Dictionary<string, PlayerHealth>();
    public static Dictionary<string, PlayerHealth> PlayersBlue = new Dictionary<string, PlayerHealth>();
    public static Dictionary<string, PlayerHealth> PlayersRed = new Dictionary<string, PlayerHealth>();
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
    public static string dead ="";
    public enum Team { Red = 1  , Blue = 2};

    public static Camera localCamera;
    // Use this for initialization
    void Start () {
     
    }

    public static void setCamera(Camera inputcamera)
    {
        localCamera = inputcamera;
    }

    public static void deadplayer(string name)
    {
        dead = name;
    }


    public static string AllPlayerLength()
    {
        return "ALL: " +alivePlayers.Count.ToString() + ";RED: " + PlayersRed.Count.ToString()+ "RED: " + PlayersBlue.Count.ToString();

       
    }

    public static void startGame()
    {
        GameObject CanvasBox = (GameObject)GameObject.FindGameObjectWithTag("opening");
        CanvasBox.GetComponent<Animator>().SetBool("run", true);
      
    }


    public static string switchCamera(int Team)
    {
        var random = Random.Range(0, PlayersBlue.Count);
        List<string> keyList; List<PlayerHealth> healths;
        var guy = "";
    
        if (Team == 1)
        {
            keyList = new List<string>(PlayersBlue.Keys);
            healths = new List<PlayerHealth>(PlayersBlue.Values);
            Debug.Log(keyList.Count);
            guy = keyList[random];
        }
        else
        {
            keyList = new List<string>(PlayersRed.Keys);
            guy = keyList[random];
        }
       
     
       // Debug.Log(guy);

     /*   GameObject temp2 = (GameObject)GameObject.Find(guy);
        temp2.GetComponentInChildren<Camera>().enabled = true;
        Debug.Log(temp2.tag);
        */
        return guy;

    }

    public static void PlayerRemove(string netID)
    {
        PlayersBlue.Remove(netID);
        AllPlayerLength();
    }

    public static void PlayerJoins(PlayerHealth player, string netID, int Team)
    {
        alivePlayers.Add(netID, player);

        if (Team == 1)
        {
            PlayersBlue.Add(netID, player);

        }
        else
        {
            PlayersRed.Add(netID, player);

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

    public static PlayerHealth getplayerby_NETID(string NetID)
    {
        return alivePlayers[NetID];
    }
    // Update is called once per frame
    void Update () {
		
	}
}

