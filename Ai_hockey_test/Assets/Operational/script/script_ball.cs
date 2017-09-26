using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_ball : MonoBehaviour {

    private HockeyAgent m_agent;
    private float m_traveled_distance = 1;
    private Vector3 m_last_position;
    private string m_last_collider = "";
    private float m_time_since_last_hit = 0;
    private Vector3 m_default_position;

    //cached
    private string c_collider_name;


    void Awake()
    {
        m_agent = transform.parent.GetChild(1).GetChild(0).GetComponent<HockeyAgent>();
        m_last_position = transform.position;
        m_default_position = transform.position;

        Time.timeScale = 10f;
    }

    void Update()
    {
        m_traveled_distance +=  Mathf.Abs(Vector3.Distance(m_last_position, transform.position));
        m_last_position = transform.position;

        m_time_since_last_hit += Time.deltaTime * Time.timeScale;

        if (m_time_since_last_hit > 4)
        {
            m_agent.reward = -0.1f;
            //Debug.Log("penaly " + m_time_since_last_hit);
            m_time_since_last_hit = 0f;

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<HockeyAgent>())
        {

            

            if (m_traveled_distance > 0.2f)
            {

                c_collider_name = collision.collider.gameObject.name;
                

                if ( c_collider_name != m_last_collider )
                {
                    //Debug.Log("reward");
                    m_last_collider = c_collider_name;
                    
                    m_agent.reward = 0.1f;
                    m_traveled_distance = 0f;

                }

                m_time_since_last_hit = 0f;

            }

        }
        
    }

    void OnTriggerExit(Collider collider)
    {
        m_agent.reward = -1f;
        m_agent.Reset();
    }

    public void ResetBall()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = m_default_position;
        m_time_since_last_hit = 0f;
        m_last_collider = "";
        m_traveled_distance = 1f;

    }
}
