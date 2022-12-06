using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightedBehaviour : MonoBehaviour
{
    private Color _startColor;
    private Renderer[] _renderers;

    private void Start()
    {
        _renderers = gameObject.GetComponentsInChildren<Renderer>();
    }


    void OnMouseEnter()
    {
        foreach (var r in _renderers)
        {
            r.material.SetColor(1, Color.yellow);
        }
    }
}