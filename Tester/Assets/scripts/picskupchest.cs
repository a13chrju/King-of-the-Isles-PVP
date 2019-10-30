using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupchest : MonoBehaviour {

    // Use this for initialization
    public int typeofteam = 0;

    public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("correct");
            var team = other.gameObject.GetComponent<setup>().Team;
           // other.gameObject.GetComponent<capture>().captureflag(team, false);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var team = other.gameObject.GetComponent<setup>().Team;
         //   other.gameObject.GetComponent<capture>().nocaptureflag(team);

        }
    }

    public void Open()
    {
        anim.SetTrigger("open");
    }

}
