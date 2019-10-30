using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallDown : MonoBehaviour {

    public Rigidbody rb;
 //   public Text score;
    public bool isactivehere = true;
    public bool alltypeofweight = false;
    public bool falling = false;
    public bool respawner = false;
    public Vector3 myposition;
    public Quaternion myrotation;
    public float timedelay;
    public float respawndelay;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        myposition = this.transform.localPosition;
        myrotation = this.transform.rotation;
       // score = GameObject.FindGameObjectWithTag("scoretxt").GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > timedelay  && falling == true) // save the position of where the player is.
        {

            if (rb.isKinematic == true)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }

            Debug.Log("AAAAABB");
            falling = false;
            respawner = true;
            respawndelay = Time.time + 8f;
        }

        if (Time.time > respawndelay && respawner == true) // save the position of where the player is.
        {
            transform.localPosition = myposition;
            transform.rotation =(myrotation);
            rb.useGravity = false;
            rb.isKinematic = true;
            Debug.Log("AAAAA");
            
            respawner = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        


        if (other.gameObject.tag == "bullet")
        {
            if (rb.isKinematic == true)
            {
                respawner = true;
                respawndelay = Time.time + 8f;
                rb.useGravity = true;
                rb.isKinematic = false;
            }
            
        }else if (other.gameObject.tag == "Player" && alltypeofweight == true)
        {
            if (rb.isKinematic == true)
            {

                timedelay = Time.time + 0.4f;
                falling = true;
            }

        }


        if (other.gameObject.tag == "debris" && isactivehere == true) //hit the floor
        {
            Destroy(gameObject, 1);
            isactivehere = false;
            gamemanagers.score++;
           // score.text = gamemanagers.score + "/" + gamemanagers.levelScore;
        }
    }
}
