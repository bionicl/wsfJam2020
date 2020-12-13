using UnityEngine;

namespace PlayerScripts
{
    public class ProjectileShooter : MonoBehaviour
    {
        public GameObject projectilePrefab;
        public float maxShotsPerSecond;

        float timeSinceLastShot;

        void Update()
        {
            timeSinceLastShot += Time.deltaTime;
        }

        public bool TryShootOnce( Vector2 direction )
        {
            if (timeSinceLastShot < 1 / maxShotsPerSecond)
                return false;

            if (!GameManager.instance.RemoveVinyl())
                return false;

            GameObject projectile = Instantiate( projectilePrefab, transform );
            projectile.GetComponent<Projectile>().Init( direction );

            timeSinceLastShot = 0;
            return true;
        }
    }
}
