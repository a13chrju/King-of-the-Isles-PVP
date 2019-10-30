using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterportal : MonoBehaviour {
    public GameObject particles;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject temp = Instantiate(particles, this.transform.position, Quaternion.identity);
            Destroy(temp, 3);
        }
    }
}
