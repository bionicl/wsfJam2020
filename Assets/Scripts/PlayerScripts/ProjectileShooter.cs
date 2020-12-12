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

        public void TryShootOnce( Vector2 direction )
        {
            if (timeSinceLastShot < 1 / maxShotsPerSecond)
                return;

            if (!GameManager.instance.RemoveVinyl())
                return;

            GameObject projectile = Instantiate( projectilePrefab, transform );
            projectile.GetComponent<Projectile>().Init( direction );

            timeSinceLastShot = 0;
        }
    }
}
