using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    Input iactions;
    Rigidbody rb;
    public Transform cameraParent;
    public Transform camera;
    float angle = 0f;
    public float speed;
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

    // Start is called before the first frame update
    void Awake()
    {
        iactions = new Input();
        iactions.Enable();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float camHorizontal = iactions.Camera.Horizontal.ReadValue<float>();
        cameraParent.Rotate(new Vector3(0f, cameraSpeed * camHorizontal, 0f));
        angle -= iactions.Camera.Vertical.ReadValue<float>()*cameraSpeed;
        angle = Mathf.Clamp(angle, -90f, 90f);
        camera.localRotation = Quaternion.Euler(angle, 0f, 0f);
        if (!jumping) jumping = iactions.Movement.Jump.triggered;
        bool grounded = Physics.CheckSphere(transform.position + Vector3.up * sphereHeight, sphereRadius, ground);

        camera.localPosition = new Vector3(0f, cameraBaseHeight + Mathf.Sin(Time.time * headBobFrequency) * headBobSize * (rb.velocity.magnitude / speed) * (grounded ? 1f : 0f), 0f);



    }
    private void FixedUpdate()
    {
        float oldyvel = rb.velocity.y;
        Vector2 relmovement = new Vector2(iactions.Movement.Horizontal.ReadValue<float>(), iactions.Movement.Vertical.ReadValue<float>());

        Vector3 movement = (cameraParent.right * relmovement.x + cameraParent.forward * relmovement.y);
        movement.y = 0f;
        movement = movement.normalized;
        Vector3 newvel = Vector3.SmoothDamp(rb.velocity, movement * speed, ref velvel, 0.1f);
        bool grounded = Physics.CheckSphere(transform.position + Vector3.up * sphereHeight, sphereRadius, ground);
        groundIndicator.SetActive(grounded);

        if (grounded && jumping)
        {
            oldyvel = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            jumping = false;
        }

        newvel.y = oldyvel;
        rb.velocity = newvel;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + Vector3.up * sphereHeight, sphereRadius);
    }
}
