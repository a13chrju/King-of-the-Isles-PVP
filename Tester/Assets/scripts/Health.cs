using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Health : NetworkBehaviour {

    // Use this for initialization
    public Text canvas_txt;
    public GameObject canvas_txt_death;
    public GameObject canvas_txt_death_child, canvas_txt_leave_child;
    public GameObject canvaschild;
    public GameObject deathCam;
    

    [SyncVar]
    public bool isalive = true;
    public float lasttime = 0;
    public float respawntime = 5f;
    [SyncVar]
    public int lastplayer = 0;

    [SyncVar]
    public int Lives = 3;

    public bool GameNotOver = true;

    [SerializeField]
    private Behaviour[] disableonDeath;

    private string spectatingplaer = "";
    public bool runonceWin = true;
    private bool runonce = true;
    public bool canwin;

    void Start () {
        
        canvas_txt = GameObject.FindGameObjectWithTag("awesome").GetComponentInChildren<Text>();


        deathCam = GameObject.FindGameObjectWithTag("deathcam");
        Debug.Log(deathCam);
        deathCam.GetComponentInChildren<Camera>().enabled = false;
        if (isLocalPlayer && canwin)
        {
            StartCoroutine(checkforwinner());
        }
    }

 
    public struct Winner
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




    public void Deathchain()
    {
        if (isLocalPlayer)
        {
            lasttime = Time.time + respawntime;
            CmdDie(Lives);

        }
    }
     private IEnumerator checkforwinner()
     {
        while (GameNotOver)
        {
            yield return new WaitForSeconds(2f);
           
                int ALiveCount = TeamManager.GetAllPlayerLength();

                Debug.Log(ALiveCount + "YE");
                if (ALiveCount <= 1 && this.Lives > 0)
                {
                    GameNotOver = false;
                    Debug.Log("YOU WIN!");
                    TeamManager.youwin();
                }
            
        }
     
       
     }
 
  /*  [Command]
    public void CmdWinnerLast()
    {
        var countAlive = TeamManager.GetAllPlayerLength();
        string netID = TeamManager.GetWinner();
        Debug.Log("OKEJ" + netID);
        if (countAlive <= 1)
        {
            RpcGameEnd(netID);
        }

    }


    [ClientRpc]
    public void RpcGameEnd(string netID)
    {
        Debug.Log(netID + " AND " + this.transform.name);
        if (this.isLocalPlayer && this.transform.name == netID)
        {
            TeamManager.youwin();
        }
    }*/

    [Command]
    public void CmdDie(int lives)
    {
        //Apply it to all other clients
        if (this.isalive)
        {
            CmdAddCanvasChild(true);
        }
        Rpcdie(lives);
        this.isalive = false;
       

    }

    [ClientRpc]
    public void Rpcdie(int lives)
    {
     

        if (lives <= 1)
        {
            this.Lives = 0;
            isalive = false;
            foreach (var obj in disableonDeath) //disable colliders
            {
                obj.enabled = false;

            }
            if (isLocalPlayer)
            {
                Image fsa = GameObject.FindGameObjectWithTag("awesome").GetComponentInChildren<Image>();
                canvas_txt.text = "Spectating\nLeft Click to Switch";
                fsa.GetComponent<CanvasGroup>().alpha = 0;
                this.GetComponentInChildren<Camera>().enabled = false; lasttime = Time.time + respawntime;
                deathCam.GetComponentInChildren<Camera>().enabled = true;

                if (GameNotOver)
                {
                    TeamManager.youlose();
                }
            
            }

        }
        else
        {
            if (isLocalPlayer)
            {
                deathCam = GameObject.FindGameObjectWithTag("deathcam");
                canvas_txt.text = "You have " + (lives - 1) + " more lives.";
                this.GetComponentInChildren<Camera>().enabled = false;
                deathCam.GetComponentInChildren<Camera>().enabled = true;
            }
        
            this.Lives = lives - 1;

            StartCoroutine(Respawn());
        }


        // this.GetComponent<capture>().removelighteffect();
        // this.GetComponent<capture>().hideflag();
        // remove score-lightning effect



        // Debug.Log(transform.name + "DIED!");


        // StartCoroutine(Respawn());





    }

    [Command]
    public void Cmdchangewinn(int value)
    {
        RpcSetWinnerBool(value);
    }
    [ClientRpc]
    public void RpcSetWinnerBool(int value)
    {
        this.lastplayer = value;
    }

    [Command]
    public void CmdAddCanvasChild(bool isdeathicon)
    {
        
        canvas_txt_death = GameObject.FindGameObjectWithTag("Battletext");
        Debug.Log(canvas_txt_death.transform.parent.gameObject);
        // GameObject fireball = (GameObject)Instantiate(fireball_prefab, posfire, this.GetComponentInChildren<Camera>().transform.rotation) as GameObject;
        if (isdeathicon)
        {
            canvaschild = (GameObject)Instantiate(canvas_txt_death_child, this.transform.position, canvas_txt_death_child.transform.rotation) as GameObject;
        }

      //  Debug.Log("hhej" + canvas_txt_death);

        NetworkServer.Spawn(canvaschild);
        RpcSyncBlockOnce(canvaschild, canvas_txt_death.transform.parent.gameObject);
        //   canvaschilds.GetComponent<changemytext>().canvas_txt_death = canvas_txt_death;
        // canvaschilds.GetComponent<changemytext>().changetext(this.gameObject.transform.name.ToString() + " DIED");
    }

    [ClientRpc]
    public void RpcSyncBlockOnce(GameObject block, GameObject parent)
    {
        block.GetComponentInChildren<Text>().text = this.GetComponent<setup>().myname.ToString() + " DIED";
        block.transform.parent = GameObject.FindGameObjectWithTag("Battletext").transform;
    }

    private IEnumerator Respawn()
    {
       
        yield return new WaitForSeconds(respawntime);
           canvas_txt.text = "";
           RpcSetDefaults();

           Transform _startpoint = NetworkManager.singleton.GetStartPosition();

           transform.position = _startpoint.position;
           transform.rotation = _startpoint.rotation;
           this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
           this.gameObject.GetComponent<move>().resetvelo();

        if (isLocalPlayer)
        {
            deathCam.GetComponentInChildren<Camera>().enabled = false;
            this.GetComponentInChildren<Camera>().enabled = true;
        }
        /*  if (isLocalPlayer)
          {
              deathCam.GetComponentInChildren<Camera>().enabled = false;
          }*/
        // CmdDestroyButton(this.gameObject);

    }

    [Command]
    public void CmdDestroyButton(GameObject button)
    {
        NetworkServer.Destroy(button);
    }


    [ClientRpc]
    public void RpcSetDefaults()
    {
       
        isalive = true;

        foreach (var obj in disableonDeath)
        {
            obj.enabled = true;
        }

        this.GetComponent<fireballs>().canfire = true;
      //  canvaschild.GetComponent<Text>().text = this.gameObject.transform.name + " DIED";


       // StartCoroutine(hideCanvasTxt());
      //  Debug.Log(transform.name + "RESPAWNED!");


    }


    private IEnumerator hideCanvasTxt()
    {

        yield return new WaitForSeconds(3f);
        NetworkServer.Destroy(canvaschild);
        
    }
}
