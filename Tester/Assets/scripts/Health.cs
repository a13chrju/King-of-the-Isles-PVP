using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Health : NetworkBehaviour {

    // Use this for initialization
    public Text canvas_txt;
    public GameObject canvas_txt_death;
    public GameObject canvas_txt_death_child;
    public GameObject canvaschild;
    public GameObject deathCam;
    

    [SyncVar]
    public bool isalive = true;
    public float lasttime = 0;
    public float respawntime = 5f;

    [SerializeField]
    private Behaviour[] disableonDeath;

    private string spectatingplaer = "";

    private bool runonce = true;
    void Start () {
        
            canvas_txt = GameObject.FindGameObjectWithTag("awesome").GetComponentInChildren<Text>();

        canvas_txt_death = GameObject.FindGameObjectWithTag("Battletext");
        deathCam = GameObject.FindGameObjectWithTag("deathcam");
        deathCam.GetComponentInChildren<Camera>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (isalive == false && isLocalPlayer && Time.time < lasttime)
        {
            canvas_txt.text = "You Died. "+ Mathf.Round(lasttime - Time.time).ToString();
        }
        if (isalive == false && isLocalPlayer && Time.time >= lasttime && runonce == true)
        {
            deathCam.GetComponentInChildren<Camera>().enabled = false;
            CmdCameraSwitch();
            canvas_txt.text = "Spectating"; runonce = false;
        }
    }
    public void Deathchain()
    {
        if (isLocalPlayer)
        {
            lasttime = Time.time + respawntime;
            CmdDie();
        }
        var name = this.transform.name;

        if (spectatingplaer == name)
        {
            runonce = true;
        }
        TeamManager.PlayerRemove(name);
       
    }


    public void CmdCameraSwitch()
    {
        var player = TeamManager.switchCamera(1);
        spectatingplaer = player;
        Debug.Log("TESTTT" + player);
        GameObject temp2 = (GameObject)GameObject.Find(player);
        temp2.GetComponentInChildren<Camera>().enabled = true;
    }


    [Command]
    public void CmdDie()
    {
        //Apply it to all other clients
        Rpcdie();
        CmdAddCanvasChild();
       
    }

    [ClientRpc]
    public void Rpcdie()
    {
        var team = GetComponent<setup>().Team; //0 = red, 1 = yellow
        Debug.Log(team);
       // this.GetComponent<capture>().removelighteffect();
       // this.GetComponent<capture>().hideflag();
        // remove score-lightning effect

        isalive = false;
        foreach (var obj in disableonDeath) //disable colliders
        {
            obj.enabled = false;

        }
        if (isLocalPlayer)
        {
            Image fsa =  GameObject.FindGameObjectWithTag("awesome").GetComponentInChildren<Image>();
            fsa.GetComponent<CanvasGroup>().alpha = 0;
            this.GetComponentInChildren<Camera>().enabled = false; lasttime = Time.time + respawntime;
            deathCam.GetComponentInChildren<Camera>().enabled = true;
        }
       

       // Debug.Log(transform.name + "DIED!");


        //StartCoroutine(Respawn());

    }

    [Command]
    public void CmdAddCanvasChild()
    {
        // GameObject fireball = (GameObject)Instantiate(fireball_prefab, posfire, this.GetComponentInChildren<Camera>().transform.rotation) as GameObject;
        canvaschild = (GameObject)Instantiate(canvas_txt_death_child, this.transform.position, canvas_txt_death_child.transform.rotation) as GameObject;

        Debug.Log("hhej" + canvas_txt_death);

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
        
    }


    [ClientRpc]
    public void RpcSetDefaults()
    {
       
        isalive = true;

        if (isLocalPlayer)
        {
            deathCam.GetComponentInChildren<Camera>().enabled = false;
            this.GetComponentInChildren<Camera>().enabled = true;
        }

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
