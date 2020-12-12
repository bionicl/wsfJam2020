using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceType
{
    Platform,
    SlopeUp,
    SlopeDown,
}


[RequireComponent(typeof(PolygonCollider2D))]
public class GeneratorPieceData : MonoBehaviour
{
    public static float[] GetProbabilitesForNextPiece(PieceType pieceType)
    {
        switch (pieceType)
        {
            case PieceType.Platform:
                return new float[] { 0.96f, 0.02f, 0.02f };
            case PieceType.SlopeUp:
                return new float[] { 0.28f, 0.70f, 0.02f };
            case PieceType.SlopeDown:
                return new float[] { 0.28f, 0.02f, 0.70f };
            default:
                return new float[] { 0, 0, 0 };
        }
    }


    public static PieceType GetNextPieceType(PieceType lastPieceType)
    {
        float[] probabilities = GetProbabilitesForNextPiece(lastPieceType);
        int i;
        float p = Random.Range(0.0f, 1.0f);
        float sum = 0;
        for (i = 0; i < probabilities.Length; i++)
        {
            sum += probabilities[i];
            if(p < sum)
                break;
        }
        return (PieceType)i;
    }
    public PieceType pieceType;
    public int colliderPlatformStartPointIndex = 0;
    public int colliderPlatformEndPointIndex = 3;
}
