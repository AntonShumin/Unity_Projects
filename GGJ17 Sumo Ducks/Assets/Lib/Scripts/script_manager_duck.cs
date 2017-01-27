using System;
using UnityEngine;

[Serializable]
public class script_manager_duck{

    public Color m_PlayerColor;
    public Transform m_SpownPoint;

    [HideInInspector]    public int m_PlayerNumber;
    [HideInInspector]    public string m_ColoredPlayerText;
    [HideInInspector]    public GameObject m_Instance;
    [HideInInspector]    public int m_Wins;

    private GameObject m_canvas_element;
    private script_movement m_script_movement;
    private Collider m_collider;
    private Rigidbody m_rigidbody;

    public void Setup()
    {
        m_script_movement = m_Instance.GetComponent<script_movement>();

        m_script_movement.m_PlayerNumber = m_PlayerNumber;

        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";
    }

    public void DisableControl()
    {
        m_script_movement.enabled = false;

  
    }

    public void EnableControl()
    {
        m_script_movement.enabled = true;
    }

    public void Reset()
    {
        m_Instance.transform.position = m_SpownPoint.position;
        m_Instance.transform.rotation = m_SpownPoint.rotation;
    }

}
