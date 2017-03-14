using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class script_manager_ui : MonoBehaviour {

    public Text[] m_menu_items;
    public float m_menu_items_pointer = 0;
    public Color m_menu_item_selected_color;
    public GameObject[] m_menu_elements;
    public bool[] m_player_joined;

    private script_manager_game m_manager_game;
    private bool m_menu_active = false;
    private bool m_join_screen = false;


    // Use this for initialization
    void Awake () {

        m_menu_active = true;
        m_manager_game = GameObject.Find("Manager_Game").GetComponent<script_manager_game>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (m_join_screen)
        {
            if (Input.GetButtonDown("Jump_1"))
            {
                menu_join_player(2);
            }
            else if (Input.GetButtonDown("Jump_2"))
            {
                menu_join_player(1);
            }
        }

        if (m_menu_active)
        {
            //move through menu items
            if (Input.GetButtonDown("Vertical_1") || Input.GetButtonDown("Vertical_2"))
            {
                float direction = Input.GetAxis("Vertical_1") + Input.GetAxis("Vertical_2");
                move_menu_items(direction);
            }

            if (Input.GetButtonDown("Jump_1") || Input.GetButtonDown("Jump_2"))
            {
                menu_confirm();
            }
        }

        

        
        
    }

    private void move_menu_items(float direction)
    {
        reset_menu_items();

        direction = direction < 0 ? 1 : -1;
        m_menu_items_pointer += direction;

        if (m_menu_items_pointer < 0)
        {
            m_menu_items_pointer = m_menu_items.Length - 1;
        }
        else if (m_menu_items_pointer > (m_menu_items.Length - 1))
        {
            m_menu_items_pointer = 0;
        }
        Text next_item = m_menu_items[(int)m_menu_items_pointer];
        next_item.color = m_menu_item_selected_color;
        next_item.fontSize = 90;
    }

    private void reset_menu_items()
    {
        foreach ( Text item in m_menu_items )
        {
            item.color = Color.white;
            item.fontSize = 50;
        }
    }

    private void menu_confirm()
    {
        Text selected_menu_item = m_menu_items[(int)m_menu_items_pointer];

        switch (selected_menu_item.text)
        {
            case "battle":
                m_manager_game.Menu_join();
                m_menu_active = false;
                m_join_screen = true;
                for(int i = 1; i<4; i++)
                {
                    m_menu_elements[i].SetActive(false);
                }
                m_menu_elements[6].SetActive(true);

                break;
        }
    }

    private void menu_join_player(int player)
    {
        if (m_player_joined[player - 1] == false)
        {
            m_player_joined[player - 1] = true;
            m_menu_elements[6 + player].SetActive(false);
            m_menu_elements[8 + player].SetActive(true);
            if(m_player_joined[0] == true && m_player_joined[1] == true)
            {
                m_manager_game.Logic_Tree("start versus");
                m_menu_elements[6].SetActive(false);
                m_menu_elements[0].SetActive(false);
            }
        }
        
    }

    public void load_menu()
    {
        //snow panel
        m_menu_elements[11].SetActive(true);
    }






}
