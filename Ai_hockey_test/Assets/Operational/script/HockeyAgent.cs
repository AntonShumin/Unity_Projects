using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HockeyAgent : Agent {

    private Transform m_ball;
    private Rigidbody m_ball_rd;
    private Transform m_stick;
    private Rigidbody m_stick_rb;
    private float m_movement_multiplier = 2f;
    private Vector3 m_default_position;
    private Quaternion m_default_rotation;

    //cached
    private Vector3 c_distance;
    private Vector3 c_velocity = Vector3.zero;
    private Vector3 c_rotation = Vector3.zero;

    void Awake()
    {
        m_ball = transform.parent.parent.GetChild(0).transform;
        m_ball_rd = m_ball.GetComponent<Rigidbody>();
        

        m_stick = transform.GetChild(0).transform;
        m_stick_rb = gameObject.GetComponent<Rigidbody>();

        m_default_position = transform.position;
        m_default_rotation = transform.rotation;
    }

	public override List<float> CollectState()
	{
		List<float> state = new List<float>();

        //position ball
        state.Add(m_ball.position.x);
        state.Add(m_ball.position.y);
        state.Add(m_ball.position.z);


        //velocity ball 
        state.Add(m_ball_rd.velocity.x);
        state.Add(m_ball_rd.velocity.y);
        state.Add(m_ball_rd.velocity.z);

        //position stick
        state.Add(m_stick.position.x);
        state.Add(m_stick.position.y);
        state.Add(m_stick.position.z);


        //rotation stick
        state.Add(m_stick.rotation.x);
        state.Add(m_stick.rotation.y);
        state.Add(m_stick.rotation.z);

        //velocity stick
        state.Add(m_stick_rb.velocity.x);
        state.Add(m_stick_rb.velocity.y);
        state.Add(m_stick_rb.velocity.z);

        //distance between stick collider and ball
        state.Add(m_ball.position.x - m_stick.position.x);
        state.Add(m_ball.position.x - m_stick.position.y);
        state.Add(m_ball.position.x - m_stick.position.z);

        return state;

	}

	public override void AgentStep(float[] act)
	{

        //limit values
        for (int i = 0; i < act.Length; i++)
        {
            act[i] = Mathf.Max(act[i], -1);
            act[i] = Mathf.Min(act[i], 1);
        }

        //move x
        c_velocity.x = act[0];

        //move y
        c_velocity.y = act[1];

        //move z
        c_velocity.z = act[2];

        //rotate x
        c_rotation.x = act[3];

        //rotate y
        c_rotation.y = act[4];

        //rotate z
        c_rotation.z = act[5];


        //move stick
        m_stick_rb.velocity = c_velocity * m_movement_multiplier;
        m_stick_rb.angularVelocity = c_rotation;

    }

    void OnTriggerExit(Collider collider)
    {
        reward = -1f;
       Reset();
    }

    public override void AgentReset()
	{
        m_ball.GetComponent<script_ball>().ResetBall();
        ResetStick();
    }

    /*
	public override void AgentOnDone()
	{

	}
    */

    public void ResetStick()
    {
        m_stick_rb.velocity = Vector3.zero;
        m_stick_rb.angularVelocity = Vector3.zero;

        transform.position = m_default_position;
        transform.rotation = m_default_rotation;

    }
}
