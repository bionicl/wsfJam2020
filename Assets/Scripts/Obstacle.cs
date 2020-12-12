using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        Player player = FindObjectOfType<Player>();

        if (other.CompareTag("Player") && player.invulnerability == false)
        {
            Destroy(gameObject);
            player.PlayerInvulnerability();
            gameManager.HitRat();
        }
    }
}
