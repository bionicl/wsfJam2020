﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    UiManager ui;

    [Header("Settings")]
    public int maxVinyls = 3;
    public float startLevel = .5f;
    public float funkyHitValue = .15f;
    public float funkyRatValue = .15f;

    [Tooltip("Per second")]
    public float levelDecreaseSpeed = .02f;

    // Internal
    bool _gameStarted = false;
    float _gameStartTime = -1;
    public float gameStartTime { get { return _gameStartTime; } }

    int _vinylNum;
    float _level;
    public float level { get { return _level; } }


    private void Awake() {
        instance = this;
        _level = startLevel;
    }
    private void Start() {
        ui = UiManager.instance;
        Time.timeScale = 0;
    }

    private void Update() {
        if (!_gameStarted && Input.GetKeyDown(KeyCode.Space)) {
            StartGame();
        }

        if (_gameStarted) {
            _level -= levelDecreaseSpeed * Time.deltaTime;
            _level = Mathf.Clamp01(_level);
        }
        ui.UpdateUi();
    }

    // Vinyls
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

    // Hidding enemies
    public void HitFunky() {
        _level += funkyHitValue;
        _level = Mathf.Clamp01(_level);
        ui.sliderObject.Hit(true);
    }
    public void HitRat() {
        _level -= funkyRatValue;
        _level = Mathf.Clamp01(_level);
        ui.sliderObject.Hit(true);
    }

    // Game control
    public void StartGame() {
        AudioManager.instance.Play("Music loop");
        _gameStartTime = Time.timeSinceLevelLoad;
        Time.timeScale = 1;
        _gameStarted = true;
        //ui.startGameObject.SetActive(false);
    }

    // DEBUG
    public void AddVinylButton() {
        AddVinyl();
    }
    public void RemoveVinylButton() {
        RemoveVinyl();
    }


}
