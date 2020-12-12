using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmObject : MonoBehaviour
{
    [Header("Objects")]
    [Tooltip("Game will try to get transform from the current object if not set");
    public Transform objectTransform;

    [Header("Settings")]
    [Range(0, 50)]
    public float normalStrength = 0f;
    [Range(0, 50)]
    public float mainStrength = 10f;
    public bool reactsToMainBeat = true;

    // internal
    float repeatTime = 0;

    private void Start() {
        repeatTime = RythmManager.instance.Register(this);
        if (objectTransform == null) {
            objectTransform = transform;
        }
    }

    public void Hit(bool main) {
        if (!objectTransform)
            return;
        float strength = normalStrength;
        if (main && reactsToMainBeat)
            strength = mainStrength;
        StartCoroutine(Lerp(strength));
    }

    IEnumerator Lerp(float strength) {
        float timeElapsed = 0;
        float duration = repeatTime / 2;

        Vector2 startPosition = objectTransform.localPosition;
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
}
