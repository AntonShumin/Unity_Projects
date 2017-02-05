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
        position.y = -1f;
        transform.position = position;
        transform.localScale = new Vector3(3, 3, 3);
        m_target_list.Clear();
    }

    void Update()
    {
        transform.localScale += new Vector3(80, 80, 5f) * Time.deltaTime;
        if(transform.localScale.x > 130f)
        {
            gameObject.SetActive(false);
        }
    }

    public void Collider_Event(Collider col)
    {
        GameObject target = col.gameObject;
        if (col.gameObject.tag == "Player")
        {
            //has a movement script
            if (target.GetComponent<script_movement>() != null)
            {
                //if a different player
                if (target.GetComponent<script_movement>().m_PlayerNumber != m_Player_Number)
                {
                    if (!m_target_list.Contains(target))
                    {
                        m_target_list.Add(target);

                        Target_Push(col.gameObject);
                    }


                }
            }
        }
    }

    private void Target_Push(GameObject target)
    {
        Vector3 force_vector = Vector3.Normalize(target.transform.position - m_origin) * 10000000;
        target.GetComponent<Rigidbody>().AddForce(force_vector);
    }


}
