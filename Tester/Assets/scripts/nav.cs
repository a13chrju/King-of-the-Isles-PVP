using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class nav : MonoBehaviour {
    public Transform [] runaround;
    NavMeshAgent agent;
    int nextindex = 1;
    float Delayedreached;
    // Use this for initialization
    void Start () {
         agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(runaround[0].position);
    }
	
	// Update is called once per frame
	void Update () {
        

        if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0 && Time.time > Delayedreached)
        {
            Delayedreached = Time.time + 2f;
            agent.SetDestination(runaround[nextindex].position);
       
            nextindex++;
            if (nextindex == runaround.Length)
            {
                nextindex = 0;
            }
            
        }


    }
}


