using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    Animator sliderAnimator;

    [Header("UI elements")]
    public Slider slider;
    public RythmObject sliderObject;
    public GameObject startGameObject;
    public Text distanceText;
    public Text pointsText;
    public GameOverAnimationsController gameOver;
    public Text gameoverScoreText;

    [Header("Spawn areas")]
    public Transform vinylSpawnArea;

    [Header("Prefabs")]
    public GameObject vinylPrefab;

    [Header("Settings")]
    public Color badColor;
    public Color goodColor;


    // Internal
    Stack<GameObject> vinyls = new Stack<GameObject>();
 

    private void Awake() {
        instance = this;
        sliderAnimator = slider.GetComponent<Animator>();
    }

    private void Update() {
        slider.value = GameManager.instance.level;
        //slider.targetGraphic.color = Color.Lerp(badColor, goodColor, slider.value);
        sliderAnimator.SetFloat("SliderValue", GameManager.instance.level);
    }

    public void AddVinyl() {
        GameObject tempGo = Instantiate(vinylPrefab, vinylSpawnArea);
        vinyls.Push(tempGo);
    }

    public void RemoveVinyl() {
        Destroy(vinyls.Pop());
    }
}
