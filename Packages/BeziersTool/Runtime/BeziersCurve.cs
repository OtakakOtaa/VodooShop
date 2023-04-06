using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class BeziersCurve
{
    public IEnumerable<Vector3> GetPoints(BeziersOrigins beziersOrigins, int pointsCount = 10)
        => Enumerable.Range(0, pointsCount + 1).Select(i => GetPoint(beziersOrigins, 1f / pointsCount * i));

    public Vector3 GetPoint(BeziersOrigins beziersOrigins, float interpolationValue)
    {
        if (interpolationValue is > 1f or < 0f)
            throw new Exception($"{nameof(BeziersCurve)} / {nameof(GetPoint)} / incorrect input data");

        var oneMinus = 1.0f - interpolationValue;
        return oneMinus * oneMinus * oneMinus * beziersOrigins.P0 +
               3.0f * oneMinus * oneMinus * interpolationValue * beziersOrigins.P1 +
               3.0f * oneMinus * interpolationValue * interpolationValue * beziersOrigins.P2 +
               interpolationValue * interpolationValue * interpolationValue * beziersOrigins.P3;
    }
    
    [Serializable] public sealed class BeziersOrigins
    {
        [SerializeField] public Vector3 P0;
        [SerializeField] public Vector3 P1;
        [SerializeField] public Vector3 P2;
        [SerializeField] public Vector3 P3;
    }
}