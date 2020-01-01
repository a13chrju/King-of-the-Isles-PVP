using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SpeedBoost : NetworkBehaviour {
    public bool isgliding = false;
    public Image SpeedBoost_icon;
    public float cooltime = 10f;
    public GameObject Particles_prefab;
    public AudioClip SpeedBoostSound;
    private AudioSource source;
    public Animator anim;

    [SyncVar]
    public float cooldown;

    [SyncVar]
    public bool canfire = true;

    private Camera myCamera;
    private float ZoomTime;
    private bool zoomCamera = false;
    // Use this for initialization
    void Start () {
        if (isLocalPlayer)
        {
            anim = GetComponent<Animator>();
            source = GetComponent<AudioSource>();
            myCamera = this.GetComponentInChildren<Camera>();
        }
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        // GameObject.Find("klickme").GetComponent<Button>().onClick.AddListener(() => CmdFireBallShot()); for mobile

        SpeedBoost_icon = GameObject.Find("SpeedBoost").GetComponent<Image>();
    }
    // Update is called once per frame
    void Update() {

        if (isgliding == true)
        {
            if (Input.GetKey(KeyCode.S))
            {
                this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                isgliding = false;
            }
        }
      
    }

        void LateUpdate()
        {

            if (isLocalPlayer)
            {
                if (zoomCamera && Time.time < ZoomTime)
                {
                    
                }

                if (Time.time < cooldown && canfire == false)
                {
                    SpeedBoost_icon.fillAmount += 1 / cooltime * Time.deltaTime;
                    
                }
                else if (canfire == false && Time.time > cooldown)
                {
                    Cmdresetnio();
                    SpeedBoost_icon.fillAmount = 0;
                }
            }

        if (isLocalPlayer)
        {
            //CmdCool();

            if (Input.GetKeyDown(KeyCode.C) && canfire == true)
            {
                source.PlayOneShot(SpeedBoostSound, 0.3f);
                this.gameObject.GetComponent<Rigidbody>().AddForce(this.gameObject.GetComponentInChildren<Camera>().transform.forward * 200, ForceMode.Impulse);
                this.isgliding = true;
                Debug.Log("SHOT");
                cooldown = Time.time + cooltime;
                ZoomTime = Time.time + 2;
                Cmdshot();
                CmdCantfire();
                zoomCamera = true;

            }


        }

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
    public void CmdCantfire()
    {
        RpcCantFire();
    }

    [ClientRpc]
    public void RpcCantFire()
    {
        //cooldown = Time.time + timeer;
        canfire = false;
    }

    [Command]
    public void Cmdshot()
    {
        Vector3 playerpos = transform.position;
        // playerpos.y = playerpos.y + 4f;
        // GameObject fireball = (GameObject)Instantiate(fireball_prefab, posfire, this.GetComponentInChildren<Camera>().transform.rotation) as GameObject;
        Particles_prefab = (GameObject)Instantiate(Particles_prefab, playerpos, transform.rotation) as GameObject;
        NetworkServer.Spawn(Particles_prefab);
    }

}
