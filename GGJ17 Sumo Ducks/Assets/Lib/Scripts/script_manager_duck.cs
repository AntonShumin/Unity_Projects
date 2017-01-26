using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_manager_duck : MonoBehaviour {

    public Color m_PlayerColor;
    public Transform m_SpownPoint;
    [HideInInspector]
    public int m_PlayerNumber;
    [HideInInspector]
    public string m_ColoredPlayerText;
    [HideInInspector]
    public GameObject m_Instance;
    [HideInInspector]
    public int m_Wins;

    private script_movement m_script_movement;
    private Collider m_collider;
    private Rigidbody m_rigidbody;

}
