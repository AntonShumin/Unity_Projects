using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_panel : MonoBehaviour {

    //public
    public GameObject m_block_prefab;
    public static float m_block_size;

    //cached
    private Vector3 c_position;

	void Awake()
    {
        float frame_width = gameObject.GetComponent<RectTransform>().rect.width;
        float frame_height = gameObject.GetComponent<RectTransform>().rect.height;
        m_block_size = frame_width / 10;
        m_block_prefab.GetComponent<RectTransform>().sizeDelta = new Vector2(m_block_size,m_block_size);

        Debug.Log(m_block_size);

        for (int x = 0; x <1; x++ )
        {

            for (int y = 0; y < 1; y++)
            {
                Debug.Log(y * m_block_size);
                c_position = new Vector3(x * m_block_size, 0, 0);
                GameObject block = Instantiate(m_block_prefab, c_position, Quaternion.identity, gameObject.transform);

            }
        }
    }
}
