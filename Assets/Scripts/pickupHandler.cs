using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class pickupHandler : MonoBehaviour
{
    public player p;

    // Start is called before the first frame update
    void Start()
    {
        last = Time.time;
        p.iactions.Interaction.Pickup.performed += ctx => TriggerPickup();
    }


    Rigidbody holding;

    public float checkRadius = 3.5f;
    public LayerMask pickupable;
    public Transform heldItemPosition;
    Vector3 heldvel;
    float last = 0f;



    public void TriggerPickup()
    {
        if (Time.time - last < 0.2f) return;
        last = Time.time;
        Debug.Log("picking up");
        if (holding)
        {
            //Throw
            holding.transform.parent = null;
            holding.isKinematic = false;
            holding.GetComponent<Collider>().enabled = true;
            Seed seed;
            if (holding.TryGetComponent(out seed))
            {
                
                seed.ThrowSeed(p.rb.velocity, transform.forward);
            }

            holding = null;
            return;
        }

        Collider[] touching = Physics.OverlapSphere(transform.position, checkRadius, pickupable);
        if (touching.Length == 0) return;

        Array.Sort(touching, (a, b) =>
        {
            return (int)Mathf.Sign(Vector3.Dot((a.transform.position - p.transform.position).normalized, transform.forward) 
                - Vector3.Dot((b.transform.position - p.transform.position).normalized, transform.forward));
        });


        holding = touching[0].attachedRigidbody;
        holding.isKinematic = true;
        holding.transform.parent = heldItemPosition;
        holding.transform.localPosition = Vector3.zero;
        holding.GetComponent<Collider>().enabled = false;
        

    }

    private void Update()
    {
        /*
        if (holding)
        {
            
            holding.transform.position = heldItemPosition.position;
        }
        */
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
