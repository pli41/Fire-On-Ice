using UnityEngine;
using System.Collections;

public class MechanimSoundEffect : MonoBehaviour {
    public bool playSound;
    public bool stopSound;

    AudioSource aSource;

    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    void playAudioClip()
    {
        playSound = false;
        aSource.Stop();
        aSource.Play();
    }

    void stopAudioClip()
    {
        stopSound = false;
        aSource.Stop();
    }

}
