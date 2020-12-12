using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float speed = 500f;
    public float spawnLifetime = 2f;
    
    Rigidbody2D rb;
    Vector2 direction;
    float remainingLifetime;

    public void Init( Vector2 shootDirection )
    {
        direction = shootDirection;

        float angle = Mathf.Atan2( shootDirection.y, shootDirection.x ) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler( 0, 0, angle );
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        remainingLifetime = spawnLifetime;
    }

    void Update()
    {
        remainingLifetime -= Time.deltaTime;

        if( remainingLifetime < 0 )
        {
            Destroy( gameObject );
        }
    }

    void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce( direction * (speed * Time.fixedDeltaTime), ForceMode2D.Impulse );
    }

    void OnTriggerEnter( Collider other )
    {
        // TODO add triggers away from camera that destroy projectiles
    }
}
