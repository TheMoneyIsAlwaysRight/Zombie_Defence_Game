using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EdgeCollider2D))]
public class Layer : MonoBehaviour
{
    EdgeCollider2D edgeCollider2D;
    LineRenderer lineRenderer;

    private void Awake()
    {
        edgeCollider2D = GetComponent<EdgeCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Play();
    }

    void Play()
    {

    }
}
