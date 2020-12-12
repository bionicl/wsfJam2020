using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public ProjectileShooter discShooter;
    Camera cam;

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

        cam = Camera.main;
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

        CheckShooting();
        HandleAnimations();
    }

    void HandleAnimations()
    {
        animator.SetBool( "jumping", !IsGrounded() );
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.CapsuleCast(_capsuleCollider2D.bounds.center, _capsuleCollider2D.bounds.size, 0f, 0, Vector2.down, 0.4f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }

    private Collider2D GetStandingOnPlatform()
    {
        RaycastHit2D raycastHit2d = Physics2D.CapsuleCast(_capsuleCollider2D.bounds.center, _capsuleCollider2D.bounds.size, 0f, 0, Vector2.down, 0.4f, platformsLayerMask);
        //Debug.Log(raycastHit2d.collider.gameObject.tag);
        if (raycastHit2d.collider != null)
        {
            if (raycastHit2d.collider.gameObject.CompareTag( "Platforms" ))
            {
                return raycastHit2d.collider;
            } 
        }
        return null;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag( "Platforms" ) && IsGrounded() && Input.GetKeyDown(KeyCode.DownArrow))
        {
            other.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D( Collider2D other )
    {
        // Pick up vinyls
        if( other.CompareTag( "vinyl pickup" ) && GameManager.instance.AddVinyl() )
        {
            Destroy( other.gameObject );
        }
    }

    void CheckShooting() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Debug.Log("Shoot!");
            Vector2 shootDir = (Input.mousePosition - cam.WorldToScreenPoint(transform.position)).normalized;
            discShooter.TryShootOnce(shootDir);
        }
    }

}
