using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceType
{
    SlopeUp,
    SlopeDown,
    Platform,
}


[RequireComponent(typeof(PolygonCollider2D))]
public class GeneratorPieceData : MonoBehaviour
{
    public static float[] GetProbabilitesForNextPiece(PieceType pieceType)
    {
        switch (pieceType)
        {
            case PieceType.SlopeUp:
                return new float[] { 0, 0.7f, 0.72f };
            case PieceType.SlopeDown:
                return new float[] { 0, 0.02f, 0.72f };
            case PieceType.Platform:
                return new float[] { 0, 0.02f, 0.04f };
            default:
                return new float[] { 0, 0, 0 };
        }
    }


    public static PieceType GetNextPieceType(PieceType lastPieceType)
    {
        float[] probabilities = GetProbabilitesForNextPiece(lastPieceType);
        int i;
        float p = Random.Range(0.0f, 1.0f);
        for (i = probabilities.Length - 1; i > -1; i--)
        {
            if (probabilities[i] < p) break;
        }
        return (PieceType)i;
    }
    public PieceType pieceType;
    public int colliderPlatformStartPointIndex = 0;
    public int colliderPlatformEndPointIndex = 3;
}
