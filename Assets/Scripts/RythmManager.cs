using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class RythmManager : MonoBehaviour
{
    public static RythmManager instance;

    [Header("Settings")]
    public int bpm = 120;

    // internal
    float repeatTime;
    static List<RythmObject> objects = new List<RythmObject>();
    int bitNo = -1;

    private void Awake() {
        instance = this;
        repeatTime = 1 / (bpm / 60f);
    }

    private void Start() {
        Debug.Log("Repeat time: " + repeatTime);
        AudioManager.instance.Play("Music loop");
    }

    public float Register(RythmObject obj) {
        if (!objects.Contains(obj))
            objects.Add(obj);
        return repeatTime;
    }

    private void Update() {
        if (Time.timeSinceLevelLoad - (repeatTime * bitNo) >= repeatTime) {
            bitNo++;
            foreach (var item in objects) {
                item.Hit(bitNo % 4 == 0);
            }
        }
    }
}
