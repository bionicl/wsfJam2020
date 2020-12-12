using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private CapsuleCollider2D CapsuleCollider2D;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        CapsuleCollider2D = GetComponent<CapsuleCollider2D>();

        if (Rigidbody2D == null)
        {
            Debug.LogError("No Rigidbody2D component");
        }
        if (CapsuleCollider2D == null)
        {
            Debug.LogError("No CapsuleCollider2D component");
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("vinyl projectile"))
        {
            Debug.Log("Vinyl projectile hit target!");//to delete later
            //call method to add Funk
            Destroy(other);
        }
    }
}
