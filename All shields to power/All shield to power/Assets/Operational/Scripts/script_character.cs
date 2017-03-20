using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_character : MonoBehaviour {

    public float speed = 2f;

    private CharacterController m_player_controller;
    private GameObject m_camera; 
    private float m_mouse_sensitivity = 4f;

	void Awake()
    {
        m_player_controller = GetComponent<CharacterController>();
        m_camera = Camera.main.gameObject;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        /*
        //movement
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed );
        movement = transform.rotation * movement;
        m_player_controller.Move(movement * Time.deltaTime);
        */
        //rotation

        float rotX = Input.GetAxis("Mouse X") * m_mouse_sensitivity;
        float rotY = Input.GetAxis("Mouse Y") * m_mouse_sensitivity;

        transform.Rotate(0,rotX,0);
        m_camera.transform.Rotate(-rotY,0,0);
		
	}
}
