using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class script_camera : MonoBehaviour {

    public float m_DampTime = 0.2f;
    public float m_ScreenEdgeBuffer = 4f;
    public float m_MinSize = 6.5f;
    [HideInInspector]
    public Transform[] m_Targets;


    private Camera m_Camera;
    private float m_ZoomSpeed;
    private Vector3 m_MoveVelocity;
    private Vector3 m_DesiredPosition;

    private int m_camera_state = 0;
    // 0 game
    // 1 menu rotate


    private void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();
    }


    private void FixedUpdate()
    {
        if(m_camera_state == 0)
        {
            Move();
            Zoom();
        } else if (m_camera_state == 1)
        {
            transform.RotateAround(transform.position, Vector3.up, 0.5f * Time.deltaTime);
        }
        
    }

    public void Set_Camera_State(string state)
    {
        switch(state)
        {
            case "menu":
                m_camera_state = 1;
                m_Camera.transform.localPosition = new Vector3(0, -59, -118 );
                m_Camera.transform.localEulerAngles = new Vector3(-11, 0, 0);
                break;
            case "joining":
                m_camera_state = 1;
                m_Camera.transform.DOLocalMove(new Vector3(0, 55, -100), 0.5f);
                m_Camera.transform.DOLocalRotate(new Vector3(30, 0, 10), 0.5f,RotateMode.Fast);
                //.transform.localEulerAngles = new Vector3(30, 0, 10);
                break;
            case "battle":
                m_camera_state = 0;
                transform.DOLocalRotate(new Vector3(40, 0, 0), 0.5f, RotateMode.Fast);
                m_Camera.transform.DOLocalMove(new Vector3(0, -10, -100), 0.5f);
                m_Camera.transform.DOLocalRotate(new Vector3(5, 0, 0), 0.5f, RotateMode.Fast);
                break;
        }
    }


    private void Move()
    {
        FindAveragePosition();

        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }


    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;

        for (int i = 0; i < m_Targets.Length; i++)
        {
            if (!m_Targets[i].GetComponent<script_movement>().m_camera_active)
                continue;

            averagePos += m_Targets[i].position;
            numTargets++;
        }

        if (numTargets > 0)
            averagePos /= numTargets;

        averagePos.y = transform.position.y;

        m_DesiredPosition = averagePos;
    }


    private void Zoom()
    {
        float requiredSize = FindRequiredSize();
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
    }


    private float FindRequiredSize()
    {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);

        float size = 0f;

        for (int i = 0; i < m_Targets.Length; i++)
        {
            if (!m_Targets[i].GetComponent<script_movement>().m_camera_active)
                continue;

            Vector3 targetLocalPos = transform.InverseTransformPoint(m_Targets[i].position);

            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
        }

        size += m_ScreenEdgeBuffer;

        size = Mathf.Max(size, m_MinSize);

        return size;
    }


    public void SetStartPositionAndSize()
    {
        FindAveragePosition();

        transform.position = m_DesiredPosition;

        m_Camera.orthographicSize = FindRequiredSize();
    }
}
