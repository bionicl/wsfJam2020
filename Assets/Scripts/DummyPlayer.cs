using Audio;
using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    public ProjectileShooter discShooter;
    Camera cam;
    
    void Start()
    {
        cam = Camera.main;
        
        // DEBUG, play music
        AudioManager.instance.Play( "Music start" );
    }
    
    void Update()
    {
        if( Input.GetKeyDown( KeyCode.Mouse0 ) )
        {
            Vector2 shootDir = ( Input.mousePosition - cam.WorldToScreenPoint( transform.position ) ).normalized;
            discShooter.TryShootOnce( shootDir );
            
            // TODO only play if the disc was thrown
            AudioManager.instance.Play( "Throw" );
        }
    }
}
