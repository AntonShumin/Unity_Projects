using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentDuck : MonoBehaviour {
    public bool destroyDuck;
    public GameObject water;

    public GameObject[] children = new GameObject[4];


    void Awake() {
        water = GameObject.FindGameObjectWithTag("Water");

    }


    public void DestroyDuck() {
        Destroy(this.gameObject);
    }


    void Update() {
        if (destroyDuck)
        DestroyDuck();

        if (this.GetComponentInChildren<Movement>().playerNum == 1) {
            water.GetComponent<water_script>().srcx1 = children[1].transform.position.x;
            water.GetComponent<water_script>().srcy1 = children[1].transform.position.z;
        }

        if (this.GetComponentInChildren<Movement>().playerNum == 2)
        {
            water.GetComponent<water_script>().srcx2 = children[1].transform.position.x;
            water.GetComponent<water_script>().srcy2 = children[1].transform.position.z;
        }

        if (children[1].GetComponent<Movement>().playerNum == 1) {
            GameObject.FindGameObjectWithTag("Water").GetComponent<water_script>().player1Wave = children[0].GetComponent<Waves>().wave;
        }

        if (children[1].GetComponent<Movement>().playerNum == 2)
        {
            GameObject.FindGameObjectWithTag("Water").GetComponent<water_script>().player2Wave = children[0].GetComponent<Waves>().wave;
        }


    }

    public void ExitArena() {

    }

    public void turn_on_gravity()
    {
        children[1].GetComponent<Rigidbody>().constraints = 0;
        foreach (Transform d in children[2].GetComponentInChildren<Transform>())
        {
            //children[2].GetComponentInChildren<Rigidbody>().constraints = 0;
            d.GetComponent<Rigidbody>().constraints = 0;
        }
    }

    public void SetDucksInWater() {

    }
}
