using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class capture : NetworkBehaviour {

    // Use this for initialization

    [SyncVar]
    public bool gotflag = false;

    public float time;
    public Image capcan;
    public GameObject teamscore;
    public bool nearflag = false;
    public GameObject chest;
    public GameObject flagmodel_red;
    public GameObject flagmodel_yellow;
    public GameObject canscore_particles;
    public GameObject hand;
    public bool isatteambase = true;
    public string allscore ="0 / 0";

    public AudioClip flagpickupSound;
    public AudioClip ScoringSound;
    private AudioSource source;
    public GameObject canvas_txt_death;
    public GameObject canvas_txt_death_child, canvas_txt_death_childchest, canvas_txt_score_child;
    public GameObject canvaschild;

    [SyncVar]
    public bool canscore = false;
   
  
    void Start()
    {
        StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine()
    {
        yield return new WaitForEndOfFrame();

        // Do your code here to assign game objects
        capcan = GameObject.FindGameObjectWithTag("capcan").GetComponentInChildren<Image>();
        teamscore = GameObject.FindGameObjectWithTag("teamscore");
        chest = GameObject.FindGameObjectWithTag("chest");
        canvas_txt_death = GameObject.FindGameObjectWithTag("Battletext");
        source = GetComponent<AudioSource>();
        Debug.Log("Katter");
    }

    // Update is called once per frame
    void Update () {
        if (isLocalPlayer)
        {
            if (Time.time < this.time && nearflag == true && isatteambase == true)
            {
               
                if (capcan.fillAmount == 1)
                {
                    Cmdcarriestheflaggotit();
                    if (this.GetComponent<setup>().Team == 1)
                    {
                        //TeamManager.redflagup = true;
                        Cmdpickedflagup(1);
                        //teamscore.GetComponent<teamscore>().flagpickedup(1);
                    }
                    else
                    {
                        //TeamManager.yellowflagup = true;
                        Cmdpickedflagup(2);
                       // teamscore.GetComponent<teamscore>().flagpickedup(2);
                    }
                    source.PlayOneShot(flagpickupSound, 0.5f);
                    capcan.fillAmount = 0;
                    Cmdshowflag();
                    CmdAddCanvasChild();
                }
                if ((this.GetComponent<setup>().Team == 1 && teamscore.GetComponent<teamscore>().redflagpicked == false))
                {
                    capcan.fillAmount += 0.3f * Time.deltaTime;
                }else if (this.GetComponent<setup>().Team == 2 && teamscore.GetComponent<teamscore>().yelflagpicked == false)
                {
                    capcan.fillAmount += 0.3f * Time.deltaTime;
                }
            }
            else if (Time.time < this.time && nearflag == true && isatteambase == false && gotflag == true)
            {
                capcan.fillAmount += 0.3f * Time.deltaTime;
                if (capcan.fillAmount == 1)
                {
                    CmdScorePart(1);
                    this.gameObject.GetComponent<fireballs>().enabled = false;
                    chest.GetComponent<pickupchest>().Open();
                    CmdCanScoreYes();
                    CmdAddCanvasChildForChest();
                    capcan.fillAmount = 0;
                    isatteambase = true;
                    //maybe show particle effect?
                }
            }
        }
        
	}

   /* public void attachflag()
    {
        Vector3 playerpos = transform.position;
        Vector3 playerDir = transform.transform.forward;
       
        Vector3 posfire = playerpos + playerDir * -2;
        Quaternion fisk = flagmodel.transform.rotation;
        GameObject flag = (GameObject)Instantiate(flagmodel, transform.position, flagmodel.transform.rotation) as GameObject;
        
        NetworkServer.Spawn(flag);

       // flag.transform.position = hand.transform.position;
        flag.transform.parent = hand.transform;
        flag.transform.Rotate(new Vector3(0, 270f,0));
    }*/

    public void captureflag(int team, bool isatbase)
    {
     
            if (isatbase == false)
            {
                nearflag = true;
                capcan.enabled = true;
                this.time = Time.time + 5f;
                isatteambase = false;
            }
            else
            {
                isatteambase = true;

                if (canscore == true && isatteambase == true && isServer) // SCORING!!! :D 
                {
                    source.PlayOneShot(ScoringSound, 1f);

                    if (team == 1 && teamscore.GetComponent<teamscore>().redflagpicked == true)
                    {
                        TeamManager.scorered();
                        CmdTSincrease(1);
                        CmdScorePart(2);
                        

                    }
                    else if (team == 2 && teamscore.GetComponent<teamscore>().yelflagpicked == true)
                    {
                        TeamManager.scoreyellow();
                        CmdTSincrease(2);
                        CmdScorePart(2);
                    }
                    RpcEnableFire();
                    Cmdgetteammanagerscore();
                    hideflag();
                    teamscore.GetComponent<teamscore>().updateui();
                    CmdAddCanvasChildscore();


                }
                else
                {
                    if (team == 1 && teamscore.GetComponent<teamscore>().redflagpicked == false)
                    {
                        nearflag = true;
                        capcan.enabled = true;
                        this.time = Time.time + 5f;
                    }
                    else if (team == 2 && teamscore.GetComponent<teamscore>().yelflagpicked == false)
                    {
                        nearflag = true;
                        capcan.enabled = true;
                        this.time = Time.time + 5f;
                    }
                }


            }
        
       

       
    }


    [Command]
    public void Cmdpickedflagup(int team)
    {
        if (team == 1)
        {
         
            teamscore.GetComponent<teamscore>().captureConverter(1);
        }
        else
        {
           
            teamscore.GetComponent<teamscore>().captureConverter(2);
        }

    }



    [Command]
    public void CmdTSincrease(int team)
    {
        if (team == 1)
        {
          
            teamscore.GetComponent<teamscore>().TeamScorered++;
        }
        else
        {
           
            teamscore.GetComponent<teamscore>().TeamScoreyell++;
        }

    }


    [Command]
    public void Cmdgetteammanagerscore()
    {
        allscore = TeamManager.getteamscore();

    }
    public void nocaptureflag(int team)
    {
        nearflag = false;
        capcan.fillAmount = 0;
        capcan.enabled = false;
    }

    /* START LOGGER */

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
            block.GetComponentInChildren<Text>().text = this.GetComponent<setup>().myname.ToString() + " Picked up the flag";
            block.transform.parent = GameObject.FindGameObjectWithTag("Battletext").transform;
        }




        [Command]
        public void CmdAddCanvasChildscore()
        {
            // GameObject fireball = (GameObject)Instantiate(fireball_prefab, posfire, this.GetComponentInChildren<Camera>().transform.rotation) as GameObject;
            canvaschild = (GameObject)Instantiate(canvas_txt_score_child, this.transform.position, canvas_txt_death_child.transform.rotation) as GameObject;

            Debug.Log("hhej" + canvas_txt_death);

            NetworkServer.Spawn(canvaschild);
            RpcSyncBlockOncescore(canvaschild, canvas_txt_death.transform.parent.gameObject);
            //   canvaschilds.GetComponent<changemytext>().canvas_txt_death = canvas_txt_death;
            // canvaschilds.GetComponent<changemytext>().changetext(this.gameObject.transform.name.ToString() + " DIED");
        }

        [ClientRpc]
        public void RpcSyncBlockOncescore(GameObject block, GameObject parent)
        {
            block.GetComponentInChildren<Text>().text = this.GetComponent<setup>().myname.ToString() + " Scored! YEY";
            block.transform.parent = GameObject.FindGameObjectWithTag("Battletext").transform;
        }


    // chest logger

    [Command]
        public void CmdAddCanvasChildForChest()
        {
            // GameObject fireball = (GameObject)Instantiate(fireball_prefab, posfire, this.GetComponentInChildren<Camera>().transform.rotation) as GameObject;
            canvaschild = (GameObject)Instantiate(canvas_txt_death_childchest, this.transform.position, canvas_txt_death_child.transform.rotation) as GameObject;

            Debug.Log("hhej" + canvas_txt_death);

            NetworkServer.Spawn(canvaschild);
            RpcSyncBlockOncechest(canvaschild, canvas_txt_death.transform.parent.gameObject);
            //   canvaschilds.GetComponent<changemytext>().canvas_txt_death = canvas_txt_death;
            // canvaschilds.GetComponent<changemytext>().changetext(this.gameObject.transform.name.ToString() + " DIED");
        }

        [ClientRpc]
        public void RpcSyncBlockOncechest(GameObject block, GameObject parent)
        {
            block.GetComponentInChildren<Text>().text = this.GetComponent<setup>().myname.ToString() + " buffed the flag!";
            block.transform.parent = GameObject.FindGameObjectWithTag("Battletext").transform;
        }

    /* END  LOGGER */

    [Command]
    public void Cmdshowflag()
    {
        //Apply it to all other clients
        if (this.GetComponent<setup>().Team == 1) // red team
        {
            Rpcredshowflag();
        }
        else
        {
            Rpcshowyellflag();
        }
       
    }
    [ClientRpc]
    public void Rpcredshowflag() 
    {
        flagmodel_red.SetActive(true);

    }

    [ClientRpc]
    public void Rpcshowyellflag()
    {
        flagmodel_yellow.SetActive(true);

    }
    public void hideflag()
    {
        var team = GetComponent<setup>().Team; //0 = red, 1 = yellow
        Debug.Log(team);
        if (team == 1)
        {
            if (gotflag == true)
            {

               // TeamManager.redflagup = false;
                teamscore.GetComponent<teamscore>().downflagpickedup(1);
                gotflag = false;
                hidetheflag();
            }
        }
        else if (team == 2)
        {
            if (gotflag == true)
            {

               // TeamManager.yellowflagup = false;
                teamscore.GetComponent<teamscore>().downflagpickedup(2);
                this.GetComponent<capture>().gotflag = false;
                this.GetComponent<capture>().hideflag();
                hidetheflag();
            }

        }

        CmdCanScoreNo();

    }

    public void hidetheflag()
    {
        Cmdhideflag();
    }

    [Command]
    public void Cmdhideflag()
    {  //Apply it to all other clients
        if (this.GetComponent<setup>().Team == 1) // red team
        {
            Rpchideredflag();
        }
        else
        {
            Rpchideyellflag();
        }  
    }

    [ClientRpc]
    public void Rpchideredflag()
    {
        flagmodel_red.SetActive(false);

    }

    [ClientRpc]
    public void Rpchideyellflag()
    {
        flagmodel_yellow.SetActive(false);

    }

    [Command]
    public void CmdCanScoreYes()
    {  //Apply it to all other clients
        canscore = true;
    }

    [Command]
    public void CmdCanScoreNo()
    {  //Apply it to all other clients
        canscore = false;
    }


    [Command]
    public void Cmdcarriestheflaggotit()
    {  //Apply it to all other clients
        gotflag = true;
    }

    public void removelighteffect()
    {
        
        Cmdremovelight();
    }

    [Command]
    public void Cmdremovelight()
    {  //Apply it to all other clients


        if (canscore == true)
        {
            RpchideSCOREPARTICLE();
        }
    }

    [Command]
    public void CmdScorePart(int shwat)
    {  //Apply it to all other clients
        if (shwat == 1) // show
        {
            RpcShowSCOREPARTICLE();
        }
        else
        {
            RpchideSCOREPARTICLE();
        }
    }


    [ClientRpc]
    public void RpchideSCOREPARTICLE()
    {
        canscore_particles.SetActive(false);

    }
    [ClientRpc]
    public void RpcShowSCOREPARTICLE()
    {
        canscore_particles.SetActive(true);

    }





    [ClientRpc]
    public void RpcEnableFire()
    {
        this.gameObject.GetComponent<fireballs>().enabled = true;

    }

}
