using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Waves : MonoBehaviour {
    [Header("General info")]
    public GameObject controller;
    public int playerNum;
    public GameObject projectilePrefab;
    public List<Transform>projectileSpawner;
    public GameObject dummyRotator;

    [Header("Waves")]
    public float waveStrength;
    public float waveValue, pastWaveValue;
    public bool wave, waveAllowed;
    public float waveTimer, timeBetweenWaves;

    [Header("Shoot timer and stuff")]
    public bool shoot;
    public float shootTimer;

    [Header("Making Waves Animation")]
    public float angle;
    public float angleOffset;
    public float angleSpeed;

    [Header("Turning Motion")]
    public float damping;

    [Header("AudioSources")]
    public AudioSource audioSource;

    // Use this for initialization
    void Start () {
        AkSoundEngine.PostEvent("fs_heavy", this.gameObject);

        waveAllowed = true;
        playerNum = controller.GetComponent<Movement>().playerNum;
	}
	
	// Update is called once per frame
	void Update () {
         waveValue = Input.GetAxis("Player_" + playerNum + "_Wave");


        if (waveValue < 0 && angle < angleOffset)
        {
            angle = angle + angleSpeed * Time.deltaTime;
            wave = true;

        }
        else if (waveValue > 0 && angle > -angleOffset)
        {
            angle = angle - angleSpeed * Time.deltaTime;
            wave = true;

        }
        else {
            wave = false;
            if (angle < 0 && waveValue == 0)
                angle = angle + angleSpeed * Time.deltaTime;
            if (angle > 0 && waveValue == 0)
                angle = angle - angleSpeed * Time.deltaTime;
        }

        if ((Input.GetButtonDown("Player_" + playerNum + "_LeanLeft") || Input.GetButtonDown("Player_" + playerNum + "_LeanRight")) && waveTimer > timeBetweenWaves)
        {

            GetComponentInParent<ParentDuck>().children[4].GetComponent<Animator>().SetTrigger("attack");

            waveTimer = 0f;
            wave = true;

            AkSoundEngine.PostEvent("kwak", this.gameObject);
            AkSoundEngine.PostEvent("wave_light", this.gameObject);

            foreach (Transform p in projectileSpawner)
            {
                GameObject projectile = (GameObject)Instantiate(projectilePrefab, p.position, p.rotation);
                projectile.transform.parent = this.transform.parent;
                projectile.GetComponent<Projectile>().generatedFrom = this.gameObject;
            }
        }
        else if (waveTimer < timeBetweenWaves){
            waveTimer = waveTimer + 1 * Time.deltaTime;
            wave = false;
        }

        if (angle < 0.1f && angle > -0.1f) {
            waveAllowed = true;
        } 

        this.transform.localEulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, (angle * -1 )* waveStrength);

        if (controller.GetComponent<DucklingsGenerator>().ducklings.Count > 0)
        {
            foreach (GameObject d in controller.GetComponent<DucklingsGenerator>().ducklings) {
                
                d.transform.localEulerAngles = new Vector3(d.transform.eulerAngles.x, d.transform.eulerAngles.y, angle * angleSpeed);
            }
        }

        Quaternion rotation = Quaternion.LookRotation(dummyRotator.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * damping);
    }
}
