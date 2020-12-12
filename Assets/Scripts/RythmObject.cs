using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RythmAnimationType {
    jump,
    zoom
}

public class RythmObject : MonoBehaviour
{
    [Header("Objects")]
    [Tooltip("Game will try to get transform from the current object if not set")]
    public Transform objectTransform;

    [Header("Settings")]
    public RythmAnimationType animationType = RythmAnimationType.jump;
    [Range(0, 50)]
    public float normalStrength = 0f;
    [Range(0, 50)]
    public float mainStrength = 10f;
    [Range(0, 0.1f)]
    public float delay = 0;
    public bool reactsToMainBeat = true;

    // internal
    float repeatTime = 0;
    Vector2 startScale;
    Vector2 startPosition;

    private void Start() {
        repeatTime = RythmManager.instance.Register(this);
        if (objectTransform == null) {
            objectTransform = transform;
        }
        startScale = objectTransform.localScale;
        startPosition = objectTransform.localPosition;
    }

    public void Hit(bool main) {
        if (!objectTransform)
            return;
        float strength = normalStrength;
        if (main && reactsToMainBeat)
            strength = mainStrength;
        if (animationType == RythmAnimationType.jump)
            StartCoroutine(Lerp(strength));
        else
            StartCoroutine(LerpSize(strength));
    }

    IEnumerator Lerp(float strength) {
        float timeElapsed = -delay;
        float duration = repeatTime / 2;

        Vector2 tempPos = objectTransform.localPosition;
        while (timeElapsed < duration) {
            tempPos = objectTransform.localPosition;
            tempPos.y = Mathf.Lerp(startPosition.y + strength, startPosition.y, timeElapsed / duration);
            objectTransform.localPosition = tempPos;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        tempPos.y = startPosition.y;
        objectTransform.localPosition = tempPos;
    }

    IEnumerator LerpSize(float strength) {
        float timeElapsed = -delay;
        float duration = repeatTime / 2;
        
        while (timeElapsed < duration) {
            float scalex = Mathf.Lerp(startScale.x + strength / 100f, startScale.x, timeElapsed / duration);
            float scaley = Mathf.Lerp(startScale.y + strength / 100f, startScale.y, timeElapsed / duration);
            Vector2 newVector = new Vector2(scalex, scaley);
            objectTransform.localScale = newVector;

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        objectTransform.localScale = startScale;
    }
}
