using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaky : MonoBehaviour {
    public float speed = 0.2f; //how fast it shakes
    public float amount = 1.0f; //how much it shakes

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float x = Random.Range(-1f, 1f) * 2;
        float y = Random.Range(-1f, 1f) * 2;

        transform.position = new Vector3(x, y, transform.position.z);


    }

 
}
