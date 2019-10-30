using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    // Use this for initialization
    public GameObject player;
    //public LineRenderer lr;
    public float timeshoot;
    public float timeshootend;
    public float timedelay;
    public Vector3 position0;
    public Vector3 position1;
    public float turnspeed;
    public GameObject bullet;
    public Quaternion fireat;

    void Start () {
      //  lr.enabled = false;
        position0 = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        position1 = player.transform.position;
    }
 
    // Update is called once per frame
    void Update () {
  
        Vector3 relativePos = position1 - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos, Vector3.up);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, toRotation, Time.deltaTime * turnspeed);
        //transform.LookAt(player.transform.position);

        

       
       // transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * turnspeed);
        if (Time.time > timedelay) // save the position of where the player is.
        {
            timedelay = Time.time + 3f;
            position1 = player.transform.position;
     
        }
        if (Time.time > timeshoot)
        {
            
         //   lr.enabled = true;
            timeshoot = Time.time + 4f;
            // lr.SetPosition(0, this.transform.position);
            // lr.SetPosition(1, position1);
         
            Instantiate(bullet, transform.position, transform.rotation);
        

        }
        if (Time.time > timeshootend)
        {
         //   lr.enabled = false;
            timeshootend = Time.time + 4.1f;
           // lr.SetPosition(0, this.transform.position);
          
        }


       

    }
}
