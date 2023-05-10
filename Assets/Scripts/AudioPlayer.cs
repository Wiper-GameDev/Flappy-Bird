using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip Hit;
    [SerializeField] AudioClip Die;
    [SerializeField] AudioClip Point;
    [SerializeField] AudioClip Swoosh;
    [SerializeField] AudioClip Wing;

    AudioSource audioSource;

    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHit(){
        audioSource.PlayOneShot(Hit);
    }

    public void PlayDie(){
        audioSource.PlayOneShot(Die);
    }

    public void PlayPoint(){
        audioSource.PlayOneShot(Point);
    }

    public void PlaySwoosh(){
        audioSource.PlayOneShot(Swoosh);
    }

    public void PlayWing(){
        audioSource.PlayOneShot(Wing);
    }
}
