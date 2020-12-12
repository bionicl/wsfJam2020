using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    UiManager ui;

    [Header("Settings")]
    public int maxVinyls = 3;
    public float startLevel = .5f;
    [Tooltip("Per second")]
    public float levelDecreaseSpeed = .02f;

    // Internal
    int _vinylNum;
    float level;

    private void Awake() {
        instance = this;
        level = startLevel;
    }
    private void Start() {
        ui = UiManager.instance;
    }

    private void Update() {
        level -= levelDecreaseSpeed * Time.deltaTime;
        ui.slider.value = level;
    }

    public bool AddVinyl() {
        if (_vinylNum >= maxVinyls)
            return false;
        _vinylNum++;
        ui.AddVinyl();
        return true;
    }

    public bool RemoveVinyl() {
        if (_vinylNum <= 0)
            return false;
        ui.RemoveVinyl();
        _vinylNum--;
        return false;
    }

    // DEBUG
    public void AddVinylButton() {
        AddVinyl();
    }
    public void RemoveVinylButton() {
        RemoveVinyl();
    }


}
