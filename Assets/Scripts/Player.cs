using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D _rigidBody2D;
    private CapsuleCollider2D _capsuleCollider2D;


    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();

        _capsuleCollider2D.size = new Vector2(1f, 2f);

        if (_rigidBody2D == null)
        {
            Debug.LogError("The RigidBody2D is NULL for Player 1.");
        }
        if (_capsuleCollider2D == null)
        {
            Debug.LogError("The _capsuleCollider2D is NULL for Player 1.");
        }
    }


    private void Update()
    {
        var platform = GetStandingOnPlatform();
        if (Input.GetKey(KeyCode.Space))
        {
            if (IsGrounded())
            {
                float jumpVelocity = 36f;
                _rigidBody2D.velocity = Vector2.up * jumpVelocity;
            }
        }
        else if (IsGrounded() && platform != null && Input.GetKey(KeyCode.S))
        {
            platform.enabled = false;
        }

        if (IsGrounded() && Input.GetKey(KeyCode.C))
        {
            _capsuleCollider2D.size = new Vector2(1f, 1f);
        }
        else
        {
            _capsuleCollider2D.size = new Vector2(1f, 2f);
        }
            

    }


    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.CapsuleCast(_capsuleCollider2D.bounds.center, _capsuleCollider2D.bounds.size, 0f, 0, Vector2.down, 0.4f, platformsLayerMask);
        Debug.Log(raycastHit2d.collider.gameObject.tag);
        return raycastHit2d.collider != null;
    }

    private Collider2D GetStandingOnPlatform()
    {
        RaycastHit2D raycastHit2d = Physics2D.CapsuleCast(_capsuleCollider2D.bounds.center, _capsuleCollider2D.bounds.size, 0f, 0, Vector2.down, 0.4f, platformsLayerMask);
        Debug.Log(raycastHit2d.collider.gameObject.tag);
        if (raycastHit2d.collider != null)
        {
            if (raycastHit2d.collider.gameObject.tag == "Platforms")
            {
                return raycastHit2d.collider;
            } 
        }
        return null;

    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platforms" && IsGrounded() && Input.GetKeyDown(KeyCode.DownArrow))
        {
            other.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

}
