using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manager_UI : MonoBehaviour {

    public GameObject[] m_elements_ui;

	public void snow_text_start(bool show)
    {

        m_elements_ui[0].SetActive(show);
        m_elements_ui[1].SetActive(show);

    }

    public void show_text_playerWin(bool show, int player)
    {

        int winner = Mathf.Abs(player - 3);

        if(player > 0 )
        {
            m_elements_ui[2].GetComponent<Text>().text = "Player " + winner + " wins";
        }
        
        m_elements_ui[2].SetActive(show);
        m_elements_ui[1].SetActive(show);

        if(player == 0)
        {
            snow_text_start(true);
        }
    }
}
