using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip jump, swing, nextLevel, coin;
    static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        coin = Resources.Load<AudioClip>("coin");
        jump = Resources.Load<AudioClip>("jump");
        swing = Resources.Load<AudioClip>("swing");
        nextLevel = Resources.Load<AudioClip>("nextLevel");

        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "jump":
                audioSource.PlayOneShot(jump);
                break;
            case "swing":
                audioSource.PlayOneShot(swing);
                break;
            case "nextLevel":
                audioSource.PlayOneShot(nextLevel);
                break;
            case "coin":
                audioSource.PlayOneShot(coin);
                break;
        }
    }
}
