using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_camera : MonoBehaviour {

    public float speed = 10.0f;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    //cached
    private Vector2 c_md = Vector2.zero;
    private Vector2 c_smooth = Vector2.zero;
    private Vector2 c_mouseLook = Vector2.zero;
    private Vector2 c_smoothV = Vector2.zero;


	// Use this for initialization
	void Start () {

        Cursor.lockState = CursorLockMode.Locked;
		
	}
	
	// Update is called once per frame
	void Update () {

        //get inputs
        c_md.x = Input.GetAxisRaw("Mouse X");
        c_md.y = Input.GetAxisRaw("Mouse Y");


        //smooth
        c_smooth.x = c_smooth.y = sensitivity * smoothing;
        c_md = Vector2.Scale(c_md, c_smooth);
        c_smoothV.x = Mathf.Lerp(c_smoothV.x, c_md.x, 1f / smoothing);
        c_smoothV.y = Mathf.Lerp(c_smoothV.y, c_md.y, 1f / smoothing);

        //
        c_mouseLook += c_smoothV;

        //move camera
        transform.localRotation = Quaternion.AngleAxis(-c_mouseLook.y, Vector3.right);
        transform.parent.transform.localRotation = Quaternion.AngleAxis(c_mouseLook.x, transform.parent.transform.up);
		
	}
}
