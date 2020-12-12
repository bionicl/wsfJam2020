using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SecondLayer
{
    public class SecondLayerManager : MonoBehaviour
    {
        public GameObject buildingPrefab;
        public float scrollSpeed = 5f;
        
        public float cutoff = -20f;
        public int nrOfBuildings = 5;

        List<Building> buildings = new List<Building>();
        Building lastBuilding;
        
        void Start()
        {
            float spawnedWidth = 0;

            for( int i = 0; i < nrOfBuildings; i++ )
            {
                Vector3 pos = transform.position + Vector3.right * (spawnedWidth + cutoff);
                Building newBuilding = Instantiate( buildingPrefab, pos, Quaternion.identity, transform ).GetComponent<Building>();
                newBuilding.SetRandHeight();
                buildings.Add( newBuilding );
                lastBuilding = newBuilding;
                
                spawnedWidth += newBuilding.GetDistance;
            }
        }
        
        void Update()
        {
            Scroll();
        }

        void Scroll()
        {
            foreach( Building building in buildings )
            {
                building.transform.Translate( Vector3.left * (scrollSpeed * Time.deltaTime) );
            }

            // Activate buildings once they are on screen
            foreach( Building building in buildings.Where( b => b.transform.position.x < -cutoff && !b.isOnScreen) )
            {
                building.isOnScreen = true;
                building.SpawnRobots();
            }
            
            // Move buildings if they are too far
            foreach( Building building in buildings.Where( building => building.transform.position.x < cutoff ) )
            {
                building.transform.position = transform.position + new Vector3(lastBuilding.transform.position.x + lastBuilding.GetDistance, 0, 0);
                building.Clear();
                building.Init();
                lastBuilding = building;
            }
        }
    }
}
