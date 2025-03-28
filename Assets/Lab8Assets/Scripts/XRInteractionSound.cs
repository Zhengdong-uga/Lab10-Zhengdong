using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class XRInteractionSound : MonoBehaviour
{
    public AudioClip HoverSound;
    public AudioClip SelectSound;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    public void PlayHoverSound()
    {
        audioSource.PlayOneShot(HoverSound);
    }

    public void PlaySelectSound()
    {
        audioSource.PlayOneShot(SelectSound);
    }
}
