using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {

    public GameObject toFollow;
    public float damping;
    public float distance;
    public float dist;
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SmoothLookAt();
	}

    void SmoothLookAt() {

        dist = Vector3.Distance(this.transform.position, toFollow.transform.position);

        if (dist > distance)
        {
            GetComponent<Rigidbody>().velocity = (transform.forward * dist * speed);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        }

        Quaternion rotation = Quaternion.LookRotation(toFollow.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * damping);
    }
}
