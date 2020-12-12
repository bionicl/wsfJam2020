using System;
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
    Ceiling,
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

    public static List<int> obstaclesIndexes = new List<int>() { 3 };

    public static PieceType GetNextPieceType(PieceType lastPieceType, float difficultyMultiplier = 1)
    {
        float[] probabilities = nextPiecesProbapilietes[lastPieceType];
        float[] probs = new float[probabilities.Length];
        probabilities.CopyTo(probs, 0);

        foreach (var item in obstaclesIndexes)
        {
            probs[item] *= difficultyMultiplier;
        }
        float sum = 0;
        foreach (var item in probs)
        {
            sum += item;
        }
        for (int j = 0; j < probs.Length; j++)
        {
            probs[j] /= sum;
        }

        int i;
        float p = UnityEngine.Random.Range(0.0f, 1.0f);

        sum = 0;
        for (i = 0; i < probs.Length; i++)
        {
            sum += probs[i];
            if (p < sum)
                break;
        }
        return (PieceType)i;
    }
    public PieceType pieceType;
    public int colliderPlatformStartPointIndex = 0;
    public int colliderPlatformEndPointIndex = 3;
}
