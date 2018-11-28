﻿using UnityEngine.Audio;
using System;
using UnityEngine;

// To trigger a sound, use the line of code below in any script
// FindObjectOfType<AudioManager>().Play("");

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static AudioManager instance;

	// Use this for initialization
	void Awake () {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

		foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (CheckForMultiple(name))
        {
            Play(PickRandomSound(name));
            return;
        }
        else if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    // Use this to stop clips before they finish or to stop looping music
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Stop();
    }

    // There are some names like Attack that have multiple sounds assocatied with it
    public bool CheckForMultiple(string name)
    {
        switch (name)
        {
            case "Attack":
                return true;
            default:
                return false;
        }
    }

    // Receives a psuedo name and returns a specfic sound name
    public string PickRandomSound(string name)
    {
        switch (name)
        {
            case "Attack":
                switch (UnityEngine.Random.Range(0, 2))
                {
                    case 0:
                        return "Attack1";
                    case 1:
                        return "Attack2";
                    default:
                        Debug.LogWarning("Range Error in PickRandomSound(Attack)");
                        break;
                }
                break;
            default:
                return null;
        }
        return null;
    }
}
