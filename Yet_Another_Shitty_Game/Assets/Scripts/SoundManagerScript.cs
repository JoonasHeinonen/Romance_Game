using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip checkPointSound;
    public static AudioClip busStopSound;
    public static AudioClip busHornSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        checkPointSound = Resources.Load<AudioClip> ("checkpoint");
        busStopSound = Resources.Load<AudioClip>("busstop");
        busHornSound = Resources.Load<AudioClip>("bushorn");
        audioSrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "checkpoint":
                Debug.Log("Sound Clip: " + checkPointSound);
                Debug.Log("Audio Source: " + audioSrc);
                audioSrc.PlayOneShot(checkPointSound);
                break;
            case "busstop":
                Debug.Log("Sound Clip: " + checkPointSound);
                Debug.Log("Audio Source: " + audioSrc);
                audioSrc.PlayOneShot(busStopSound);
                break;
            case "bushorn":
                Debug.Log("Sound Clip: " + busHornSound);
                Debug.Log("Audio Source: " + audioSrc);
                audioSrc.PlayOneShot(busHornSound);
                break;
        }
    }
}
