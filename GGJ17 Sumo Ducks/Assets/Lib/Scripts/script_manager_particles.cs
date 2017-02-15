using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_manager_particles : MonoBehaviour {

    public GameObject[] m_cfx_type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void cfx_spown(int type, Vector3 position)
    {
        GameObject instance = CFX_SpawnSystem.GetNextObject(m_cfx_type[type]);
        //instance.transform.position = position;
    }
}
