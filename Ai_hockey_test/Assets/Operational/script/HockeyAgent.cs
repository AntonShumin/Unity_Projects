using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HockeyAgent : Agent {

    private Transform m_ball;
    private Rigidbody m_ball_rd;
    private Transform m_stick;
    private Rigidbody m_stick_rb;

    //cached
    private Vector3 c_distance;

    void Awake()
    {
        m_ball = GameObject.Find("TennisBall").transform;
        m_ball_rd = m_ball.GetComponent<Rigidbody>();

        m_stick = transform.Find("/collider").transform;
        m_stick_rb = m_stick.GetComponent<Rigidbody>();
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

        //move x

        //move y

        //move z

        //rotate x

        //rotate y

        //rotate z

	}

	public override void AgentReset()
	{

	}

	public override void AgentOnDone()
	{

	}
}
