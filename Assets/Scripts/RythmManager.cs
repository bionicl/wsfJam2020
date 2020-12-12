using System.Collections;
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
        if ((Time.timeSinceLevelLoad - delay) - (repeatTime * bitNo) >= repeatTime) {
            bitNo++;
            bool isHit = bitNo % 4 == 0;
            foreach (var item in objects) {
                item.Hit(isHit);
            }
            UiManager.instance.gameOver.Hit(isHit);
        }
    }
}
