using Borodar.FarlandSkies.LowPoly;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class script_manager_game : MonoBehaviour {

    [HideInInspector]
    public int m_game_state = 0; //0 loading 1 playing 2 exit bounds

    public int m_rounds_total;
    public float m_start_delay;
    public float m_end_delay;
    public script_camera m_script_camera;
    public GameObject m_prefab_duck;
    public GameObject m_prefab_wave;
    public script_manager_duck[] m_script_ducks;
    public int[] m_versus_score = new int[5];

    private int m_rounds_current = 0;
    private script_manager_duck m_RoundWinner;
    private script_manager_duck m_GameWinner;
    private script_manager_collector m_ObjectCollector;
    private script_manager_ui m_manager_ui;
    private float m_daytime_factor = 0;

    private void Start()
    {
        Cursor.visible = false;
        //setup objects
        m_ObjectCollector = GameObject.Find("Object Collector").GetComponent<script_manager_collector>();
        m_manager_ui = GameObject.Find("Manager_Ui").GetComponent<script_manager_ui>();
        script_movement.m_particle_manager = GameObject.Find("Manager_Particles").GetComponent<script_manager_particles>();
        Spawn_All_Ducks();
        Set_Camera_Targets();

        //start game sequence
        m_manager_ui.load_menu();
        m_script_camera.Set_Camera_State("menu");

    }


    void Update()
    {
        if (Input.GetKeyDown("return") || Input.GetKeyDown("enter"))
        {
            StopAllCoroutines();
        }

        if(m_daytime_factor != 0 )
        {
            SkyboxDayNightCycle.Instance.TimeOfDay += (Time.deltaTime / m_daytime_factor) * 100f;
        }
        
    }

    //delay:   Invoke("LaunchProjectile", 2);
    /************************************
     *******Central Event Logic********** 
     ***********************************/
    public void Logic_Tree(string event_name, int param1 = 0)
    {
        switch (event_name)
        {

            case "start versus":

                
                m_rounds_current = 0;
                foreach (script_manager_duck duck in m_script_ducks)
                {
                    duck.m_Lives = 3;
                }
                Logic_Tree("next round");
                Update_Lives_UI();
                break;

            case "round reset":

                foreach (script_manager_duck script in m_script_ducks)
                {
                    //reset position
                    script.Reset();
                }
                break;

            case "next round":

                
                Logic_Tree("round reset");
                m_game_state = 0;
                m_rounds_current++;
                string round_message = "Round <color=#ffa500ff>" + m_rounds_current + "</color>";
                m_ObjectCollector.m_UI[0].GetComponent<Text>().text = round_message;
                m_ObjectCollector.m_UI[0].SetActive(true);
                StartCoroutine(Next_Round_Wait());
                m_script_camera.Set_Camera_State("battle prep");

                break;

            case "next round wait":

                m_script_camera.Set_Camera_State("battle");
                Show_Lives(true);
                m_game_state = 1;
                m_ObjectCollector.m_UI[0].SetActive(false);

                break;

            case "":
                break;


        }
    }
    //---------END EVENT LOGIC-----------






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



    public void Exit_Bounds(int player_number)
    {
        if(m_game_state == 1)
        {
            //Play Sound
            script_manager_sound.m_Instance.Play_Exit_Bounds();
            script_manager_duck duck_script = m_script_ducks[player_number - 1];
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
        Logic_Tree("next round");
    }

    private void Show_Winner(int winner)
    {
        Show_Lives(false);
        string round_message = m_script_ducks[winner-1].m_ColoredPlayerText  + " duck scores!";
        m_script_camera.Set_Camera_State("round winner");
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


    private IEnumerator Next_Round_Wait()
    {
        yield return new WaitForSeconds(2);
        Logic_Tree("next round wait");

    }

    private void End_Round(int loser)
    {
        Logic_Tree("start versus");
    }

    private void Update_Lives_UI()
    {

        foreach (script_manager_duck script in m_script_ducks)
        {
            script.m_Score_Text.text = script.Get_Lives();
        }
    }

    private void Show_Lives(bool b_show)
    {
        foreach (script_manager_duck script in m_script_ducks)
        {
            script.m_Score_Text.gameObject.SetActive(b_show);
        }
    }

    public void Menu_join()
    {
        m_script_camera.Set_Camera_State("joining");
    }



}
