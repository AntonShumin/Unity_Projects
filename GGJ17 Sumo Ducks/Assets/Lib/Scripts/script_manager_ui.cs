using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class script_manager_ui : MonoBehaviour {

    public Text[] m_menu_items;
    public bool m_menu_active = false;
    public float m_menu_items_pointer = 0;
    public Color m_menu_item_selected_color;


	// Use this for initialization
	void Start () {

        m_menu_active = true;
		
	}
	
	// Update is called once per frame
	void Update () {
        \
        if(m_menu_active)
        {
            //move through menu items
            if (Input.GetButtonDown("Vertical_1") || Input.GetButtonDown("Vertical_2"))
            {
                float direction = Input.GetAxis("Vertical_1") + Input.GetAxis("Vertical_2");
                move_menu_items(direction);
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


}
