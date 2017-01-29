using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_manager_game : MonoBehaviour {

    public int m_rounds_total;
    public float m_start_delay;
    public float m_end_delay;
    public script_camera m_script_camera;
    public GameObject m_prefab_duck;
    public script_manager_duck[] m_script_ducks;

    private int m_rounds_current;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    private script_manager_duck m_RoundWinner;
    private script_manager_duck m_GameWinner;

    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_start_delay);
        m_EndWait = new WaitForSeconds(m_end_delay);

        Spawn_All_Ducks();
        Set_Camera_Targets();
    }


    void Update()
    {
        if (Input.GetKeyDown("return") || Input.GetKeyDown("enter"))
        {
            Round_Reset();
        }
    }




    private void Spawn_All_Ducks()
    {
        for (int i = 0; i < m_script_ducks.Length; i++ )
        {
            m_script_ducks[i].m_Instance = Instantiate(m_prefab_duck, m_script_ducks[i].m_SpownPoint.position, m_script_ducks[i].m_SpownPoint.rotation) as GameObject;
            m_script_ducks[i].m_PlayerNumber = i + 1;
            m_script_ducks[i].Setup();    
        } 
    }

    private void Set_Camera_Targets()
    {
        //Preset vaar
        Transform[] targets = new Transform[m_script_ducks.Length];

        //colleect targets
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = m_script_ducks[i].m_Instance.transform;
        }

        //set targets
        m_script_camera.m_Targets = targets;

    }

    private void Round_Reset()
    {
        foreach( script_manager_duck script in m_script_ducks )
        {
            //reset position
            script.Reset();


        }
    }



}
