using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_manager_collector : MonoBehaviour {

    public GameObject m_prefab_missile;
    public GameObject[] m_UI;

    private List<GameObject> m_list_missiles_available = new List<GameObject>();
    private GameObject m_missile_parent;

    private void Awake()
    {
        m_missile_parent = GameObject.Find("Missiles");
        
    }

    private void Start()
    {
        //set variabels
        GameObject missile;
        script_missile.m_object_collector = this;

        //spown missiles
        for ( int i = 0; i < 10; i++)
        {
            missile = Instantiate(m_prefab_missile);
            m_list_missiles_available.Add(missile);
            missile.SetActive(false);
            missile.transform.parent = m_missile_parent.transform;
        }
        
    }

    public GameObject Get_Missile()
    {
        GameObject missile = m_list_missiles_available[0];
        m_list_missiles_available.RemoveAt(0);
        return missile;

    }

    public void Return_Missile(GameObject missile)
    {
        missile.SetActive(false);
        m_list_missiles_available.Add(missile);

    }
}
