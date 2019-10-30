using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class cam : NetworkBehaviour {

        public GameObject camera;
        public float turnSpeed = 4.0f;
        public Transform player;

        private Vector3 offset;

        void Start() 
        {
         
            if (!isLocalPlayer)
            {
                Destroy(this);
                return;
            }
           // offset = new Vector3(player.position.x, player.position.y + 8.0f, player.position.z + 10.0f);
        }

        void LateUpdate()
        {
        // offset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, Vector3.up) ;
       // Vector3 offsetwa = new Vector3(transform.position.x, transform.position.y, transform.position.z * Input.GetAxis("Mouse Y"));
        //  transform.position = player.position + offset;
        //transform.LookAt(player.position);

            
        }

    private void Update()
    {
        var y = Input.GetAxis("Mouse Y") * 6f;
       // var x = Input.GetAxis("Mouse X") * 6f;

        camera.transform.Rotate(-y, 0, 0);

    }
}
