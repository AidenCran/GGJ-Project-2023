using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    CheckpointManager _checkpointManager;

    void Start()
    {
        _checkpointManager = CheckpointManager.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<player>(out var player))
        {
            // Add UI Display | Checkpoint Activated
            Debug.Log("Checkpoint Activated");

            _checkpointManager.SetCheckpoint?.Invoke(this);
        }
    }
}
