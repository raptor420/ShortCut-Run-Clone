using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource sourceBGM;
    [SerializeField] AudioSource source2;

    [SerializeField ]public  AudioClip pickup;
    [SerializeField] public AudioClip splash;
    [SerializeField] public AudioClip victory;
    [SerializeField] public AudioClip startRace;
    [SerializeField] public AudioClip jump;
    [SerializeField] public AudioClip build;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void PlayAudio(AudioClip clip)
    {
        source2.PlayOneShot(clip);
    }
}
