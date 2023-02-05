using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class bouncePad : MonoBehaviour
{
    const int previewLength = 15;
    const float previewRes = 5f;
    const float previewRayRadius = 1f;

    public Vector3 bounceForce;

    [Header("Axes to Override")]
    public bool x, y, z;


    public bool stun;

    public AudioClip sound;


    void OnTriggerEnter(Collider other)
    {
        Vector3 vel = other.attachedRigidbody.velocity;

        vel.x = x ? bounceForce.x : vel.x + bounceForce.x;
        vel.y = y ? bounceForce.y : vel.y + bounceForce.y;
        vel.z = z ? bounceForce.z : vel.z + bounceForce.z;


        other.attachedRigidbody.velocity = vel;

        player p;
        if (stun && other.TryGetComponent(out p)) p.Stun();
        soundPool.Play(sound, transform.position,0.5f);
    }


    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = bounceForce;
        float delta = 1f / previewRes;
        for (float i = 0; i < previewLength*previewRes; i++)
        {
            Ray r = new Ray(pos, velocity);
            RaycastHit hit;

            velocity += Physics.gravity * delta;
            Gizmos.DrawRay(pos, velocity * delta);

            if (Physics.SphereCast(r,previewRayRadius,out hit,velocity.magnitude*delta))
            {
                Gizmos.DrawSphere(hit.point, previewRayRadius);
                break;
            }

            
            pos += velocity * delta;

        }
    }
}
