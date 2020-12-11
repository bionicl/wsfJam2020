using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> prefabs;
    public int maxVisiblePieces = 20;
    public float speed = 1;

    private List<GameObject> currentVisiblePieces;

    private GameObject nextPiece;
    // Start is called before the first frame update
    void Start()
    {
        currentVisiblePieces = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
        if (nextPiece == null)
        {
            nextPiece = prefabs[Random.Range(0, prefabs.Count)];
        }
        if(currentVisiblePieces.Count > 0)
        {
            GameObject lastpiece = currentVisiblePieces[currentVisiblePieces.Count - 1];
            if (!lastpiece.GetComponent<PolygonCollider2D>().bounds.Intersects(nextPiece.GetComponent<PolygonCollider2D>().bounds))
            {
                SpawnPiece();
                Debug.Log("Dlaczego");
            }
        }
        else
        {
            SpawnPiece();
        }
    }

    void SpawnPiece()
    {
        if(nextPiece != null)
        {
            if (currentVisiblePieces.Count == 0)
            {
                currentVisiblePieces.Add(Instantiate(nextPiece, transform));
            }
            else
            {
                GameObject lastpiece = currentVisiblePieces[currentVisiblePieces.Count - 1];
                Vector2 lastPiecePosition = new Vector2(lastpiece.transform.position.x, lastpiece.transform.position.y);
                Vector2 lastPiecePlatformEnd = lastpiece.GetComponent<PolygonCollider2D>().points[lastpiece.GetComponent<GeneratorPieceData>().colliderPlatformEndPointIndex];
                Vector2 nextPiecePlatformStart = nextPiece.GetComponent<PolygonCollider2D>().points[nextPiece.GetComponent<GeneratorPieceData>().colliderPlatformStartPointIndex];
                currentVisiblePieces.Add(Instantiate(nextPiece, lastPiecePlatformEnd - nextPiecePlatformStart + lastPiecePosition, Quaternion.identity, transform));
            }
            nextPiece = null;
        }
    }

    void Scroll()
    {
        foreach (GameObject item in currentVisiblePieces)
        {
            item.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}
