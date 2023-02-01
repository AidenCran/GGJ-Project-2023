using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncePad : MonoBehaviour
{
    public float bounceHeight;

    void OnTriggerEnter(Collider other)
    {
        Vector3 vel = other.attachedRigidbody.velocity;
        vel.y = Mathf.Sqrt(-2f * Physics.gravity.y * bounceHeight);
        other.attachedRigidbody.velocity = vel;
    }
}
