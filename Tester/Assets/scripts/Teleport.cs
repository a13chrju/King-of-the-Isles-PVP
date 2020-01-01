using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class Teleport : NetworkBehaviour {
    public bool isgliding = false;
    public Rigidbody rb;
    public Image Tele_icon;
    public float cooltime = 10f;
    public GameObject PlaceholderModel_prefab, Teleparticles_prefab;
    private Vector3 PlaceHolderTransform;
    private AudioSource source;
    public AudioClip TeleSound;
    public Animator anim;
    [SyncVar]
    public float cooldown;

    [SyncVar]
    public bool canfire = true;
    public bool canTeleport = false;
    // Use this for initialization
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        // GameObject.Find("klickme").GetComponent<Button>().onClick.AddListener(() => CmdFireBallShot()); for mobile

        Tele_icon = GameObject.Find("telep").GetComponent<Image>();
    }

    void Start () {
        
            rb = this.GetComponent<Rigidbody>();
            source = GetComponent<AudioSource>();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {

        if (isLocalPlayer)
        {
            if (Time.time < cooldown && canfire == false)
            {
                Tele_icon.fillAmount += 1 / cooltime * Time.deltaTime;
            }
            else if (canfire == false && Time.time > cooldown)
            {
                Cmdresetnio();
                canTeleport = false;
                Tele_icon.fillAmount = 0;
            }
        }

        if (isLocalPlayer)
        {
            //CmdCool();
            if (Input.GetKeyDown(KeyCode.E) && canTeleport == true)
            {
                CmdSpawnTeleparticles();
                source.PlayOneShot(TeleSound, 0.4f);
                Debug.Log("weeeeeeeeeow");
                // source.PlayOneShot(SpeedBoostSound, 0.3f);
                canTeleport = false;
                CmdMoveMe(PlaceHolderTransform);
            }
            if (Input.GetKeyDown(KeyCode.E) && canfire == true)
            {
                // source.PlayOneShot(SpeedBoostSound, 0.3f);
                canTeleport = true;
                this.isgliding = true;
                PlaceHolderTransform = rb.position;
                Debug.Log("SHOT");
                cooldown = Time.time + cooltime;
                Cmdshot(transform.position);
                CmdCantfire();

            }

           

           

        }
    }

    [Command]
    public void CmdMoveMe(Vector3 positionRB)
    {
        Debug.Log("hola");
        RpcMovement(positionRB);
    }

    [ClientRpc]
    public void RpcMovement(Vector3 positionRB)
    {
        rb.position = positionRB;


    }
    [Command]
    public void CmdSpawnTeleparticles()
    {
        Vector3 playerpos = transform.position;
        // playerpos.y = playerpos.y + 4f;
        // GameObject fireball = (GameObject)Instantiate(fireball_prefab, posfire, this.GetComponentInChildren<Camera>().transform.rotation) as GameObject;
        Teleparticles_prefab = (GameObject)Instantiate(Teleparticles_prefab, playerpos, transform.rotation) as GameObject;
        NetworkServer.Spawn(Teleparticles_prefab);
    }

    [Command]
    public void Cmdshot(Vector3 position)
    {
  
        // playerpos.y = playerpos.y + 4f;
        // GameObject fireball = (GameObject)Instantiate(fireball_prefab, posfire, this.GetComponentInChildren<Camera>().transform.rotation) as GameObject;
        GameObject PlaceholderModel_prefab_new = (GameObject)Instantiate(PlaceholderModel_prefab, position, transform.rotation) as GameObject;
        PlaceholderModel_prefab_new.GetComponent<DeleteAfter>().startKillingPrefab();
        NetworkServer.Spawn(PlaceholderModel_prefab_new);
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
}
