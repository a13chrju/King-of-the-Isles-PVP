using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class fireballs : NetworkBehaviour {

    // Use this for initialization
    public GameObject fireball_prefab;
    public Transform transforms;
    public Animator anim;
    public float shotdelay, fireballspeed;

    [SyncVar]
    public float cooldown;

    [SyncVar]
    private bool active = false;

    public bool canshoot = false;
    public Rigidbody rb;
    public GameObject aim_reference;
    public AudioClip FireballSound;
    private AudioSource source;
    private Camera myCamera;
    public bool isgliding = false;
    //non local
    public Image fire_icon;

    //local
    public Image localfire_icon;

    public float cooltime = 2f, lifetime;
    public GameObject fireball;
    [SyncVar]
    public bool canfire = true;

    void Start () {
        lifetime = Time.time + 12f;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //fireball_prefab = GameObject.FindGameObjectWithTag("bullet");
        localfire_icon = GameObject.FindGameObjectWithTag("myuihehe").GetComponentInChildren<Image>();
        source = GetComponent<AudioSource>();
        myCamera = GetComponentInChildren<Camera>();
     
        if (isLocalPlayer)
        {
            this.GetComponentInChildren<CanvasGroup>().alpha = 0;
        }
    }
    void OnEnable()
    {
        fire_icon.fillAmount = 0;
    }

    // Update is called once per frame
    void LateUpdate () {

       /* if (Time.time > lifetime)
        {
            if (isServer)
            {
                NetworkServer.Destroy(this.gameObject);
                return;
            }
        }*/
        /* if (fire_icon.fillAmount > 0.99)
         {
             fire_icon.fillAmount = 0;

             if (isLocalPlayer)
             {
                 Cmdresetnio();
             }
             else
             {
                 canfire = true;
             }

         }*/
        if (isLocalPlayer)
        {
            
            if (Time.time < cooldown && canfire == false)
            {
                localfire_icon.fillAmount += 1 / 2f * Time.deltaTime;
            }
            else if (canfire == false && Time.time > cooldown)
            {


                if (isLocalPlayer)
                {
                   
                    Cmdresetnio();
                    localfire_icon.fillAmount = 0;
                }
              
            }

        }
        else
        {
            if (canfire == false)
            {
                fire_icon.fillAmount += 1 / 2f * Time.deltaTime;
            }
            else
            {
                fire_icon.fillAmount = 0;
            }
        }


        if (isLocalPlayer)
        {
            //CmdCool();



            if (Input.GetKeyDown(KeyCode.Mouse0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("fireball") && canfire == true)
            {
                source.PlayOneShot(FireballSound, 1f);
                cooldown = Time.time + 2f;
               
                Cmdshot(myCamera.transform.rotation);
                Cmdshotanim();
                CmdCantfire(2);
                RpcShoot();


            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                this.gameObject.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.forward * 100, ForceMode.Impulse);
                this.isgliding = true;

            }

            if (isgliding == true)
            {
                if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
                {
                    this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    isgliding = false;
                }
            }
        }
    }

    [Command]
    public void CmdCantfire(int timeer)
    {
        Rpcfdsfdsfsd(timeer);
    }

    [ClientRpc]
    public void Rpcfdsfdsfsd(int timeer)
    {
        //cooldown = Time.time + timeer;
        canfire = false;
    }

    [Command]
    public void Cmdresetnio()
    {
        RpcShootds();
    }
    [ClientRpc]
    public void RpcShootds()
    {
        canfire = true;

    }

    [Command]
    public void Cmdshot(Quaternion rotation)
    {
        Vector3 playerpos = transform.position;
        playerpos.y = playerpos.y + 4f;
      
        Vector3 playerDir2 = this.transform.forward;
        Quaternion playerRot = transform.transform.rotation;
        Vector3 posfire = playerpos + playerDir2 * 10;





        // GameObject fireball = (GameObject)Instantiate(fireball_prefab, posfire, this.GetComponentInChildren<Camera>().transform.rotation) as GameObject;
        fireball = (GameObject)Instantiate(fireball_prefab, posfire, rotation) as GameObject;

        //fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * fireballspeed;
        fireball.GetComponent<bullet>().direction = rotation;

       
        NetworkServer.Spawn(fireball);
    }
    [ClientRpc]
    public void RpcShoot()
    {
        fireball.transform.rotation = myCamera.transform.rotation;
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
        anim.SetTrigger("fireball");

    }


}
