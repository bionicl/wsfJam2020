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

    private List<GameObject> generatedPieces = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float p = Random.Range(0.0f, 1.0f);

        if (generatedPieces.Count == 0)
        {
            GameObject nextPiece = prefabs[0];

            generatedPieces.Add(Instantiate(nextPiece, transform.position, Quaternion.identity, transform));
        }
        else
        {

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
                    nextPieceType = GeneratorPieceData.GetNextPieceType(lastPiece.GetComponent<GeneratorPieceData>().pieceType, GameManager.instance.Multiplayer/4.0f+0.75f);
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
                    failCount = 0;
                }
                else
                {
                    failCount++;
                }
            }

        }
        Scroll();
        DestroyOutsideBound();
    }

    void Scroll()
    {
        foreach (GameObject item in generatedPieces)
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
    }
}
