using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class builder : NetworkBehaviour {

    public GameObject placehodler;
    public GameObject wall;
    public Camera camera;
    public AudioClip ForceFieldSound;
    private AudioSource source;
    public float builderTime;

    [SyncVar]
    public float cooldown;

    public Animator Animator;
    public bool switcher = false, canbuild = true;
    //local
    public Image builder_icon;
    // Use this for initialization
    void Start () {
        Animator = GetComponent<Animator>();
        if (isLocalPlayer)
        {
            camera = this.GetComponentInChildren<Camera>();
            cooldown = Time.time;
            source = GetComponent<AudioSource>();

        }
        else
        {
            return;
        }
        // 

    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        //GameObject.Find("buildlerUI").GetComponent<Button>().onClick.AddListener(() => CmdBuilderBuild());
        builder_icon = GameObject.Find("builder_icon").GetComponent<Image>();
    }
  /*  [Command]
    public void CmdBuilderBuild()
    {
        RpcBuildBoi();

    }

    //Client Side.   
    [ClientRpc]
    void RpcBuildBoi()
    {
        Debug.Log("wall!");
        if (isLocalPlayer && canbuild == true)
        {
            source.PlayOneShot(ForceFieldSound, 1f);
            Cmdshotanim();
            cooldown = Time.time + builderTime;
            Cmdbuild();
            Cmdreset(2);
        }
    }*/
    // Update is called once per frame
    void Update () {
 
          if (isLocalPlayer)
          {

              if (Time.time < cooldown && canbuild == false && builder_icon.fillAmount < 1)
              {
               /// 2f * Time.deltaTime;
                    builder_icon.fillAmount += 1 / builderTime * Time.deltaTime;
                }
              else if (canbuild == false && Time.time > cooldown)
              {

                  if (isLocalPlayer)
                  {

                      Cmdresetnio();
                     builder_icon.fillAmount = 0;
                  }

              }

          }
         /* else
          {
              if (canbuild == false)
              {
                  builder_icon.fillAmount += 1 / 2f * Time.deltaTime;
              }
              else
              {
                  builder_icon.fillAmount = 0;
              }
          }*/
          
        
        if (isLocalPlayer)
        {

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
               
                    if (canbuild == true)
                    {
                        source.PlayOneShot(ForceFieldSound, 0.5f);
                        Cmdshotanim();
                        cooldown = Time.time + builderTime;
                        Cmdbuild();
                        Cmdreset(2);
                    }


            }

        
        }
        
    }


    [Command]
    public void Cmdreset(int timeer)
    {
        Rpcfdsfdsfsd(timeer);
    }

    [ClientRpc]
    public void Rpcfdsfdsfsd(int timeer)
    {
        //cooldown = Time.time + timeer;
        canbuild = false;
    }

    [Command]
    public void Cmdresetnio()
    {
        RpcShootds();
    }
    [ClientRpc]
    public void RpcShootds()
    {
        canbuild = true;

    }
    [Command]
    public void Cmdshotanim()
    {
        //Apply it to all other clients
        RpcShootanim();
    }
    [ClientRpc]
    public void RpcShootanim()
    {
        Animator.SetTrigger("fireball");

    }


    [Command]
    public void Cmdbuild()
    {
        //Apply it to all other clients
       // RpcBuildwall();
        Vector3 playerpos = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
        Vector3 playerDir = transform.transform.forward;
        Quaternion playerRot = this.transform.rotation; playerRot.x = -90f;

        //transform.rotation = Quaternion.LookRotation(camera.velocity);
        Vector3 v = transform.rotation.eulerAngles;
        Quaternion dsaotation = Quaternion.Euler(0, v.y - 20, v.z);

        Quaternion dsa = Quaternion.Euler(playerRot.x, playerRot.y, playerRot.z);

        Vector3 posfire = playerpos + playerDir * 10;

        // GameObject fireball = (GameObject)Instantiate(fireball_prefab, posfire, this.GetComponentInChildren<Camera>().transform.rotation) as GameObject;
        GameObject Dwall = (GameObject)Instantiate(wall, posfire, dsaotation) as GameObject;
        NetworkServer.SpawnWithClientAuthority(Dwall, this.gameObject);
        


    }
    [ClientRpc]
    public void RpcBuildwall()
    {
      

    }
}
