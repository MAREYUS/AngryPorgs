using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour {

    public UnityEvent OnStartEvent;

    private AudioSource audioSource;

    private void Awake()
    {
        OnStartEvent.Invoke();
    }

    public void PlayAudioClip(AudioClip clip)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
    }

}
