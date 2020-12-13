using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    // Camera shake
    public float maxShakeDuration;
    float shakeDuration = 0f;
    public float defaultShakeMagnitude;
    float shakeMagnitude;
    float dampingSpeed = 1.0f;

    Vector3 spawnPos;
    
    void Start()
    {
        spawnPos = transform.position;
    }

    void Update()
    {
        HandleCameraShake();
    }
    
    void HandleCameraShake()
    {
        if( shakeDuration > 0 )
        {
            transform.position += (Vector3)Random.insideUnitCircle * shakeMagnitude; 
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeMagnitude = defaultShakeMagnitude;
            transform.position = spawnPos;
        }
    }

    public void TriggerShake()
    {
        if( shakeDuration > 0 ) shakeMagnitude += defaultShakeMagnitude;
        shakeDuration = maxShakeDuration;
    }
}
