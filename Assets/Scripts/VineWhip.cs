using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class VineWhip : MonoBehaviour
{
    [SerializeField] float maxDist = 10;
    public LayerMask lm;
    public player p;
    public Animator anim;
    public AudioClip sound;

    void Start()
    {
        p.iactions.Interaction.Whip.performed += ctx => OnActive();
    }

    // On <LMB>
    void OnActive()
    {
        anim.SetTrigger("activate");
        StartCoroutine(whip());
    }


    IEnumerator whip()
    {
        soundPool.Play(sound, Camera.main.transform.position,0.2f);
        yield return new WaitForSeconds(0.2f);
        Ray r = new Ray(transform.position, transform.forward);
        // Raycast forward
        if (!Physics.SphereCast(r, 1f, out var hit, maxDist, lm, QueryTriggerInteraction.Ignore)) yield break;
        Debug.Log("whipping");

        // Check for objects
        if (hit.transform.TryGetComponent<IWhippable>(out var whip))
        {
            whip.OnWhipHit();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(new Ray(transform.position, transform.forward*maxDist));
    }
}
