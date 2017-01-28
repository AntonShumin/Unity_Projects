using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_movement : MonoBehaviour {

    public int m_PlayerNumber = 1;
    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;
    public bool m_push_enabled = true;

    private string m_HorizontalAxisName;
    private string m_VerticalAxisName;
    private Rigidbody m_Rigidbody;
    private Vector3 m_MovementVector;
    private Vector3 m_Last_Velocity;
    

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

        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        velocity.y = 0f;
        m_Last_Velocity = velocity;
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

    //Duck collision pushback
    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player" && m_push_enabled)
        {
            //Get attack vector
            Vector3 pos_target = col.transform.position;
            Vector3 pos_origin = transform.position;
            Vector3 difference_vector =Vector3.Normalize( pos_target - pos_origin );
            difference_vector.x = Mathf.Abs(difference_vector.x);
            difference_vector.y = Mathf.Abs(difference_vector.y);
            difference_vector.z = Mathf.Abs(difference_vector.z);
            Debug.Log("difference is " + difference_vector);

            Rigidbody rigid = col.GetComponent<Rigidbody>();
            Vector3 force = Vector3.Scale(Vector3.Normalize(m_Last_Velocity),difference_vector)  * 8000000;

            rigid.velocity = Vector3.zero;
            rigid.AddForce(force);
            m_push_enabled = false;
            StartCoroutine(resetPush());
            Debug.Log("force is " + force + " original force is " + Vector3.Normalize(m_Last_Velocity) * 1000000);

            //trigger oponent push
            col.GetComponent<script_movement>().OnTriggerEnter(gameObject.GetComponent<Collider>());
        }
        
    }

    private IEnumerator resetPush()
    {
        yield return new WaitForSeconds(0.5f);
        m_push_enabled = true;
    }


}
