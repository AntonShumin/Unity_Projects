using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class script_score : MonoBehaviour {

    [HideInInspector]
    public GameObject m_panel;
    public GameObject m_prefab_duck;

    private GameObject[] m_ducks_background = new GameObject[5];
    private GameObject[] m_ducks_foreground = new GameObject[5];
    private int m_ducks_shown = 0;

    private Rect m_size_panel;
    private Rect m_size_duck;




	// Use this for initialization
	void Awake () {
        m_panel = gameObject;
        m_size_panel = m_panel.GetComponent<RectTransform>().rect;
        m_size_duck = m_prefab_duck.GetComponent<RectTransform>().rect;
        Build_Panel();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            Panel_Add_Winner(Random.Range(1,3));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Show();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Hide();
        }
    }

    private void Build_Panel()
    {
        //preset vars
        float x;
        int number_of_items = 5;

        //calculations
        float space_between_elements = ( m_size_panel.width - (number_of_items * m_size_duck.width) ) / (number_of_items + 1);

        //instantiate elements
        for ( int i = 0; i < number_of_items; i++  )
        {
            m_ducks_background[i] = Instantiate(m_prefab_duck, m_panel.transform,false); //false to instantiateToWorldSpace
            m_ducks_foreground[i] = Instantiate(m_prefab_duck, m_panel.transform, false);
            x = (-m_size_panel.width / 2) - (m_size_duck.width / 2) + (space_between_elements * (i + 1)) + ( m_size_duck.width * (i+1)) ; //get left edge + first element offset + space between elements + element width
            m_ducks_background[i].GetComponent<RectTransform>().anchoredPosition = m_ducks_foreground[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(x, 0);
            m_ducks_foreground[i].GetComponent<Image>().color = Color.red;
            m_ducks_foreground[i].SetActive(false);
        }
    }

    private void Panel_Add_Winner(int player_number)
    {
        Debug.Log(player_number);
        //preset vars
        GameObject relevant_duck = m_ducks_foreground[m_ducks_shown];
        Color color_duck = Color.black;
        switch(player_number)
        {
            case 1:
                color_duck = Color.blue;
                break;
            case 2:
                color_duck = Color.red;
                break;
        }


        //action
        relevant_duck.GetComponent<Image>().color = color_duck;
        relevant_duck.SetActive(true);
        relevant_duck.transform.DOScale(4, 0.2f).From().SetEase(Ease.OutBack).OnComplete( Shake_Panel );

        //prep next action
        m_ducks_shown++;
    }

    private void Shake_Panel()
    {
        m_panel.transform.DOShakeScale(0.5f,0.2f);
    }

    private void Show()
    {
        Vector3 position = new Vector3(0, 333, 0);
        m_panel.GetComponent<RectTransform>().DOAnchorPos(position, 0.3f).SetEase(Ease.OutBack).OnComplete( Show_Complete );
        m_panel.transform.DOScale(1, 0.2f).SetEase(Ease.OutBack);
        
    }

    private void Show_Complete()
    {
        Panel_Add_Winner(Random.Range(1, 3));
    }

    private void Hide()
    {
        Vector3 position = new Vector3(0, -500, 0);
        m_panel.GetComponent<RectTransform>().DOAnchorPos(position, 0.3f).SetEase(Ease.OutBack);
        m_panel.transform.DOScale(0.35f, 0.2f).SetEase(Ease.OutBack);
    }


}
