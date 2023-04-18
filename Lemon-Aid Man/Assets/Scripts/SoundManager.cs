using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // For BGM
    private Sprite bgmOnSprite;
    public Sprite bgmOffSprite;
    public Button bgmButton;
    private bool bgmIsOn = true;
    private AudioSource bgmAudioSrc;
    
    // For SFX
    private Sprite sfxOnSprite;
    public Sprite sfxOffSprite;
    public Button sfxButton;
    private bool sfxIsOn = true;
    private AudioSource sfxAudioSrc;

    void Start() {
        bgmOnSprite = bgmButton.image.sprite;
        if (FindObjectOfType<BGM>()) {
            bgmAudioSrc = FindObjectOfType<BGM>().GetComponent<AudioSource>();
        }
        
        sfxOnSprite = sfxButton.image.sprite;
        if (FindObjectOfType<SoundFxManager>()) {
            sfxAudioSrc = FindObjectOfType<SoundFxManager>().GetComponent<AudioSource>();
        }
    }

    public void OnBgmButtonClick() {
        if (bgmAudioSrc) {
            if (bgmIsOn) {
                bgmButton.image.sprite = bgmOffSprite;
                bgmIsOn = false;
                bgmAudioSrc.mute = true;
            } else {
                bgmButton.image.sprite = bgmOnSprite;
                bgmIsOn = true;
                bgmAudioSrc.mute = false;
            }
        }
    }
    
    public void OnSFXButtonClick() {
        if (sfxAudioSrc) {
            if (sfxIsOn) {
                sfxButton.image.sprite = sfxOffSprite;
                sfxIsOn = false;
                sfxAudioSrc.mute = true;
            } else {
                sfxButton.image.sprite = sfxOnSprite;
                sfxIsOn = true;
                sfxAudioSrc.mute = false;
            }
        }
    }
}
