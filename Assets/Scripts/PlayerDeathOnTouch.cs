using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathOnTouch : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<player>(out var player))
        {
            player.OnDeath?.Invoke();
        }
    }
}
