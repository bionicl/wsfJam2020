using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    public ProjectileShooter discShooter;
    Camera cam;
    
    void Start()
    {
        cam = Camera.main;
    }
    
    void Update()
    {
        if( Input.GetKeyDown( KeyCode.Mouse0 ) )
        {
            Vector2 shootDir = ( Input.mousePosition - cam.WorldToScreenPoint( transform.position ) ).normalized;
            discShooter.TryShootOnce( shootDir );
        }
    }
}
