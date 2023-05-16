using System.Collections;
using System.Collections.Generic;

using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class SwooshSound : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlaySwooshSound()
    {
        audioSource.PlayOneShot(clip);
    }
}
