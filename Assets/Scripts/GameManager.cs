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
    public float distanceMultiplier = 10;
    public float pointsMultiplier = 5;
    public float[] multiplayers = { 0.8f, 0.6f, 0 };

    [Tooltip("Per second")]
    public float levelDecreaseSpeed = .02f;

    public bool gameOver = false;

    // Internal
    bool _gameStarted = false;
    float _gameStartTime = -1;
    public float gameStartTime { get { return _gameStartTime; } }

    int _vinylNum;
    float _level;
    public float level { get { return _level; } }
    float points = 0;

    private void Awake() {
        instance = this;
        _level = startLevel;
    }
    private void Start() {
        ui = UiManager.instance;
        //Time.timeScale = 0;
        StartCoroutine(StartMusicWithDelay());
    }

    IEnumerator StartMusicWithDelay() {
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.Play("Music loop");
    }

    private void Update() {
        if (!_gameStarted && Input.GetKeyDown(KeyCode.Space)) {
            StartGame();
        }

        if (_gameStarted && !gameOver) {
            _level -= levelDecreaseSpeed * Time.deltaTime;
            _level = Mathf.Clamp01(_level);

            int distance = Mathf.FloorToInt(Time.timeSinceLevelLoad * distanceMultiplier);
            ui.distanceText.text = distance.ToString();

            float newPoints = Time.deltaTime * Multiplayer * pointsMultiplier;
            points += newPoints;
            ui.pointsText.text = Mathf.FloorToInt(points).ToString();
        }
        if (!gameOver && level <= 0.05) {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.V)) {
            AddVinyl();
        }
    }
    public int Multiplayer {
        get {
            if (level > multiplayers[0])
                return 3;
            else if (level > multiplayers[1])
                return 2;
            else
                return 1;
        }
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
        return true;
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
        _gameStartTime = Time.timeSinceLevelLoad;
        _gameStarted = true;
        ui.startGameObject.GetComponent<Animator>().enabled = true;
    }
    public void GameOver() {
        gameOver = true;
        ui.gameOver.gameObject.SetActive(true);
        ui.gameoverScoreText.text = Mathf.FloorToInt(points).ToString();
        Destroy(PlayerScripts.Player.instance.gameObject);
    }

    // DEBUG
    public void AddVinylButton() {
        AddVinyl();
    }
    public void RemoveVinylButton() {
        RemoveVinyl();
    }


}
