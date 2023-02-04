using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SeedPlatform : MonoBehaviour
{
    // ReSharper disable once MemberCanBePrivate.Global
    public Rigidbody Rigidbody { get; set; }

    [SerializeField] GameObject platformPrefab;

    const float ThrowSpeed = 10;

    // Ready to spawn platform on collision
    bool _isActive = true;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void ThrowSeed(Vector3 inertia)
    {
        _isActive = true;
        Rigidbody.velocity = inertia + ThrowSpeed * transform.forward;
        
        // Play Sound?
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!_isActive) return;
        gameObject.SetActive(false);
        
        // Create Platform & Set Position
        var newPlatform = Instantiate(platformPrefab);
        newPlatform.SetActive(false);
        newPlatform.transform.position = collision.contacts[0].point;

        // Cache Scale
        var localScale = newPlatform.transform.localScale;
        var currentScale = localScale;
        
        localScale *= 0;
        
        newPlatform.transform.localScale = localScale;
        newPlatform.SetActive(true);
        
        // Bounce to Spawn Scale
        newPlatform.transform.DOScale(currentScale, 0.50f).SetEase(Ease.OutBounce);
        
        // Play Sound?
        // Spawn Particles?
    }
}
