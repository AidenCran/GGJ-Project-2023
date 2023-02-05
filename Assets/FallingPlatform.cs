using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallingPlatform : MonoBehaviour
{
    Rigidbody _rb;
    
    [SerializeField] float delay = 3;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<player>(out var player))
        {
            StartCoroutine(ReleasePlatform());
        }
    }

    IEnumerator ReleasePlatform()
    {
        yield return Helper.GetWait(delay);
        
        // Release Constraints
        _rb.constraints = RigidbodyConstraints.None;
    }
}