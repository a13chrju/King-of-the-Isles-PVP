using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DeleteAfter : NetworkBehaviour {

	// Use this for initialization
	void Start () {
     
    }

    IEnumerator destroydelay()
    {
        Debug.Log("HEY OKej");
        yield return new WaitForSeconds(10);
        NetworkServer.Destroy(this.gameObject);
    }

    public void startKillingPrefab()
    {
        StartCoroutine(destroydelay());
    }
}
