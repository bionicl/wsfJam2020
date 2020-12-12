using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> prefabs;
    public float speed = 1;
    public float xBoundOffset = -10;
    public float yTopBound = 10;
    public float yBottomBound = -10;
    public int maxFailCount = 1000;
    public float timeToNextVinyl = 5;
    public GameObject vinylPickup;

    private List<GameObject> generatedPieces = new List<GameObject>();
    private List<GameObject> generatedPickups = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        generatedPieces.Add(Instantiate(prefabs[0], transform.position, Quaternion.identity, transform));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameStartTime == -1 || GameManager.instance.gameOver)
            return;

        timeToNextVinyl -= Time.deltaTime;
        

        float p = Random.Range(0.0f, 1.0f);

        GameObject lastPiece = generatedPieces[generatedPieces.Count - 1];

        int failCount = 0;
        while (lastPiece.transform.position.x < transform.position.x)
        {
            lastPiece = generatedPieces[generatedPieces.Count - 1];
            PieceType nextPieceType;
            if (lastPiece.transform.position.y > yTopBound)
            {
                nextPieceType = lastPiece.GetComponent<GeneratorPieceData>().pieceType == PieceType.SlopeUp ? PieceType.Platform : PieceType.SlopeDown;
            }
            else if (lastPiece.transform.position.y < yBottomBound)
            {
                nextPieceType = lastPiece.GetComponent<GeneratorPieceData>().pieceType == PieceType.SlopeDown ? PieceType.Platform : PieceType.SlopeUp;
            }
            else
            {
                nextPieceType = GeneratorPieceData.GetNextPieceType(lastPiece.GetComponent<GeneratorPieceData>().pieceType, GameManager.instance.Multiplayer / 4.0f + 0.75f);
            }

            List<GameObject> possibleNextPieces = prefabs.Where((e) => e.GetComponent<GeneratorPieceData>().pieceType == nextPieceType).ToList();

            GameObject nextPiece = possibleNextPieces[Random.Range(0, possibleNextPieces.Count)];

            Vector2 lastPieceEndPlatformOffset = lastPiece.GetComponent<PolygonCollider2D>().points[lastPiece.GetComponent<GeneratorPieceData>().colliderPlatformEndPointIndex];
            Vector2 nextPieceStartPlatformOffset = nextPiece.GetComponent<PolygonCollider2D>().points[nextPiece.GetComponent<GeneratorPieceData>().colliderPlatformStartPointIndex];

            Vector2 offsetsDiff = lastPieceEndPlatformOffset * lastPiece.transform.localScale - nextPieceStartPlatformOffset * nextPiece.transform.localScale;

            Vector3 nextPosition = lastPiece.transform.position + new Vector3(offsetsDiff.x, offsetsDiff.y);
            if (nextPosition.y > yBottomBound && nextPosition.y < yTopBound || failCount > maxFailCount)
            {
                generatedPieces.Add(Instantiate(nextPiece, nextPosition, Quaternion.identity, transform));
                if (timeToNextVinyl < 0 || Random.Range(0f, 1f) < 0.1f)
                {
                    timeToNextVinyl = 5;
                    generatedPickups.Add(Instantiate(vinylPickup, nextPiece.GetComponent<GeneratorPieceData>().vinylPickUpSpawnPosition.transform.position + nextPosition, Quaternion.identity, transform));
                }
                failCount = 0;
            }
            else
            {
                failCount++;
            }
        }
        if (timeToNextVinyl < 0)
        {
            timeToNextVinyl = 5;
        }
        DestroyOutsideBound();
        Scroll();
    }

    void Scroll()
    {
        foreach (GameObject item in generatedPieces)
        {
            item.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        foreach (GameObject item in generatedPickups)
        {
            item.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
    void DestroyOutsideBound()
    {
        for (int i = 0; i < generatedPieces.Count;)
        {
            if (generatedPieces[i].transform.position.x < transform.position.x + xBoundOffset)
            {
                GameObject gameObject = generatedPieces[i];
                generatedPieces.Remove(gameObject);
                Destroy(gameObject);
            }
            else
            {
                i++;
            }
        }
        for (int i = 0; i < generatedPickups.Count;)
        {
            GameObject gameObject = generatedPickups[i];
            if(gameObject != null)
            {
                if (gameObject.transform.position.x < transform.position.x + xBoundOffset)
                {
                    generatedPickups.Remove(gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    i++;
                }
            }
            else
            {
                generatedPickups.RemoveAt(i);
            }
        }
    }
}
