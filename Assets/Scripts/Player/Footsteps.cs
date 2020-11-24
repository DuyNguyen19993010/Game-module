using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] clips;
    [SerializeField]
    private AudioClip[] jumps;
    [SerializeField]
    private AudioClip[] attacks;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
        AudioClip clip1 = GetRandomClip(clips);
        audioSource.PlayOneShot(clip1);
    }

    private void JumpSound()
    {
        AudioClip clip2 = GetRandomClip(jumps);
        audioSource.PlayOneShot(clip2);
    }

    private void AttackSound()
    {
        AudioClip clip3 = GetRandomClip(attacks);
        audioSource.PlayOneShot(clip3);
    }

    private AudioClip GetRandomClip(AudioClip[] sounds)
    {
        return sounds[UnityEngine.Random.Range(0, sounds.Length)];
    }

}
