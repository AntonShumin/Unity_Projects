using System;
using UnityEngine;

[Serializable]
public class script_manager_duck{

    public Color m_PlayerColor;
    public Transform m_SpownPoint;
    public Texture m_Texture;

    [HideInInspector]    public int m_PlayerNumber;
    [HideInInspector]    public string m_ColoredPlayerText;
    [HideInInspector]    public GameObject m_Instance;
    [HideInInspector]    public int m_Wins;
    [HideInInspector]    public script_movement m_script_movement;

    private GameObject m_canvas_element;
    
    private Collider m_collider;
    private Rigidbody m_rigidbody;

    public void Setup()
    {

        //Scripts
        m_script_movement = m_Instance.GetComponent<script_movement>();
        m_rigidbody = m_Instance.GetComponent<Rigidbody>();


        //Vars
        m_script_movement.m_PlayerNumber = m_PlayerNumber;

        //Colors
        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";
        m_Instance.GetComponentInChildren<Renderer>().material.SetTexture("_MainTex", m_Texture);

    }

    public void DisableControl()
    {
        //m_script_movement.enabled = false;
        m_script_movement.m_movement_active = false;
    }

    public void EnableControl()
    {
        //m_script_movement.enabled = true;
        m_script_movement.m_movement_active = true;

    }

    public void DisableCamera()
    {
        m_script_movement.m_camera_active = false;
    }

    public void EnableCamera()
    {
        m_script_movement.m_camera_active = true;
    }

    

    public void Reset()
    {
        //reset position
        m_Instance.transform.position = m_SpownPoint.position;
        m_Instance.transform.rotation = m_SpownPoint.rotation;

        //reset velocity 
        m_Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;

        //enable movement script
        EnableControl();
        EnableCamera();
    }

    public void OutOfBounds_Push()
    {
        if(m_rigidbody.velocity.magnitude < 30 )
        {
            Vector3 direction = Vector3.Normalize(m_Instance.transform.position);

        }
    }

}
