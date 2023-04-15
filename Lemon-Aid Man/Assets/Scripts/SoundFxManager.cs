using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFxManager : MonoBehaviour
{
    public static SoundFxManager instance;
    
    // Outlets
    private AudioSource _audioSource;
    public AudioClip keyboardPress;
    public AudioClip powerupSound;
    public AudioClip healSound;
    public AudioClip LAMOuchSound;

    void Awake() {
        instance = this;
    }

    void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayKeyboardPress() {
        if (_audioSource) {
            _audioSource.PlayOneShot(keyboardPress, 0.6f);
        }
    }
    
    public void PlayPowerupSound() {
        if (_audioSource) {
            _audioSource.PlayOneShot(powerupSound, 0.3f);
        }
    }
    
    public void PlayHealSound() {
        if (_audioSource) {
            _audioSource.PlayOneShot(healSound, 0.3f);
        }
    }
    
    public void PlayLAMOuch() {
        if (_audioSource) {
            _audioSource.PlayOneShot(LAMOuchSound, 0.3f);
        }
    }
}
