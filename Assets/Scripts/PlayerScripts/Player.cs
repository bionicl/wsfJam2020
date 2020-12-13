using System.Collections;
using Audio;
using UnityEngine;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public Animator animator;
        public ProjectileShooter discShooter;
        public GroundCheck groundCheck;
        public GameObject slidingSmokeEffect;
        Camera cam;
        
        Rigidbody2D _rigidBody2D;
        CapsuleCollider2D _capsuleCollider2D;
        SpriteRenderer _spriteRenderer;

        public LayerMask fallThroughPlatforms;
        
        [HideInInspector] public bool invulnerability = false;

        public float jumpForce = 30f;
        const float jumpDelay = .05f;
        float timeSinceJump;

        void Start()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            _capsuleCollider2D.size = new Vector2(1f, 2f);

            if (_rigidBody2D == null)
            {
                Debug.LogError("The RigidBody2D is NULL for Player.");
            }
            if (_capsuleCollider2D == null)
            {
                Debug.LogError("The _capsuleCollider2D is NULL for Player.");
            }

            cam = Camera.main;
        }

        void Update()
        {
            var platform = GetStandingOnPlatform();
            
            // Jumping
            if( IsGrounded() && Input.GetKey( KeyCode.Space ) && timeSinceJump > jumpDelay && _rigidBody2D.velocity.y <= 0 )
            {
                _rigidBody2D.AddForce( Vector2.up * jumpForce, ForceMode2D.Impulse );
                AudioManager.instance.Play( "Jump" );
                timeSinceJump = 0;
            }
            
            // Going down platforms
            else if( IsGrounded() && platform != null && Input.GetKey( KeyCode.S ) )
            {
                if( ( ( 1 << platform.gameObject.layer ) & fallThroughPlatforms ) != 0 )
                {
                    platform.enabled = false;
                }
            }

            // Sliding
            if( IsGrounded() && Input.GetKey( KeyCode.C ) )
            {
                _capsuleCollider2D.size = new Vector2(1f, 1f);
                _capsuleCollider2D.offset = new Vector2(0, -0.5f);
                animator.SetBool( "down", true );
                slidingSmokeEffect.SetActive( true );
            }
            else
            {
                _capsuleCollider2D.size = new Vector2(1f, 2f);
                _capsuleCollider2D.offset = Vector2.zero;
                animator.SetBool( "down", false );
                slidingSmokeEffect.SetActive( false );
            }

            CheckShooting();
            HandleAnimations();

            timeSinceJump += Time.deltaTime;
        }

        void HandleAnimations()
        {
            animator.SetBool( "jumping", !IsGrounded() );
            animator.SetBool( "velocity_up", _rigidBody2D.velocity.y > 0 );
        }

        bool IsGrounded() => groundCheck.IsGrounded();

        Collider2D GetStandingOnPlatform() => groundCheck.GetStandingOnPlatform();

        void OnCollisionEnter2D( Collision2D other )
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
                AudioManager.instance.Play( "Pickup" );
                Destroy( other.gameObject );
            }
        }

        void CheckShooting()
        {
            if( Input.GetKeyDown( KeyCode.Mouse0 ) )
            {
                Vector2 shootDir = ( Input.mousePosition - cam.WorldToScreenPoint( transform.position ) ).normalized;
                if( discShooter.TryShootOnce( shootDir ) )
                {
                    AudioManager.instance.Play( "Throw" );
                }
            }
        }

        public void PlayerInvulnerability()
        {
            invulnerability = true; //switch off box collider2D
            AudioManager.instance.Play( "Damage" );
            
            //start coroutine to switch on and off character graphic
            StartCoroutine(PlayerOffAndOn());

        }

        IEnumerator PlayerOffAndOn()
        {
            Color colorFull = Color.white;
            Color colorHalf = Color.white;
            colorHalf.a = 0.5f;
            _spriteRenderer.color = colorHalf;
            yield return new WaitForSeconds(0.3f);
            _spriteRenderer.color = colorFull;
            yield return new WaitForSeconds(0.3f);
            _spriteRenderer.color = colorHalf;
            yield return new WaitForSeconds(0.3f);
            _spriteRenderer.color = colorFull;
            yield return new WaitForSeconds(0.3f);
            _spriteRenderer.color = colorHalf;
            yield return new WaitForSeconds(0.3f);
            _spriteRenderer.color = colorFull;
            yield return new WaitForSeconds(0.3f);
            invulnerability = false;
        }

    }
}
