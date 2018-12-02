using UnityEngine.Audio;
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

    // Set a sound clip's volume to zero
    public void Mute(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found on Mute");
            return;
        }
        s.source.volume = 0.0f;
    }

    // Reset a sound clip's volume
    public void Unmute(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found on Mute");
            return;
        }
        s.source.volume = s.volume;
    }

    // There are some names like Attack that have multiple sounds assocatied with it
    private bool CheckForMultiple(string name)
    {
        switch (name)
        {
            case "Attack":
                return true;
            case "Jump":
                return true;
            case "Woof":
                return true;
            case "Dash":
                return true;
            default:
                return false;
        }
    }

    // Receives a psuedo name and returns a specfic sound name
    private string PickRandomSound(string name)
    {
        switch (name)
        {
            case "Attack":
                switch (UnityEngine.Random.Range(0, 2))
                {
                    case 0:
                        return "Attack0";
                    case 1:
                        return "Attack1";
                    default:
                        Debug.LogWarning("Range Error in PickRandomSound(" + name + ")");
                        break;
                }
                break;

            case "Jump":
                switch (UnityEngine.Random.Range(0, 4))
                {
                    case 0:
                        return "Jump0";
                    case 1:
                        return "Jump1";
                    case 2:
                        return "Jump2";
                    case 3:
                        return "Jump3";
                    default:
                        Debug.LogWarning("Range Error in PickRandomSound(" + name + ")");
                        break;
                }
                break;

            case "Woof":
                switch (UnityEngine.Random.Range(0, 3))
                {
                    case 0:
                        return "WoofLow";
                    case 1:
                        return "WoofMid";
                    case 2:
                        return "WoofHigh";
                    default:
                        Debug.LogWarning("Range Error in PickRandomSound(" + name + ")");
                        break;
                }
                break;

            case "Dash":
                switch (UnityEngine.Random.Range(0, 2))
                {
                    case 0:
                        return "Dash0";
                    case 1:
                        return "Dash1";
                    default:
                        Debug.LogWarning("Range Error in PickRandomSound(" + name + ")");
                        break;
                }
                break;

            default:
                Debug.LogWarning("Name Error in PickRandomSound(" + name + ")");
                return null;
        }
        return null;
    }
}
