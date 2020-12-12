using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> prefabs;
    public float speed = 1;
    public float xBoundOffset = -10;

    private List<GameObject> generatedPieces = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float p = Random.Range(0.0f, 1.0f);
        
        if(generatedPieces.Count == 0)
        {
            GameObject nextPiece = prefabs[0];
            generatedPieces.Add(Instantiate(nextPiece,transform.position,Quaternion.identity,transform));
        }
        else
        {

            GameObject lastPiece = generatedPieces[generatedPieces.Count - 1];
            while (lastPiece.transform.position.x < transform.position.x)
            {
                lastPiece = generatedPieces[generatedPieces.Count - 1];
                PieceType nextPieceType = GeneratorPieceData.GetNextPieceType(lastPiece.GetComponent<GeneratorPieceData>().pieceType);

                List<GameObject> possibleNextPieces = prefabs.Where((e) => e.GetComponent<GeneratorPieceData>().pieceType == nextPieceType).ToList();

                GameObject nextPiece = possibleNextPieces[Random.Range(0,possibleNextPieces.Count)];

                Vector2 lastPieceEndPlatformOffset = lastPiece.GetComponent<PolygonCollider2D>().points[lastPiece.GetComponent<GeneratorPieceData>().colliderPlatformEndPointIndex];
                Vector2 nextPieceStartPlatformOffset = nextPiece.GetComponent<PolygonCollider2D>().points[nextPiece.GetComponent<GeneratorPieceData>().colliderPlatformStartPointIndex];

                Vector2 offsetsDiff = lastPieceEndPlatformOffset * lastPiece.transform.localScale - nextPieceStartPlatformOffset * nextPiece.transform.localScale;

                generatedPieces.Add(Instantiate(nextPiece, lastPiece.transform.position + new Vector3(offsetsDiff.x, offsetsDiff.y), Quaternion.identity, transform));
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
            if(generatedPieces[i].transform.position.x < transform.position.x + xBoundOffset)
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
