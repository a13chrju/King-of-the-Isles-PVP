using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFall : MonoBehaviour {
    public float effect;
   // public Rigidbody rb;
    public bool hasfallen = false;
    // Use this for initialization
    void Start()
    {
        
       // rb.AddForce(transform.forward * effect);
    }

    // Update is called once per frame
    void Update () {
      /*  if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Space key was pressed.");
            rb.AddForce(transform.right * effect);
        }*/
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Player" && hasfallen == false)
        {
            hasfallen = true;
         //   rb.AddForce((transform.forward * -1) * effect);
        }

    }
}
