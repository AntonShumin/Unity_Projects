using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPosScript : MonoBehaviour {

    public GameObject toMatch;

	// Update is called once per frame
	void Update () {
        this.transform.position = toMatch.transform.position;
	}
}
