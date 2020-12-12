using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGroup : MonoBehaviour
{
    public int layer;
    Animator animator;

    bool happy = false;

    float[] thresholds = { 0.8f, 0.65f, 0.5f, 0.35f };

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (GameManager.instance.level >= thresholds[layer]) {
            if (!happy)
                animator.SetTrigger("Happy");
                happy = true;
        } else {
            if (happy) {
                animator.SetTrigger("Sad");
                happy = false;
            }
        }
    }
}
