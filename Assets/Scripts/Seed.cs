using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Seed : MonoBehaviour
{
    // ReSharper disable once MemberCanBePrivate.Global
    public Rigidbody Rigidbody { get; set; }


    public float ThrowSpeed = 10;

    // Ready to spawn platform on collision
    bool _isActive = false;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void ThrowSeed(Vector3 inertia, Vector3 direction)
    {
        _isActive = true;
        Rigidbody.velocity = inertia + ThrowSpeed * direction;

        // Play Sound?
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!_isActive) return;
        gameObject.SetActive(false);

        HitAction(collision);
    }

    public abstract void HitAction(Collision collision);


}
