using UnityEngine;

/// <summary>
/// Rotate Canvas to face player
/// </summary>
public class FacePlayer : MonoBehaviour
{
    // Cache Cam
    Camera _cam;

    Transform _selectedTransform;
    Transform _transform;

    void Start()
    {
        _cam = Camera.main;
        _selectedTransform = _cam.transform;
        _transform = GetComponent<Transform>();
    }

    void Update() => _transform.LookAt(_selectedTransform);
}
