using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class script_manager_duck{

    public Color m_PlayerColor;
    public Transform m_SpownPoint;
    public Texture m_Texture;
    public string m_Name;
    public Text m_Score_Text;

    [HideInInspector]    public int m_PlayerNumber;
    [HideInInspector]    public string m_ColoredPlayerText;
    [HideInInspector]    public GameObject m_Instance;
    [HideInInspector]    public int m_Lives;
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
        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">"+ m_Name +  "</color>";
        m_Instance.GetComponentInChildren<Renderer>().material.SetTexture("_MainTex", m_Texture);

    }

    public void DisableControl()
    {
        //m_script_movement.enabled = false;
        m_script_movement.movement_active(false);
    }

    public void EnableControl()
    {
        //m_script_movement.enabled = true;
        m_script_movement.movement_active(true);

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
        //EnableControl();
        EnableCamera();
    }

    public void OutOfBounds_Push()
    {
        m_script_movement.m_animator.SetTrigger("flap_slow");
        if (m_rigidbody.velocity.magnitude < 80 )
        {
            
            Vector3 direction = Vector3.Normalize(m_Instance.transform.position);
            m_rigidbody.AddForce(direction * 9000000);
        }
    }

    public int Lose_Life()
    {

        m_Lives--;
        return m_Lives;
    }

    public string Get_Lives()
    {
        return "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">" + m_Lives + "</color>";
    }

}
