using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_boundaries : MonoBehaviour {

    public manager_Game m_game_manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("enter player " + col.name);
    }
    void OnTriggerExit(Collider col)
    {

        //col.GetComponent
        
        if (col.name == "Duck_1")
        {
        
            
            //trigger lose event
            int player_number = col.GetComponent<Movement>().playerNum;
            m_game_manager.exit_ring(player_number);

            //trigger gravity
            col.transform.parent.GetComponent<ParentDuck>().turn_on_gravity();

        }
        
        

        /*
        int number = col.GetComponent<temp_duck>().m_player_number;
        m_game_manager.exit_ring(number);
        */

    }
}
