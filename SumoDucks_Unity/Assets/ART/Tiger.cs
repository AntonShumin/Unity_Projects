using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : MonoBehaviour {
    float timer;

    public int chance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int num = Random.Range(0, 100);

        if (timer < 2)
        {
            timer = timer + 1 * Time.deltaTime;
        }
        else {
            timer = 0;
            print("LION TIMER");
            if (num < chance) {
                print("Lion Sound");
                AkSoundEngine.PostEvent("tiger", this.gameObject);

            }
        }
	}
}
