using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class friendly : MonoBehaviour {

    // Use this for initialization
    public bool notConfused = true;
    public GameObject falloff;
    private Rigidbody rb;
    private NavMeshAgent agent;
    private GameObject player;
    private bool isMoveable = true;
    private float canmove = 0f;
    private float distToGround;

    void Start () {
        rb = this.GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }
	
	// Update is called once per frame
	void Update () {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        catch (Exception e)
        {

        }
        if (isMoveable && player != null && IsGroundedTwo())
        {
       
            agent.SetDestination(player.transform.position);
        }

        if (isMoveable == false && Time.time > canmove)
        {
            agent.enabled = true;
            isMoveable = true;
        }
        // Debug.Log(agent.remainingDistance);


        /*  Vector3 direction = (rb_player.transform.position - transform.position).normalized;
          Vector3 falloff_dir = (falloff.transform.position - transform.position).normalized;

          float angle = Vector3.Angle(this.transform.forward, direction);
          Debug.Log(angle);
          if (notfacing)
          {
              rb.MovePosition(transform.position + direction * 5f * Time.deltaTime);
          }
      */
    }

    public bool IsGroundedTwo()
    {
        Debug.Log(distToGround);
        Debug.Log(Physics.Raycast(transform.position, -Vector3.up, distToGround + 1f));
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    public void getHit()
    {
        canmove = Time.time + 4f;
        isMoveable = false;
        agent.enabled = false;
    }

}
