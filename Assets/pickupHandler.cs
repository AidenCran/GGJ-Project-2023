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
        p.iactions.Interaction.Pickup.performed += ctx => TriggerPickup();
    }


    Rigidbody holding;

    public float checkRadius = 3.5f;
    public LayerMask pickupable;
    public Transform heldItemPosition;
    Vector3 heldvel;



    public void TriggerPickup()
    {
        Debug.Log("picking up");
        if (holding)
        {
            //Throw
            //holding.isKinematic = false;
            holding.GetComponent<Collider>().enabled = true;

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
        //holding.isKinematic = true;
        holding.GetComponent<Collider>().enabled = false;
        

    }

    private void FixedUpdate()
    {
        if (holding)
        {
            
            holding.velocity = (heldItemPosition.position-holding.position)/Time.deltaTime;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
