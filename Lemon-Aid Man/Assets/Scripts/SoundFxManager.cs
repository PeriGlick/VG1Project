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
    public AudioClip earnMoneySound;
    public AudioClip gameOverArcadeSFX;
    public AudioClip gameOverManVoice;
    public AudioClip duckQuackNormal;
    public AudioClip duckQuackHigh;

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
    
    public void PlayEarnMoneySound() {
        if (_audioSource) {
            _audioSource.PlayOneShot(earnMoneySound, 0.3f);
        }
    }
    
    public void PlayGameOverSound() {
        if (_audioSource) {
            _audioSource.PlayOneShot(gameOverArcadeSFX, 0.2f);
            _audioSource.PlayOneShot(gameOverManVoice, 0.2f);
            Debug.Log("game over sound");
        }
    }
    
    public void PlayDuckQuackNormal() {
        if (_audioSource) {
            _audioSource.PlayOneShot(duckQuackNormal, 0.3f);
        }
    }
    
    public void PlayDuckQuackHigh() {
        if (_audioSource) {
            _audioSource.PlayOneShot(duckQuackHigh, 0.3f);
        }
    }
}
