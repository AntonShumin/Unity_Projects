using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_input : MonoBehaviour {

    private Rigidbody m_rb;
    public float m_velocity_multiplier = 10;

    //cached
    private Vector3 c_velocity = Vector3.zero;
    private Vector3 c_rotation = Vector3.zero;

	void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        c_velocity.x = Input.GetAxis("X");
        c_velocity.y = Input.GetAxis("Z");
        c_velocity.z = Input.GetAxis("Y");
        c_velocity *= m_velocity_multiplier;
        c_rotation.z = Input.GetAxis("rot_z");
        Debug.Log(c_velocity);
        m_rb.velocity = c_velocity;
        m_rb.angularVelocity = c_rotation;
        
		
	}
}
