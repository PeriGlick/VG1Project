using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;
    public Text timeCounter;
    public TimeSpan timePlaying; //was private, made public for testing
    public bool timerGoing; // same as above
    public float elapsedTime; //also same as above
    public float endTime = 300.00f;
    

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        timeCounter.text = "00:00";
        timerGoing = false;
    }

    public void BeginTimer() {
        timerGoing = true;
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());
    }

    public void EndTimer() {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer() {
        while (timerGoing) {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("mm':'ss");
            timeCounter.text = timePlayingStr;
            if (elapsedTime >= endTime)
            {
                gameManager.instance.timeUp = true;

            } 
            yield return null;
        }
    }
}