using UnityEngine;

namespace SecondLayer
{
    public class Building : MonoBehaviour
    {
        // public float width = 20f;
        public float minPosY = 0f;
        public float maxPosY = 5f;
        public float minDistance = 5.5f;
        public float maxDistance = 10f;
        
        public Window[] windows;
        public float chanceOfRobot = 0.1f;
        public float chanceOfFunky = 0.6f;    // Chance of rat = 1 - Chance of funky
        [HideInInspector] public bool isOnScreen;
        
        public void Init()
        {
            SetRandHeight();
        }

        public void SetRandHeight()
        {
            Vector3 pos = transform.position;
            pos.y = Random.Range( minPosY, maxPosY );
            transform.position = pos;
        }

        public void SpawnRobots()
        {
            foreach( Window window in windows )
            {
                if( Random.Range( 0f, 1f ) < chanceOfRobot )
                {
                    window.SpawnRobot( Random.Range( 0f, 1f ) < chanceOfFunky );
                }
            }
        }

        public void Clear()
        {
            foreach( Window window in windows )
            {
                window.ClearRobot();
                isOnScreen = false;
            }
        }

        public float GetDistance => Random.Range( minDistance, maxDistance );
    }
}
