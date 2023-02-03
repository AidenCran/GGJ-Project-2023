using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FadePlatform : MonoBehaviour
{
    [SerializeField] float fadeDelay = 3;

    Material _mat;
    
    void Start() => _mat = GetComponent<MeshRenderer>().material;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<player>(out var player))
        {
            // Can do something to player (E.g. Speed Bonus)
            
            // Start Fade Timer
            StartCoroutine(FadeTimer());
        }
    }

    IEnumerator FadeTimer()
    {
        var noAlpha = _mat.color;
        noAlpha.a = 0;

        yield return _mat.DOColor(noAlpha, fadeDelay).WaitForCompletion();

        // Disable
        gameObject.SetActive(false);
    }
}
