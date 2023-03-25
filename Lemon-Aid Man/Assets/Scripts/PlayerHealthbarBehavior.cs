using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbarBehavior : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    private Vector3 _offset = new Vector3(0, 1, 0);

    public void SetHealth(int health, int maxHealth) {
        slider.value = health;
        slider.maxValue = maxHealth;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }

    // Update is called once per frame
    void Update() {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + _offset);
    }
}
