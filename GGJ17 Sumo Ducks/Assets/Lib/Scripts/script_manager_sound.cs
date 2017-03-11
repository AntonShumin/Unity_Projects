using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_manager_sound : MonoBehaviour {

    public static script_manager_sound m_Instance;
    public AudioSource m_source_fx;
    public AudioSource m_source_voice;
    public AudioClip[] m_sound_kwak;
    public AudioClip[] m_sound_voice;
    public AudioClip[] m_sound_impact;
    public AudioClip[] m_sound_jump;
    public AudioClip[] m_sound_splash;


    // Use this for initialization
    void Awake () {
        m_Instance = GetComponent<script_manager_sound>();
	}

    public void Play_Duck()
    {
        AudioClip kwak = m_sound_kwak[Random.Range(1, m_sound_kwak.Length-1)];
        m_source_fx.PlayOneShot(kwak, 1);
    }

    public void Play_Impact()
    {
        AudioClip sound = m_sound_impact[Random.Range(0, m_sound_impact.Length - 1)];
        m_source_fx.PlayOneShot(sound, 0.2f);
    }

    public void Play_Jump()
    {
        AudioClip sound = m_sound_jump[Random.Range(0, m_sound_jump.Length - 1)];
        m_source_fx.PlayOneShot(sound, 2);
    }

    public void Play_Splash()
    {
        AudioClip sound = m_sound_splash[Random.Range(0, m_sound_splash.Length - 1)];
        m_source_fx.PlayOneShot(sound, 2);
    }

    public void Play_Exit_Bounds()
    {
        AudioClip sound = m_sound_kwak[0];
        m_source_fx.PlayOneShot(sound, 1);
    }

}
