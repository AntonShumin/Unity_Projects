using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_ship_movement : MonoBehaviour {

    public float m_movement_speed;

    private Rigidbody m_rigid_body;

	void Awake()
    {
        m_rigid_body = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        m_rigid_body.AddForce(movement * m_movement_speed);

    }
}
