using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public float speed = 5.0f;


    void Start()
    {

    }

    void Update()
    {
        transform.Translate(-Vector3.right * speed * Time.deltaTime);
    }

}



