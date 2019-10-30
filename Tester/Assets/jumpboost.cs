using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpboost : MonoBehaviour {
    public Rigidbody rb;
    public GameObject player;
    public float effect;
    // Use this for initialization
    void Start () {
       // rb = player.GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" )
        {

            rb.AddForce((other.gameObject.transform.forward  * -1) * effect);
        }

    }
}
