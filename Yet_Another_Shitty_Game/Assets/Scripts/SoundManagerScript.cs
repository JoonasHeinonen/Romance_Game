using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip checkPointSound;
    public static AudioClip busStopSound;
    public static AudioClip busHornSound;
    public static AudioClip coinSound;
    public static AudioClip questItemSound;
    public static AudioClip questCompleteSound;
    public static AudioClip menuItemSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        checkPointSound = Resources.Load<AudioClip> ("checkpoint");
        busStopSound = Resources.Load<AudioClip>("busstop");
        busHornSound = Resources.Load<AudioClip>("bushorn");
        coinSound = Resources.Load<AudioClip>("coin");
        questItemSound = Resources.Load<AudioClip>("questitem");
        questCompleteSound = Resources.Load<AudioClip>("questcomplete");
        menuItemSound = Resources.Load<AudioClip>("menu item");

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
            case "menuitem":
                Debug.Log("Sound Clip: " + menuItemSound);
                Debug.Log("Audio Source: " + audioSrc);
                audioSrc.PlayOneShot(menuItemSound);
                break;
            case "coin":
                Debug.Log("Sound Clip: " + coinSound);
                Debug.Log("Audio Source: " + audioSrc);
                audioSrc.PlayOneShot(coinSound);
                break;
            case "questitem":
                Debug.Log("Sound Clip: " + questItemSound);
                Debug.Log("Audio Source: " + audioSrc);
                audioSrc.PlayOneShot(questItemSound);
                break;
            case "questcomplete":
                Debug.Log("Sound Clip: " + questCompleteSound);
                Debug.Log("Audio Source: " + audioSrc);
                audioSrc.PlayOneShot(questCompleteSound);
                break;
        }
    }
}
