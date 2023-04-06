using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteAlways]
public sealed class BeziersComponent : MonoBehaviour
{
    [Header("Origin Points")] 
    [SerializeField] private List<CurvePoints> _beziersOrigins = new ();
    
    [Space]
    [SerializeField, Range(5, 40)] private int _renderingQuality = 20;
    [SerializeField] private Color _renderColor = Color.red;
    [SerializeField] private Color _auxiliaryColor = Color.yellow;
    
    private readonly Vector3 _drawPointSize = new (0.1f, 0.1f, 0.1f);

    private BeziersCurve _beziersCurve;

    private void Start()
    {
        _beziersCurve = new BeziersCurve(); 
        InstantiateCurve();
    }
    
    public void OnDrawGizmos()
    {
        if(_beziersOrigins.Count == 0) return;
        DrawOriginPoints();
        DrawAuxiliaryLines();
        DrawCurve();
    }
    
    public void ExtendCurve()
        => InstantiateCurve(isFirstLink: false);

    private void DrawAuxiliaryLines()
    {
        var cachedColor = Gizmos.color;
        Gizmos.color =_auxiliaryColor;
        
        const float factorScale = 2.0f;
        
        foreach (var item in _beziersOrigins)
        {
            var position = item.P0.position;
            var position1 = item.P3.position;

            Gizmos.DrawLine(position, position + (item.P1.position - position) * factorScale);
            Gizmos.DrawLine(position1, position1 + (item.P2.position - position1) * factorScale);
        }
        
        Gizmos.color = cachedColor;
    }

    private void DrawOriginPoints()
    {
        var cachedColor = Gizmos.color;
        Gizmos.color = _renderColor;

        foreach (var item in _beziersOrigins)
        {
            Gizmos.DrawCube(item.P0.position, _drawPointSize);
            Gizmos.DrawCube(item.P1.position, _drawPointSize);
            Gizmos.DrawCube(item.P2.position, _drawPointSize);
            Gizmos.DrawCube(item.P3.position, _drawPointSize);
        }
        
        Gizmos.color = cachedColor;
    }

    private void DrawCurve()
    {
        var cachedColor = Gizmos.color;
        Gizmos.color = _renderColor;

        foreach (var segment in _beziersOrigins)
        {
            var pointsOnCurve = _beziersCurve.GetPoints(segment.ToBeziersOrigins(), _renderingQuality).ToArray();
            for (var i = 0; i < pointsOnCurve.Length - 1; i++)
                Gizmos.DrawLine(pointsOnCurve.ElementAt(i), pointsOnCurve.ElementAt(i + 1));
        }
        
        Gizmos.color = cachedColor;
    }

    private void InstantiateCurve(bool isFirstLink = true)
    {
        const float xDefaultOffset = 0.2f;
        const float yDefaultOffset = 0.3f;
        var originZ = transform.position.z;

        var nestedObject = new GameObject("Item").transform;
        
        CurvePoints curvePoints =new ();
        
        if (isFirstLink)
        {
            curvePoints.P0 = new GameObject("P0").transform;
            curvePoints.P1 = new GameObject("P1").transform;
        }
        else
        {
            curvePoints.P0 = _beziersOrigins.Last().P3;
            curvePoints.P1 = _beziersOrigins.Last().P2;
        }

        curvePoints.P2 = new GameObject("P2").transform;
        curvePoints.P3 = new GameObject("P3").transform;

        nestedObject.parent = transform;
        curvePoints.P3.parent = nestedObject;
        curvePoints.P2.parent = nestedObject;
        curvePoints.P1.parent = nestedObject;
        curvePoints.P0.parent = nestedObject;

        var offset = isFirstLink ? 0 : 
            Vector3.Distance(_beziersOrigins.Last().P0.position, _beziersOrigins.Last().P3.position);

        curvePoints.P0.Translate(new Vector3(-xDefaultOffset + offset, yDefaultOffset, originZ));
        curvePoints.P1.Translate(new Vector3(-xDefaultOffset + offset, -yDefaultOffset, originZ));
        curvePoints.P2.Translate(new Vector3(xDefaultOffset + offset, -yDefaultOffset, originZ));
        curvePoints.P3.Translate(new Vector3(xDefaultOffset + offset, yDefaultOffset, originZ));
        
        _beziersOrigins.Add(curvePoints);
    }
    
    [Serializable] public sealed class CurvePoints 
    {
        [SerializeField] public Transform P0;
        [SerializeField] public  Transform P1;
        [SerializeField] public  Transform P2;
        [SerializeField] public  Transform P3;

        public BeziersCurve.BeziersOrigins ToBeziersOrigins()
            => new() { P0 = P0.position, P1 = P1.position, P2 = P2.position, P3 = P3.position };
    }
}