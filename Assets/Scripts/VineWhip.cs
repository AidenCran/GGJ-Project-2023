using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class VineWhip : MonoBehaviour
{
    [SerializeField] float maxDist = 10;
    bool isHeld;

    void Start()
    {
        // ADD BELOW FUNCTION TO ON PREFORMED
    }

    // On <LMB>
    void OnActive()
    {
        if (!isHeld) return;

        // Raycast forward
        if (!Physics.Raycast(transform.position, transform.forward, out var hit, maxDist)) return;
        
        // Check for objects
        if (hit.transform.TryGetComponent<IWhippable>(out var whip))
        {
            whip.OnWhipHit();
        }
    }
}
