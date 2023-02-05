using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedTeleport : Seed
{
    protected override void HitAction()
    {
        base.HitAction();
        
        gameObject.SetActive(false);
        player.Instance.transform.position = transform.position;
    }
}
