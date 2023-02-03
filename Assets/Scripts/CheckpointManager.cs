using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    #region Singleton

    public static CheckpointManager Instance { get; private set; }

    private void Singleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    #endregion

    
    public Action<Checkpoint> SetCheckpoint;

    Transform _respawnLocation;

    void Awake()
    {
        Singleton();
    }

    void Start()
    {
        SetCheckpoint += (x) => _respawnLocation = x.transform;
    }

    public void RespawnPlayerOnDeath(Transform playerTransform)
    {
        playerTransform.position = _respawnLocation.transform.position;
    }
}
