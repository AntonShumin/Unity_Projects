using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_wave : MonoBehaviour {

    private int m_Player_Number;
    private List<GameObject> m_target_list = new List<GameObject>();
    private int m_speed = 5;
    private Vector3 m_origin;

    public void Start_Wave(int player_number, Vector3 position)
    {
        m_Player_Number = player_number;
        m_origin = position;
        position.y = 0.4f;
        transform.position = position;
        transform.localScale = new Vector3(3, 3, 10);
    }

    void Update()
    {
        transform.localScale += new Vector3(50, 50, 10f) * Time.deltaTime;
        if(transform.localScale.x > 100f)
        {
            gameObject.SetActive(false);
        }
    }


}
