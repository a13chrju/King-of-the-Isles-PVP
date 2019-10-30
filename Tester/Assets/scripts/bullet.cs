using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class bullet : NetworkBehaviour  {
    public float speed;
    private bullet myScript;
    public float speedcollid;
    CharacterController fiskar;
    public GameObject collidedwith, impact;
    public AudioClip explodeSound, hitplayerSound;
    private AudioSource source;


    [SyncVar]
    public Quaternion direction;

    public float SpeedInUnitsPerSecond = 20;

    float canbounce;
	// Use this for initialization
	void Start () {
        myScript = gameObject.GetComponent<bullet>();
        source = GetComponent<AudioSource>();
        // fiskar = gameObject.GetComponent<CharacterController>();

        /* if (isLocalPlayer)
         {
             Destroy(this);
             return;
         }*/

        //this.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        this.GetComponent<Rigidbody>().velocity = (this.transform.forward) *  100;
    }

    private void Update()
    {
        this.transform.rotation = direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "debris")
        {
            source.PlayOneShot(explodeSound, 1f);
        }
        if (collision.collider.tag != "debris" && collision.collider.tag != "Player")
        {
           
                Cmdremove();
          
        
        }

      

    }

    [Command]
    public void Cmdremove()
    {
        //Apply it to all other clients

        Vector3 playerpos = transform.position;





        // GameObject fireball = (GameObject)Instantiate(fireball_prefab, posfire, this.GetComponentInChildren<Camera>().transform.rotation) as GameObject;
        GameObject balstimpact = (GameObject)Instantiate(impact, playerpos, this.transform.rotation) as GameObject;
        NetworkServer.Spawn(balstimpact);
        
        StartCoroutine(destroydelay());
    

    

  
    }

    IEnumerator destroydelay()
    {
       
        yield return new WaitForSeconds(3);
        NetworkServer.Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player" && isServer )
        {
            if (other.gameObject.GetComponent<disabelrag>().stopit == false)
            {
                other.gameObject.GetComponent<disabelrag>().ragdollenable_rpcchain();
              //  collidedwith = other.gameObject;
              //  Debug.Log(other.gameObject.tag);
                var fisk = this.gameObject.GetComponent<Rigidbody>().velocity.normalized * speedcollid;
               other.gameObject.GetComponent<move>().pushback(fisk);

            }
            if (!source.isPlaying)
            {
                source.PlayOneShot(hitplayerSound, 0.6f);
            }

        }


    }
    
    [Command]
    public void Cmdforce()
    {
        
        //Apply it to all other clients
        NetworkInstanceId bullet_id = this.netId;
        //Rpcforce();
    }

    [Command]
    public void Cmdmovebullet()
    {
        NetworkInstanceId bullet_id = this.netId;
        //Apply it to all other clients
        Rpcmovebullet();
    }

    [ClientRpc]
    public void Rpcmovebullet()
    {
        // GameObject bullet = ClientScene.FindLocalObject(bullet_id);
        // bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        this.transform.Translate(transform.forward * Time.deltaTime * 20); 
    }

    [Command]
    public void Cmdshotanim()
    {
        //Apply it to all other clients
       // collidedwith.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward);
    }

}
