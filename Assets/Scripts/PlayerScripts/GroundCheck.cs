using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class GroundCheck : MonoBehaviour
    {
        public bool grounded;
        Collider2D standingOnPlatform;
        public LayerMask platformsLayers;

        List<int> collidingPlatforms = new List<int>();
        
        void OnTriggerEnter2D( Collider2D other )
        {
            if( ( ( 1 << other.gameObject.layer ) & platformsLayers ) != 0 )
            {
                grounded = true;
                standingOnPlatform = other.GetComponent<Collider2D>();
                collidingPlatforms.Add( other.gameObject.GetInstanceID() );
            }
        }
    
        void OnTriggerExit2D( Collider2D other )
        {
            if( ( ( 1 << other.gameObject.layer ) & platformsLayers ) != 0 )
            {
                collidingPlatforms.Remove( other.gameObject.GetInstanceID() );

                if( collidingPlatforms.Count == 0 )
                {
                    grounded = false;
                    standingOnPlatform = null;
                }
            }
        }

        public bool IsGrounded() => grounded;
        public Collider2D GetStandingOnPlatform() => standingOnPlatform;
    }
}
