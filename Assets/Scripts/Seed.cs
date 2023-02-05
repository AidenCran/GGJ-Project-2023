using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Seed : MonoBehaviour, IWhippable
{
    // ReSharper disable once MemberCanBePrivate.Global
    public Rigidbody Rigidbody { get; set; }

    public AudioClip throwSound;
    public AudioClip hitSound;


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

        soundPool.Play(throwSound, transform.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!_isActive) return;

        HitAction();
    }

    protected virtual void HitAction()
    {
        gameObject.SetActive(false);
    }


    public void OnWhipHit()
    {
        HitAction();
    }
}
