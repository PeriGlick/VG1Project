using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private static BGM _bgm;

    private void Awake() {
        if (_bgm == null) {
            _bgm = this;
            DontDestroyOnLoad(_bgm);
        } else {
            Destroy(gameObject);
        }
    }
}
