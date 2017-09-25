using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_ball : MonoBehaviour {

    private HockeyAgent m_agent;
    private float m_traveled_distance = 1;
    private Vector3 m_last_position;
    private Vector3 m_last_direction;

    //cached
    private Vector3 c_diff_vector;

    void Awake()
    {
        m_agent = transform.parent.GetChild(1).GetChild(0).GetComponent<HockeyAgent>();
        m_last_position = transform.position;
    }

    void Update()
    {
        m_traveled_distance +=  Mathf.Abs(Vector3.Distance(m_last_position, transform.position));
        m_last_position = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<HockeyAgent>())
        {

            c_diff_vector = transform.position - collision.gameObject.transform.position;
            c_diff_vector.y = 0;
            Debug.Log(c_diff_vector);


            if (m_traveled_distance > 0.1f)
            {
                

                if( Vector3.Dot(m_last_direction, c_diff_vector) < 0 )
                {
                    Debug.Log(Vector3.Dot(m_last_direction, GetComponent<Rigidbody>().velocity));
                }
                m_traveled_distance = 0f;

            }

            m_last_direction = c_diff_vector;

        }
        
    }
}
