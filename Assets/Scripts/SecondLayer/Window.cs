using UnityEngine;

namespace SecondLayer
{
    public class Window : MonoBehaviour
    {
        public GameObject robotPrefab;

        GameObject robot;
        
        public void SpawnRobot( bool isFunky )
        {
            robot = Instantiate( robotPrefab, transform );
            robot.GetComponent<RobotController>().Init( isFunky );
        }

        public void ClearRobot()
        {
            if( robot != null ) Destroy( robot );
        }
    }
}
