﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class move : NetworkBehaviour {
    public Rigidbody rb;
    public float MoveSpeed;
    public float gravityscale;
    public float jumpforce;
    public CharacterController chara;
    public float jumptimer;
    public float speedeffect;
    public float power = 0;
    public float fall;
    private Camera camera;
    public float lastClickTime;
    public GameObject particles;
    public Animator anim;
    public float distToGround;
    public Vector3 moveDirection;
    public bool isjumping = false;
    public float jumpdelay;
    public Vector3 getpushed;
    public float fireballimpactspeed;
    public GameObject innerbone;
    public float lookspeed;

    public GameObject water;

    public AudioClip jumpSound;
    private AudioSource source;
    private float timedelayjump;
    private float canjump = 0f;
    private float moveHorizontal, moveVertical;
    private Vector3 movHorizontal, movVertical;

    private bl_Joystick Joystick;

    private bool btnJump = false; 
    // Use this for initialization

         public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
       // GameObject.Find("Sjukt").GetComponent<Button>().onClick.AddListener(() => CmdJump());
     //   Debug.Log(GameObject.Find("Sjukt"));
       // Application.targetFrameRate = 24;
    }
/*
    [Command]
    public void CmdJump()
    {
        Debug.Log("OOOwwwwww!");
        RpcFireBoiJump();

    }*/


    void Start() {
        if (isLocalPlayer)
        {
            camera = this.GetComponentInChildren<Camera>();
            distToGround = GetComponent<Collider>().bounds.extents.y;
            rb = this.GetComponent<Rigidbody>();
            chara = GetComponent<CharacterController>();
            jumptimer = Time.time + 2f; Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.lockState = CursorLockMode.Confined;
            lastClickTime = Time.time;
           // Joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<bl_Joystick>();
        }
        else
        {
           // Destroy(this);
        }

        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }


    public float rotateSpeed = 3.0F;
    /*
    void Update()
    {

        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Cmdshotanim();
            }
        
            if (chara.isGrounded)
            {
                // We are grounded, so recalculate
                // move direction directly from axes
                

                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection = moveDirection * MoveSpeed;

                if (Input.GetKeyDown(KeyCode.Space))
                {

                    Cmdjump();
                    // anim.SetBool("isfalling", true);
                    jumpdelay = Time.time + 0.1f;
                    isjumping = true;


                }
            }


            if (Input.GetKeyDown(KeyCode.E))
            {
                SetKinematic(false);

            }
        
            if (Time.time > jumpdelay && isjumping == true)
            {
                isjumping = false;
            
                moveDirection.y = jumpforce;
            }
            // Apply gravity
            moveDirection.y = moveDirection.y - (Physics.gravity.magnitude * Time.deltaTime * fall);
            //moveDirection.y -= (Physics.gravity.y * -1)* Time.deltaTime;
            transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);
            // Move the controller
            chara.Move(moveDirection * Time.deltaTime);

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            // Animate the player.
            Animating(moveHorizontal, moveVertical);
        }
    }
 */
 public void resetvelo()
    {
        movHorizontal = (transform.right * 0) * -1f;
        movVertical = ((transform.forward * 0) * -1f);
    }

 /*
    //Client Side.   
    [ClientRpc]
    void RpcFireBoiJump()
    {
        Debug.Log("jump!");
        if (Input.GetKeyDown("space") isLocalPlayer && IsGroundedTwo() && (Time.time > canjump))
        {
            Debug.Log("JUMP");
            canjump = Time.time + 0.3f;
            source.PlayOneShot(jumpSound, 1f);
            rb.AddForce(Vector3.up * jumpforce);
            Cmdjump();
            jumpdelay = Time.time + 0.1f;
            isjumping = true;
        }
    }
*/

    void Update()
    {
        if (isLocalPlayer)
        {
            moveHorizontal = Input.GetAxis("Horizontal") * -1f; //Joystick.Horizontal
            moveVertical = Input.GetAxis("Vertical") * -1f; // Joystick.Vertical
            movHorizontal = (transform.right * moveHorizontal) * -1f;
            movVertical = ((transform.forward * moveVertical) * -1f);

            rb.AddForce(Vector3.down * gravityscale, ForceMode.Force);

            Vector3 Velocity = ((movHorizontal + movVertical).normalized) * MoveSpeed;
            rb.MovePosition(rb.position + Velocity * Time.fixedDeltaTime);
            Animating(moveHorizontal, moveVertical);

        }

        if (isLocalPlayer)
        {
            //for andriod use mouse X: Joystick.Horizontal
            Vector3 offset = new Vector3(0, Input.GetAxis("Mouse X") * lookspeed, 0);
           
                rb.MoveRotation(rb.rotation * Quaternion.Euler(offset));

            if (Input.GetKeyDown("space") && /*(Time.time > canjump)*/ IsGroundedTwo())
            {
                Debug.Log("JUMP2");
                canjump = Time.time + 0.5f;
                source.PlayOneShot(jumpSound, 1f);
                rb.AddForce(Vector3.up * jumpforce);
                Cmdjump();
                jumpdelay = Time.time + 0.1f;
                isjumping = true;
            }

        }
    }


    void FixedUpdate()
    {



    }



    public void Animating(float valone, float valtwo){

        bool walking = valone != 0f || valtwo != 0f;

        // anim.SetBool("isfalling", false);

        anim.SetFloat("Vertical", valtwo * -1);
        anim.SetFloat("Horizontal", valone * -1);
        //anim.SetBool("isrunning", walking);
       

        if (rb.velocity.y < -70f && (IsGrounded() == false))
         {
          //  Debug.Log(rb.velocity.y);
            anim.SetBool("isfalling", true);
         }else if (rb.velocity.y > 40f && (IsGrounded() == false))
        {
            anim.SetBool("isfalling", true);
        }
         else
         {
            if ((rb.velocity.y < 30f) && (Time.time > timedelayjump) && (IsGrounded() == true))
            {
              //  Debug.Log(rb.velocity.y + "true");
                anim.SetBool("isfalling", false);
            }
             //anim.SetBool("isfalling", false);
             //anim.SetBool("isrunning", walking);
         }
    }

    public bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 12f);
    }

    public bool IsGroundedTwo()
    {
        Debug.Log(distToGround);
        Debug.Log(Physics.Raycast(transform.position, -Vector3.up, distToGround + 1f));
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
    

    void SetKinematic(bool newValue)
    {
        GetComponent<Animator>().enabled = false;
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        rb.isKinematic = false;
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = false;
        }
    }

    [Command]
    public void Cmdjump()
    {
        //Apply it to all other clients
        Rpcjump();
    }

    [ClientRpc]
    public void Rpcjump()
    {
        timedelayjump = Time.time + 0.4f;
       // anim.SetTrigger("jump");
        anim.SetBool("isfalling", true);
    }


    [Command]
    public void Cmdshotanim()
    {

        Debug.Log("fafa");
        //Apply it to all other clients
        this.GetComponent<Rigidbody>().AddForce(transform.forward * fireballimpactspeed);
    }


    public void pushback(Vector3 force)
    {
        getpushed = force;
        Rpcforce(force);
    }

    [Command]
    public void Cmdfsafasfaim()
    {
        //Rpcforce();
    }


    [ClientRpc]
    public void Rpcforce(Vector3 fisk)
    {

        this.gameObject.GetComponent<Rigidbody>().AddForce(fisk * 5);
    }
}
