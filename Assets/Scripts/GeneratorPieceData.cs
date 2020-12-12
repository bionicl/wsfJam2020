using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceType
{
    Platform,
    SlopeUp,
    SlopeDown,
    Fence,
    AbovePlatform,
    Start,
}


[RequireComponent(typeof(PolygonCollider2D))]
public class GeneratorPieceData : MonoBehaviour
{
    public static Dictionary<PieceType, float[]> nextPiecesProbapilietes = new Dictionary<PieceType, float[]>()
    {
        { PieceType.Platform, new float[] {0.76f, 0.02f, 0.02f, 0.05f, 0.15f, 0.00f} },
        { PieceType.SlopeUp, new float[] {0.50f, 0.50f, 0.00f, 0.00f, 0.00f, 0.00f}},
        { PieceType.SlopeDown,new float[] {0.30f, 0.00f, 0.50f, 0.10f, 0.10f, 0.00f}},
        { PieceType.Fence,new float[] {0.70f, 0.00f, 0.20f, 0.00f, 0.10f, 0.00f}},
        { PieceType.AbovePlatform,new float[] { 0.30f, 0.00f, 0.00f, 0.00f, 0.70f, 0.00f }},
        { PieceType.Start, new float[] { 1.00f, 0.00f, 0.00f, 0.00f, 0.00f, 0.00f }},
    };


    public static PieceType GetNextPieceType(PieceType lastPieceType)
    {
        float[] probabilities = nextPiecesProbapilietes[lastPieceType];
        int i;
        float p = Random.Range(0.0f, 1.0f);
        float sum = 0;
        for (i = 0; i < probabilities.Length; i++)
        {
            sum += probabilities[i];
            if (p < sum)
                break;
        }
        return (PieceType)i;
    }
    public PieceType pieceType;
    public int colliderPlatformStartPointIndex = 0;
    public int colliderPlatformEndPointIndex = 3;
}
