﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_movement : MonoBehaviour {

    public int m_PlayerNumber = 1;
    public float m_Speed;
    public float m_Speed_Airborm;
    public float m_TurnSpeed = 180f;
    public bool m_push_enabled = true;

    private string m_HorizontalAxisName;
    private string m_VerticalAxisName;
    private string m_jump_name;
    private string m_fire_name;
    private Rigidbody m_Rigidbody;
    private Vector3 m_MovementVector;
    private Vector3 m_Last_Velocity;
    private bool m_airborn;
    private Vector3 m_raycast_offset = new Vector3(0,2,0);
    private script_manager_collector m_object_collector;
    

    private float m_OriginalPitch;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementVector.x = 0;
        m_MovementVector.z = 0;
    }


    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_HorizontalAxisName = "Horizontal_" + m_PlayerNumber;
        m_VerticalAxisName = "Vertical_" + m_PlayerNumber;
        m_jump_name = "Jump_" + m_PlayerNumber;
        m_fire_name = "Fire_" + m_PlayerNumber;
        m_object_collector = GameObject.Find("Object Collector").GetComponent<script_manager_collector>();

    }

    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
        m_MovementVector.x = Input.GetAxis(m_HorizontalAxisName);
        m_MovementVector.z = Input.GetAxis(m_VerticalAxisName);
        m_MovementVector.y = 0;

        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        velocity.y = 0f;
        m_Last_Velocity = velocity;

        Jump();
        Fire();
    }



    private void FixedUpdate()
    {
        Move();
        if (m_MovementVector.magnitude > 0)
        {
            Turn();
        }
        
    }

    private void Move()
    {
        if(transform.position.y > 1)
        {
            m_Rigidbody.AddForce(m_MovementVector * m_Speed_Airborm);
        } else
        {
            m_Rigidbody.AddForce(m_MovementVector * m_Speed);
        }
        

    }

    private void Turn()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_MovementVector), Time.fixedDeltaTime*5);
    }

    //Duck collision pushback
    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player" && m_push_enabled)
        {
            // if difference_vector and velocity have posite sign, set difference vector xyz to 0
            // add minimum force (if abs < 300 000 then *4)


            //Get attack vector
            Vector3 pos_target = col.transform.position;
            Vector3 pos_origin = transform.position;
            Vector3 difference_vector =Vector3.Normalize( pos_target - pos_origin );
            difference_vector.x = Mathf.Abs(difference_vector.x);
            difference_vector.y = Mathf.Abs(difference_vector.y);
            difference_vector.z = Mathf.Abs(difference_vector.z);
            //Debug.Log("difference is " + difference_vector);


            //calculate force
            Rigidbody rigid = col.GetComponent<Rigidbody>();
            Vector3 force = Vector3.Scale(m_Last_Velocity,difference_vector)  * 300000;

            //push 
            rigid.velocity = Vector3.zero;
            if (Mathf.Abs(force.x) < 2000000) force.x *= 3;
            if (Mathf.Abs(force.z) < 2000000) force.z *= 3;
            rigid.AddForce(force);
            Debug.Log("force is " + force + " original force is " + Vector3.Normalize(m_Last_Velocity) * 300000);

            //timeout push
            m_push_enabled = false;
            StartCoroutine(resetPush());
            

            //trigger oponent push
            col.GetComponent<script_movement>().OnTriggerEnter(gameObject.GetComponent<Collider>());
        }
        
    }

    private IEnumerator resetPush()
    {
        yield return new WaitForSeconds(0.5f);
        m_push_enabled = true;
    }

    private void Jump()
    {
        //Grounded detection
        

        if ( Input.GetButtonDown(m_jump_name) )
        {
            if (Physics.Raycast(transform.position + m_raycast_offset, Vector3.down, 3))
            {
                //half velocity 
                m_Rigidbody.velocity /= 2;

                //add jump force 
                m_Rigidbody.AddForce(new Vector3(0, 7000000f, 0));

                //set vars
                m_airborn = true;
            } else if (transform.position.y > 3 && m_airborn)
            {
                //add dive force 
                m_Rigidbody.AddForce(new Vector3(0, -9000000f, 0));
                m_airborn = false;
            }

        } 
       
    }

    private void Fire()
    {
        if( Input.GetButtonDown(m_fire_name) )
        {
            Debug.Log("Fire");
            GameObject missile = m_object_collector.Get_Missile();
            missile.SetActive(true);
            missile.GetComponent<script_missile>().Fire(m_PlayerNumber, transform.position,transform.forward);
        }
    }





   


}
