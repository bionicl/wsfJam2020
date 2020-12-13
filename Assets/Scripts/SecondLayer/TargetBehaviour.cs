using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum TargetType
{
    Funky,
    Rat
}

public class TargetBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D ownCollider;
    public bool targetUsed = false;

    public TargetType type;

    [HideInInspector] public UnityEvent gotHit;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ownCollider = GetComponent<Collider2D>();

        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D component");
        }
        if (ownCollider == null)
        {
            Debug.LogError("No Collider2D component");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("vinyl projectile") && targetUsed == false)
        {
            gotHit.Invoke();
            
            if (type == TargetType.Funky)
            {
                GameManager.instance.HitFunky();
            }
            else
            {
                GameManager.instance.HitRat();
            }
            
            // Destroy the vinyl projectile
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            targetUsed = true;
        }
    }
}
