using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AkSoundEngine.PostEvent("music_play", this.gameObject);

    }
}
