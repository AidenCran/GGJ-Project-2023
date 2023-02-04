using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SeedPlatform : Seed
{
    [SerializeField] GameObject platformPrefab;

    protected override void HitAction()
    {
        base.HitAction();
        
        // Create Platform & Set Position
        var newPlatform = Instantiate(platformPrefab);
        newPlatform.SetActive(false);
        newPlatform.transform.position = transform.position;
        newPlatform.transform.rotation = Quaternion.identity;

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
