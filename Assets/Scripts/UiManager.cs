using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [Header("UI elements")]
    public Slider slider;

    [Header("Spawn areas")]
    public Transform vinylSpawnArea;

    [Header("Prefabs")]
    public GameObject vinylPrefab;


    // Internal
    Stack<GameObject> vinyls = new Stack<GameObject>();
 

    private void Awake() {
        instance = this;
    }

    public void AddVinyl() {
        GameObject tempGo = Instantiate(vinylPrefab, vinylSpawnArea);
        vinyls.Push(tempGo);
    }

    public void RemoveVinyl() {
        Destroy(vinyls.Pop());
    }
}
