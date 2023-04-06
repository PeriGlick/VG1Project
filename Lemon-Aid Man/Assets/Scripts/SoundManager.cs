using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private Sprite bgmOnSprite;
    public Sprite bgmOffSprite;
    public Button bgmButton;
    private bool bgmIsOn = true;

    private AudioSource bgmAudioSrc;

    void Start() {
        bgmOnSprite = bgmButton.image.sprite;
        bgmAudioSrc = FindObjectOfType<BGM>().GetComponent<AudioSource>();
    }

    public void OnBgmButtonClick() {
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
