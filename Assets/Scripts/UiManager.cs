using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [Header("UI elements")]
    public Slider slider;
    public RythmObject sliderObject;

    [Header("Spawn areas")]
    public Transform vinylSpawnArea;

    [Header("Prefabs")]
    public GameObject vinylPrefab;


    // Internal
    Stack<GameObject> vinyls = new Stack<GameObject>();
 

    private void Awake() {
        instance = this;
    }

    private void Update() {
        slider.value = GameManager.instance.level;
    }

    public void AddVinyl() {
        GameObject tempGo = Instantiate(vinylPrefab, vinylSpawnArea);
        vinyls.Push(tempGo);
    }

    public void RemoveVinyl() {
        Destroy(vinyls.Pop());
    }

    public void UpdateUi() {
        
    }
}
