using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedTeleport : Seed
{
    public Vector3 velocityOverride;
    protected override void HitAction()
    {
        base.HitAction();
        
        gameObject.SetActive(false);
        player.Instance.transform.position = transform.position;
        player.Instance.rb.velocity = velocityOverride;
    }
}
