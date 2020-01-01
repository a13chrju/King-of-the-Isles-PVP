using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class disabelrag : NetworkBehaviour {
    Collider[] rigColliders;
    Rigidbody[] rigRigidbodies;
    CharacterJoint[] hingess;
    Animator anim;
    public Collider MainCollider;
    public Rigidbody MainRigidbody;

    public HingeJoint connectoir;
    public Rigidbody joiner;
    public FixedJoint fixedjjo;

    public Transform innerrotationbone;
    public GameObject innerbone;

    // public Collider maincollid;
    public GameObject spinecollid;
    private float Timetostoprag = 0;

    [SyncVar]
    public bool stopit = false;
    private float coolrag = 5;

    //canvas
    public GameObject mycanvas;
    public GameObject mycanvasDazerNet;
    // public Text mycanvastxt;

    [SyncVar]
    public bool isragdoll = false;

    public float dazetime;


    void Start () {
        //joiner = connectoir.anchor;
        rigColliders = GetComponentsInChildren<Collider>();
        anim = this.GetComponent<Animator>();
        rigRigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdolldisable();
        fixedjjo = GetComponentInChildren<FixedJoint>();
        hingess = GetComponentsInChildren<CharacterJoint>();
        mycanvas = GameObject.FindGameObjectWithTag("dazed");
       // mycanvastxt = mycanvas.GetComponentInChildren<Text>();

      /*  if (!isLocalPlayer)
        {
            if (this.GetComponentInChildren<FixedJoint>() == null)
            {
                innerbone.AddComponent<FixedJoint>();
                this.GetComponentInChildren<FixedJoint>().connectedBody = this.GetComponent<Rigidbody>();

            }
            ragdollenable_rpcchain();
        }*/

        mycanvasDazerNet.GetComponent<CanvasGroup>().alpha = 0;
        mycanvas.GetComponent<CanvasGroup>().alpha = 0;
    }
	
	// Update is called once per frame
	void Update () {


        if (isragdoll == true && isServer)
        {
            Rpcmovebullet();
        }

        if (Time.time > Timetostoprag && isServer && stopit == true)
        {
            //stopit = false;
            Rpcfalsestop();
            RpcDisableRPCragdoll();
        }
        if (isLocalPlayer)
        {
            if (stopit == false)
            {
                mycanvas.GetComponent<CanvasGroup>().alpha = 0;
            }

            if (stopit == true)
            {
                //stopit = false;
                mycanvas.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
        else
        {
            if (stopit == false)
            {
                mycanvasDazerNet.GetComponent<CanvasGroup>().alpha = 0;
            }

            if (stopit == true)
            {
                //stopit = false;
                mycanvasDazerNet.GetComponent<CanvasGroup>().alpha = 1;
            }
          
        }
       
    }

    [ClientRpc]
    public void RpcDisableRPCragdoll()
    {
        Debug.Log("EEEEEE");
        // GameObject bullet = ClientScene.FindLocalObject(bullet_id);
        // bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        ragdolldisable();

    }



    public void ragdolldisable()
    {
        this.GetComponent<fireballs>().enabled = true;
        this.GetComponent<move>().enabled = true;

        if (this.GetComponentInChildren<FixedJoint>() != null)
        {
            Destroy(this.GetComponentInChildren<FixedJoint>());
        }
        anim.enabled = true;
       

        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.velocity = Vector3.zero;
            rb.detectCollisions = false;
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        foreach (Collider col in rigColliders)
        {
            col.enabled = false;
        }

        MainRigidbody.detectCollisions = true;
        MainCollider.enabled = true;


    }
    public void ragdollenable_rpcchain()
    {

        Rpcmovebullet();

        if (isServer)
        {
            Timetostoprag = Time.time + dazetime;
        }
    }
    

    [ClientRpc]
    public void Rpcmovebullet()
    {

        // GameObject bullet = ClientScene.FindLocalObject(bullet_id);
        // bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        ragdollenable();
        Rpcisragdollfal();
    }

    [ClientRpc]
    public void Rpcfalsestop()
    {
        // GameObject bullet = ClientScene.FindLocalObject(bullet_id);
        // bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        stopit = false;
       // Timetostoprag = Time.time + 5f;
    }

    [ClientRpc]
    public void RpcTruestop()
    {
        // GameObject bullet = ClientScene.FindLocalObject(bullet_id);
        // bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        stopit = true;
        // Timetostoprag = Time.time + 5f;
    }

    [ClientRpc]
    public void Rpcisragdoll()
    {
        // GameObject bullet = ClientScene.FindLocalObject(bullet_id);
        // bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        isragdoll = true;
    }

    [ClientRpc]
    public void Rpcisragdollfal()
    {
        // GameObject bullet = ClientScene.FindLocalObject(bullet_id);
        // bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        isragdoll = false;
    }


    public void ragdollenable()
    {
        Timetostoprag = Time.time + dazetime;
        RpcTruestop();
        this.GetComponent<fireballs>().enabled = false;
        this.GetComponent<move>().enabled = false;
        this.GetComponent<Rigidbody>().isKinematic = true;
      //  Vector3 playerpos = transform.position; //move up player so it does glitch through the floor
       // playerpos.y = playerpos.y + 4f;
       // this.transform.position = playerpos;

     

        if (this.GetComponentInChildren<FixedJoint>() == null)
        {
            innerbone.AddComponent<FixedJoint>();
            this.GetComponentInChildren<FixedJoint>().connectedBody = this.GetComponent<Rigidbody>();

        }
       
       

        /* if (this.GetComponent<HingeJoint>() == null)
         {
             HingeJoint fsa = this.gameObject.AddComponent<HingeJoint>();
            this.GetComponent<HingeJoint>().connectedBody = joiner;
         }
         */
        anim.enabled = false;

       
        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.velocity = Vector3.zero;
            rb.detectCollisions = false;
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        foreach (Collider col in rigColliders)
        {
            col.enabled = true;
        }

        MainRigidbody.detectCollisions = true;
        this.GetComponent<Rigidbody>().isKinematic = false;


    }
}


 