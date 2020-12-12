using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SłoneczkoRotate : MonoBehaviour
{
    public float speed = 5;
    public float showThreshold = 0.9f;
    SpriteRenderer sprite;

    private void Awake() {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sprite.enabled = GameManager.instance.level >= showThreshold;
        float change = Time.deltaTime * speed;
        transform.RotateAround(transform.position, new Vector3(0,0,1), change);
    }
}
