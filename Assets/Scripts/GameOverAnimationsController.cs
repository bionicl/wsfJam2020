using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverAnimationsController : MonoBehaviour
{
    public Image box;
    public Sprite box1;
    public Sprite box2;
    bool isOnBox1 = true;

    public GameObject[] flarePrefabs;
    public Transform flaresBackground;

    public void Hit(bool main) {
        if (!GameManager.instance.gameOver)
            return;

        if (isOnBox1) {
            box.sprite = box2;
        } else {
            box.sprite = box1;
        }
        isOnBox1 = !isOnBox1;

           SpawnFlare();
    }

    void SpawnFlare() {
        GameObject flare = flarePrefabs[Random.Range(0, flarePrefabs.Length)];
        GameObject tempGo = Instantiate(flare, flaresBackground);
        tempGo.transform.localPosition = new Vector2(Random.Range(-300, 300), Random.Range(-300, 300));
    }
}
