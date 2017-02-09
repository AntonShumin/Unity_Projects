using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private int m_rounds_current = 0;
    private script_manager_duck m_RoundWinner;
    private script_manager_duck m_GameWinner;
    private script_manager_collector m_ObjectCollector;

    private void Start()
    {
        m_ObjectCollector = GameObject.Find("Object Collector").GetComponent<script_manager_collector>();
        Spawn_All_Ducks();
        Set_Camera_Targets();
        Start_Versus();
    }


    void Update()
    {
        if (Input.GetKeyDown("return") || Input.GetKeyDown("enter"))
        {
            StopAllCoroutines();
            //Next_Round();
        }
    }

    private void Start_Versus()
    {
        m_rounds_current = 0;
        foreach(script_manager_duck duck in m_script_ducks)
        {
            duck.m_Lives = 3; 
        }
        Next_Round();
        Update_Lives_UI();

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


    public void Exit_Bounds(int player_number)
    {
        script_manager_duck duck_script = m_script_ducks[player_number - 1];
        if(duck_script.m_script_movement.m_movement_active)
        {
            duck_script.OutOfBounds_Push();
            Block_Player_Movement();
            m_game_state = 2;
            int lives_left = m_script_ducks[player_number - 1].Lose_Life();
            Update_Lives_UI();
            if(lives_left < 1)
            {
                End_Round(player_number);
            } else
            {
                StartCoroutine(ZoomWinner(duck_script));
            }

        }

    }

    private IEnumerator ZoomWinner(script_manager_duck duck_script)
    {

        yield return new WaitForSeconds(1f);
        duck_script.DisableCamera();
        m_game_state = 3;
        Show_Winner(Mathf.Abs(duck_script.m_PlayerNumber - 3));

        yield return new WaitForSeconds(2f);
        m_ObjectCollector.m_UI[1].SetActive(false);
        Next_Round();
    }

    private void Show_Winner(int winner)
    {
        string round_message = m_script_ducks[winner-1].m_ColoredPlayerText  + " duck scores!";
        m_ObjectCollector.m_UI[1].GetComponent<Text>().text = round_message;
        m_ObjectCollector.m_UI[1].SetActive(true);
    }

    private void Block_Player_Movement()
    {
        foreach (script_manager_duck script in m_script_ducks)
        {
            script.DisableControl();
        }
    }

    private void Next_Round()
    {
        Debug.Log("add");
        Round_Reset();
        m_game_state = 0;
        m_rounds_current++;
        string round_message = "Round <color=#ffa500ff>" + m_rounds_current + "</color>";
        m_ObjectCollector.m_UI[0].GetComponent<Text>().text = round_message;
        m_ObjectCollector.m_UI[0].SetActive(true);
        StartCoroutine(Next_Round_Wait());
    }

    private IEnumerator Next_Round_Wait()
    {
        yield return new WaitForSeconds(2);
        m_game_state = 1;
        m_ObjectCollector.m_UI[0].SetActive(false);

    }

    private void End_Round(int loser)
    {
        Start_Versus();
    }

    private void Update_Lives_UI()
    {

        foreach (script_manager_duck script in m_script_ducks)
        {
            script.m_Score_Text.gameObject.SetActive(true);
            script.m_Score_Text.text = script.Get_Lives();
        }
    }



}
