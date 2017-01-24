using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchRipples : MonoBehaviour {

    public GameObject duckA, duckB;
    public float duckAX, duckAY, duckBX, duckBY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        duckAX = duckA.transform.position.x;
        duckAY = duckA.transform.position.y;
        duckBX = duckB.transform.position.x;
        duckBY = duckB.transform.position.y;


    }
}
