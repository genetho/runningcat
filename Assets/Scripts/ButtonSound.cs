using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip hoverSound;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void HoverSound()
    {
        audioSource.PlayOneShot(hoverSound);
    }
}
