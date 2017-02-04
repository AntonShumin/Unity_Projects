using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_missile : MonoBehaviour {

    [HideInInspector]
    public static script_manager_collector m_object_collector;

    private int m_Player_Number;
    private Vector3 m_Origin;
    private Vector3 m_Direction;
    private bool m_active = false;
    private List<GameObject> m_target_list = new List<GameObject>();



    public void Fire(int player_nr, Vector3 pos_origin, Vector3 direction)
    {
        m_Player_Number = player_nr;
        m_Direction = direction;
        transform.position = pos_origin;

        m_active = true;
        StartCoroutine(Kill_Missile());
    }

    private void Update()
    {
        if (m_active)
        {
            transform.position = transform.position + m_Direction * 100f * Time.deltaTime;
        }
        
    }

    private void OnEnable ()
    {
        m_target_list.Clear();
    }

    IEnumerator Kill_Missile()
    {
        yield return new WaitForSeconds(1);
        m_active = false;
        m_object_collector.Return_Missile(gameObject);
    }

    public void OnTriggerEnter (Collider col)
    {
        GameObject target = col.gameObject;
        if (col.gameObject.tag == "Player" )
        {
            //has a movement script
            if(target.GetComponent<script_movement>() != null)
            {
                //if a different player
                if(target.GetComponent<script_movement>().m_PlayerNumber != m_Player_Number )
                {
                    if(!m_target_list.Contains(target))
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
        Vector3 force = m_Direction * 15000000;
        target.GetComponent<Rigidbody>().AddForce(force);
    }


}
