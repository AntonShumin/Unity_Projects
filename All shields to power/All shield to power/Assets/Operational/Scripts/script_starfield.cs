using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_starfield : MonoBehaviour {

    public int m_starsMax = 100;
    public float m_starSize = 1f;
    public float m_starDistance = 10f;
    public float m_starClipDistance = 1f;


    private Transform m_transform;
    private ParticleSystem.Particle[] m_points;
    private float m_starDistanceSqr;
    private float m_starClipDistanceSqr;

   

    void Awake()
    {
        m_transform = transform;
        m_starClipDistanceSqr = m_starClipDistance * m_starClipDistance;
        //GetComponent<ParticleSystem>().SetParticles (points ,points.Length);
    }


    // Update is called once per frame
    void Update () {
		
	}

    private void CreateStars()
    {
        m_points = new ParticleSystem.Particle[m_starsMax];

        for (int i = 0; i < m_starsMax; i++)
        {
            m_points[i].position = Random.insideUnitSphere * m_starDistance + m_transform.position;
            m_points[i].color = new Color(1, 1, 1, 1);
            m_points[i].size = m_starSize;
        }

        GetComponent<ParticleSystem>().SetParticles(m_points, m_points.Length);
    }
}
