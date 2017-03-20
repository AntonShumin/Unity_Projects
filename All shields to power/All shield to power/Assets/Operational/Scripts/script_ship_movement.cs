using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_ship_movement : MonoBehaviour {

    public float m_movement_speed;
    public float m_rotation_speed;

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
        //movement
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(0, 0, moveVertical);
        movement = transform.rotation * movement;
        m_rigid_body.AddForce(movement * m_movement_speed);

        //
        float moveHorizontal = Input.GetAxis("Horizontal");
        transform.Rotate(0, moveHorizontal * m_rotation_speed, 0);

    }
}
