using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Input iactions;
    Rigidbody rb;
    public Transform cameraParent;
    public Transform camera;
    float angle = 0f;
    public AnimationCurve speed;
    public float cameraSpeed;
    public float jumpHeight;
    Vector3 velvel;

    public float headBobSize;
    public float headBobFrequency;
    public float cameraBaseHeight = 0.25f;
    public float sphereHeight;
    public float sphereRadius;
    public LayerMask ground;
    public GameObject groundIndicator;
    bool jumping;

    [System.NonSerialized]
    bool airStunned = false;
    [System.NonSerialized]
    float lastStunned;
    [System.NonSerialized]
    float jumpTime;
    [System.NonSerialized]
    float jumpBufferTime;


    float speedCursor = 0f;
    public float maxSpeedDamp = 0.2f;
    float speedCursorVel;

    public Action OnDeath;

    CheckpointManager _checkpointManager;


    public void Stun()
    {
        airStunned = true;
        lastStunned = Time.time;
    }

    // Start is called before the first frame update
    void Awake()
    {
        lastStunned = Time.time - 0.2f;
        jumpTime = Time.time - 0.4f;
        jumpBufferTime = Time.time - 0.4f;
        iactions = new Input();
        iactions.Enable();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        // Connect Checkpoint Manager
        _checkpointManager = CheckpointManager.Instance;
        OnDeath += () => _checkpointManager.RespawnPlayerOnDeath(transform);
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = new Ray(transform.position, Vector3.up * sphereHeight);
        RaycastHit hit;

        bool grounded = Physics.SphereCast(r, sphereRadius, out hit, Mathf.Abs(sphereHeight) + sphereRadius, ground);
        float camHorizontal = iactions.Camera.Horizontal.ReadValue<float>();
        cameraParent.Rotate(new Vector3(0f, cameraSpeed * camHorizontal, 0f));
        transform.up = Vector3.Slerp(transform.up,Vector3.Slerp(grounded ? hit.normal : Vector3.up,Vector3.up,0.8f),0.05f);
        angle -= iactions.Camera.Vertical.ReadValue<float>()*cameraSpeed;
        angle = Mathf.Clamp(angle, -90f, 90f);
        camera.localRotation = Quaternion.Euler(angle, 0f, 0f);
        if (!jumping)
        {
            jumping = iactions.Movement.Jump.triggered;
            if (jumping) jumpBufferTime = Time.time;
        }
        
        float s = new Vector2(rb.velocity.x, rb.velocity.z).magnitude;

        camera.localPosition = new Vector3(0f, cameraBaseHeight + Mathf.Sin(Time.time * headBobFrequency) * headBobSize * (s / speed.Evaluate(speedCursor)) * (grounded ? 1f : 0f), 0f);



    }
    private void FixedUpdate()
    {
        rb.useGravity = true;
        float oldyvel = rb.velocity.y;
        Vector2 relmovement = new Vector2(iactions.Movement.Horizontal.ReadValue<float>(), iactions.Movement.Vertical.ReadValue<float>());

        Vector3 movement = (cameraParent.right * relmovement.x + cameraParent.forward * relmovement.y).normalized;
        //movement.y = 0f;
        //movement = movement.normalized;
        Ray r = new Ray(transform.position, Vector3.up * sphereHeight);
        RaycastHit hit;

        bool grounded = Physics.SphereCast(r, sphereRadius, out hit, Mathf.Abs(sphereHeight) + sphereRadius, ground);


        float s = Vector3.Scale(rb.velocity,ToTangent(rb.velocity,grounded ? hit.normal : Vector3.up)).magnitude;

        //Debug.Log(s);

        speedCursor = s > speedCursor ? s : Mathf.SmoothDamp(speedCursor, s, ref speedCursorVel, maxSpeedDamp);
        Vector3 newvel = Vector3.SmoothDamp(rb.velocity, ToTangent(movement,grounded ? hit.normal : Vector3.up) * movement.magnitude * speed.Evaluate(speedCursor), ref velvel, 0.1f);

        
        if (grounded && Time.time - 0.2f > lastStunned) airStunned = false;
        if (airStunned) return;

        groundIndicator.SetActive(grounded);


        if (Time.time - jumpBufferTime > 0.4f) jumping = false;

        if (grounded && jumping)
        {
            newvel.y = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            jumping = false;
            jumpTime = Time.time;
        }




        if (!grounded)
        {
            newvel.y = oldyvel;
            jumpTime = Time.time - 1f;
        }
        rb.velocity = newvel;
        if (grounded && (Time.time - jumpTime > 0.4f))
        {
            rb.useGravity = false;
            //rb.velocity += hit.normal * -5f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + Vector3.up * sphereHeight, sphereRadius);
    }

    Vector3 ToTangent(Vector3 v, Vector3 targ)
    {
        return Vector3.Cross(Vector3.Cross(targ,v), targ).normalized;
    }
}
