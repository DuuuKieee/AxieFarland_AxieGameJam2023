using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager instance;
    public Sound[] musicSound, sfxSound;
    public AudioSource musicSource, sfxSource;
    void Awake()
    {
        instance = this;
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");

        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
}
