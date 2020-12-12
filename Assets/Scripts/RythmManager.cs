﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class RythmManager : MonoBehaviour
{
    public static RythmManager instance;
    GameManager gm;

    [Header("Settings")]
    public int bpm = 120;
    public float delay = 0;

    // internal
    float repeatTime;
    static List<RythmObject> objects = new List<RythmObject>();
    int bitNo = -1;

    private void Awake() {
        instance = this;
        repeatTime = 1 / (bpm / 60f);
    }

    private void Start() {
        gm = GameManager.instance;
    }

    public float Register(RythmObject obj) {
        if (!objects.Contains(obj))
            objects.Add(obj);
        return repeatTime;
    }


    private void Update() {
        if (gm.gameStartTime == -1)
            return;
        if ((Time.timeSinceLevelLoad - delay) - (repeatTime * bitNo) >= repeatTime) {
            bitNo++;
            foreach (var item in objects) {
                item.Hit(bitNo % 4 == 0);
            }
        }
    }
}
