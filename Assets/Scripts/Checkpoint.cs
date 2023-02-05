using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    CheckpointManager _checkpointManager;
    public AudioClip sound;

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

            soundPool.Play(sound, transform.position);

            _checkpointManager.SetCheckpoint?.Invoke(this);
        }
    }
}
