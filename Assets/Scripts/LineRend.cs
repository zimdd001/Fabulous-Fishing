using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRend : MonoBehaviour
{
    public Transform target;
    private LineRenderer lineRenderer;
    void Awake()
    {
     lineRenderer = GetComponent<LineRenderer>();
    }
    
    private void AssignTarget(Vector3 startPosition, Transform newTarget)
    {
     lineRenderer.positionCount = 2;
     lineRenderer.SetPosition(0,startPosition);
     target = newTarget;
    }
    void Update()
    {
     lineRenderer.SetPosition(1,target.position);
    }
}

  

