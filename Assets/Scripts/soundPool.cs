using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundPool : pool
{
    public static soundPool main;
    public AudioClip defaultMenuClick;

    public override void Awake()
    {
        base.Awake();
        if (!main)
        {
            main = this;
            Debug.Log("Ahaha I am the main soundPool");
        }
        else
        {
            Debug.Log("Killing myself now");

            Destroy(gameObject);
        }
    }

    public static GameObject Play(AudioClip clip, Vector3 position, float volume = 1f, float range = 50f, float pitch = 1f, float pitchRange = 0f, float volumeRange = 0f, bool relative = false)
    {
        AudioListener al = FindObjectOfType<AudioListener>();
        AudioSource p = main.Pull(relative ? al.transform.position + position : position, Quaternion.identity).GetComponent<AudioSource>();
        p.volume = volume + Random.Range(-volumeRange, volumeRange);
        p.maxDistance = range;
        p.pitch = pitch + Random.Range(-pitchRange, pitchRange);
        p.PlayOneShot(clip);
        main.Timeout(p.gameObject,clip.length);
        return p.gameObject;
    }
}
