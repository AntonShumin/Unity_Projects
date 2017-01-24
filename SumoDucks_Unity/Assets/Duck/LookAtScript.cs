using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScript : MonoBehaviour {
    public GameObject lookAt;
    public float damping;

    void Update()
    {
        //Quaternion rotation = Quaternion.LookRotation(lookAt.transform.position - this.transform.position);
        //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * damping);
    }
}
