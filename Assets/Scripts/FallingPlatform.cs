using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallingPlatform : MonoBehaviour
{
    Rigidbody _rb;
    
    [SerializeField] float delay = 3;

    public AudioClip sound;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<player>(out var player))
        {
            soundPool.Play(sound, transform.position);
            StartCoroutine(ReleasePlatform());
        }
    }

    IEnumerator ReleasePlatform()
    {
        var delayInThird = delay / 3;

        yield return transform.DOPunchRotation(new Vector3(2, 2, 2), delay).WaitForCompletion();

        // Release Constraints
        _rb.constraints = RigidbodyConstraints.None;


    }
}