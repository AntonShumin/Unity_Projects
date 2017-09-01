using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class script_block : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //vars
    private Vector3 m_position_default;
    private RectTransform m_rectTransform;

    //cached
    private Vector3 c_position_desired = new Vector3(0,0,0);
    private Vector3 c_position_start = new Vector3(0, 0, 0);
    private Vector2 c_scale_desired = new Vector2(0, 0);
    private Vector2 c_scale_start = new Vector2(0, 0);
    private float c_running_time;
    private float c_time_desired;

    void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_position_default = m_rectTransform.transform.position; //store default position
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(Scale(1));

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(Scale(2));
    }

    IEnumerator Scale(int type)
    {

        switch (type)
        {
            case 1: //bigger
                c_time_desired = 0.05f;
                c_scale_desired.x = c_scale_desired.y = script_manager.m_block_size * 2;
                c_position_desired.x = m_position_default.x - script_manager.m_block_size / 2;
                c_position_desired.y = m_position_default.y + script_manager.m_block_size / 2;
                m_rectTransform.SetAsLastSibling(); //max z index
                break;
            case 2: //smaller
                c_time_desired = 0.5f;
                c_scale_desired.x = c_scale_desired.y = script_manager.m_block_size;
                c_position_desired.x = m_position_default.x;
                c_position_desired.y = m_position_default.y;
                break;
            default: //fallback
                yield break;
        }

        //preset vars
        c_running_time = 0f;
        c_scale_start = m_rectTransform.sizeDelta;
        c_position_start = m_rectTransform.position;

        //interate
        do
        {
            m_rectTransform.sizeDelta = Vector2.Lerp(c_scale_start, c_scale_desired, c_running_time / c_time_desired);
            m_rectTransform.transform.position = Vector3.Lerp(c_position_start, c_position_desired, c_running_time / c_time_desired);
            c_running_time += Time.deltaTime;
            yield return null;

        } while (c_running_time <= c_time_desired);
    }

}
