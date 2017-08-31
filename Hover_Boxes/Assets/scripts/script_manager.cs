using UnityEngine;
using UnityEngine.UI;

public class script_manager : MonoBehaviour {

    //vars
    public GameObject m_block_prefab;
    public static float m_block_size;

    //setup
    void Awake()
    {
        //screen orientation
        Screen.orientation = ScreenOrientation.Portrait;

        //block size
        m_block_size = Screen.width / 10;
        m_block_prefab.GetComponent<RectTransform>().sizeDelta = new Vector2(m_block_size, m_block_size);

        //start
        Build_Grid();

    }

    //build block grid
    private void Build_Grid() {

        for (int x = 0; x < Screen.width / m_block_size ; x++)
        {

            for (int y = 1; y <= Screen.height / m_block_size + 1; y++)
            {

                Vector3 position = new Vector3(x * m_block_size, y * m_block_size, 0);
                GameObject block = Instantiate(m_block_prefab, position, Quaternion.identity, gameObject.transform);
                block.GetComponent<Image>().color = Random.ColorHSV(0f, 1f, 0f, 1f, 0.5f, 1f);

            }
        }

    }
}
