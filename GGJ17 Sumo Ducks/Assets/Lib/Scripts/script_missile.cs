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

    IEnumerator Kill_Missile()
    {
        yield return new WaitForSeconds(1);
        m_active = false;
        m_object_collector.Return_Missile(gameObject);
    }


}
