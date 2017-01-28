using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_movement : MonoBehaviour {

    public int m_PlayerNumber = 1;
    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;

    private string m_HorizontalAxisName;
    private string m_VerticalAxisName;
    private Rigidbody m_Rigidbody;
    private Vector3 m_MovementVector;

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

    }

    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
        m_MovementVector.x = Input.GetAxis(m_HorizontalAxisName);
        m_MovementVector.z = Input.GetAxis(m_VerticalAxisName);
        m_MovementVector.y = 0;

        if (m_PlayerNumber == 1)
        {
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            velocity.y = 0f;
            float speed = velocity.magnitude;
            Debug.Log(speed);
        }
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

        m_Rigidbody.AddForce(m_MovementVector * m_Speed);
    }

    private void Turn()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_MovementVector), Time.fixedDeltaTime*5);
    }


}
