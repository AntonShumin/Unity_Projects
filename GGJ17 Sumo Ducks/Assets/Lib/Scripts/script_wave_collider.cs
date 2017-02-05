using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_wave_collider : MonoBehaviour {

    private script_wave m_parent;

    void Start()
    {
        m_parent = transform.GetComponentInParent<script_wave>();
    }

    public void OnTriggerEnter(Collider col)
    {
        m_parent.Collider_Event(col);
    }
}
