using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TargetType
{
    Funky,
    Rat
}
public class TargetBehaviour : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private CapsuleCollider2D CapsuleCollider2D;
    public bool TargetUsed = false;

    public TargetType type;
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
        if (other.CompareTag("vinyl projectile") && TargetUsed == false)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();

            if (type == TargetType.Funky)
            {
                gameManager.HitFunky();
            }
            else
            {
                gameManager.HitRat();
            }
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            TargetUsed = true;
        }
    }
}
