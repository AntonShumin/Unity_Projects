﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_manager_game : MonoBehaviour {

    [HideInInspector]
    public int m_game_state = 0; //0 loading 1 playing // 2 exit bounds

    public int m_rounds_total;
    public float m_start_delay;
    public float m_end_delay;
    public script_camera m_script_camera;
    public GameObject m_prefab_duck;
    public GameObject m_prefab_wave;
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
            StopAllCoroutines();
        }
    }




    private void Spawn_All_Ducks()
    {
        for (int i = 0; i < m_script_ducks.Length; i++ )
        {
            m_script_ducks[i].m_Instance = Instantiate(m_prefab_duck, m_script_ducks[i].m_SpownPoint.position, m_script_ducks[i].m_SpownPoint.rotation) as GameObject;
            m_script_ducks[i].m_Instance.GetComponent<script_movement>().m_script_wave = Instantiate(m_prefab_wave).GetComponent<script_wave>();
            m_script_ducks[i].m_PlayerNumber = i + 1;
            m_script_ducks[i].Setup(); 
        }
        Round_Reset();
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
        m_game_state = 1;
    }


    public void Exit_Bounds(int player_number)
    {
        script_manager_duck duck_script = m_script_ducks[player_number - 1];
        if(duck_script.m_script_movement.m_movement_active)
        {
            duck_script.OutOfBounds_Push();
            Block_Player_Movement();
            m_game_state = 2;
            StartCoroutine(ZoomWinner(duck_script));

        }

    }

    private IEnumerator ZoomWinner(script_manager_duck duck_script)
    {

        yield return new WaitForSeconds(1.5f);
        duck_script.DisableCamera();
        m_game_state = 3;

        yield return new WaitForSeconds(2f);
        Round_Reset();
    }

    private void Block_Player_Movement()
    {
        foreach (script_manager_duck script in m_script_ducks)
        {
            script.DisableControl();
        }
    }



}
