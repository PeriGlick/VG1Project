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
    // public AudioClip duckQuack;

    void Awake() {
        instance = this;
        // if (instance == null) {
        //     instance = this;
        //     DontDestroyOnLoad(instance);
        // } else {
        //     Destroy(gameObject);
        // }
    }

    // Start is called before the first frame update
    void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayKeyboardPress() {
        if (_audioSource) {
            _audioSource.PlayOneShot(keyboardPress);
        }
    }
}
