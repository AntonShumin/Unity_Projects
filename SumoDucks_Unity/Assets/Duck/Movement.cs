using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [Header("General Info")]
    public int playerNum;

    public float movSpeed;
    public Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        rb.velocity = new Vector3(Input.GetAxis("Player_" + playerNum + "_Horizontal") * movSpeed, rb.velocity.y, Input.GetAxis("Player_" + playerNum + "_Vertical") * movSpeed);

        if(Input.GetAxis("Player_" + playerNum + "_Horizontal") != 0 || Input.GetAxis("Player_" + playerNum + "_Vertical") != 0 )
        {
            GetComponentInParent<ParentDuck>().children[4].GetComponent<Animator>().SetBool("bool_walk", true);
        } else
        {
            GetComponentInParent<ParentDuck>().children[4].GetComponent<Animator>().SetBool("bool_walk", false);
        }


            //GetComponentInParent<ParentDuck>().children[4].GetComponent<Animator>().SetTrigger("attack");
    }
}