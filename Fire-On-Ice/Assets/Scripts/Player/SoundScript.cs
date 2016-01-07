using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {
    public AudioSource aSource;
    public AudioClip[] aClips;

    public float maxPitch;
    public float minPitch;
    public float maxVol;
    public float minVol;


    void Start()
    {
        if (aSource == null)
        {
            aSource = GetComponent<AudioSource>();
        }
    }

    public void setRandomPitch()
    {
        aSource.pitch = Random.Range(minPitch, maxPitch);
    }

    public void setRandomVolume()
    {
        aSource.volume = Random.Range(minVol, maxVol);
    }

    public void setRandomAudioClip()
    {
        aSource.clip = aClips[Random.Range(0, aClips.Length)];
    }

    public void playSound()
    {
        aSource.Stop();
        aSource.Play();
    }

    public void playRandomSound()
    {
        aSource.Stop();
        setRandomAudioClip();
        setRandomPitch();
        setRandomVolume();
        aSource.Play();
    }

    public void stopSound()
    {
        aSource.Stop();
    }
}
