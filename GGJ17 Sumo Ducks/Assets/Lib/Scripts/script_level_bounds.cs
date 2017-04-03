using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_level_bounds : MonoBehaviour {

    public script_manager_game m_game_manager;

    void Awake()
    {
        m_game_manager = GameObject.Find("Manager_Game").GetComponent<script_manager_game>();
    }

	public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //has a movement script
            if (col.gameObject.GetComponent<script_movement>() != null)
            {
                //pass player number to game manager
                m_game_manager.Exit_Bounds(col);
            }
        }
    }
}
