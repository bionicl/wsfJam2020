using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class GeneratorPieceData : MonoBehaviour
{
    public int colliderPlatformStartPointIndex = 0;
    public int colliderPlatformEndPointIndex = 3;
}
