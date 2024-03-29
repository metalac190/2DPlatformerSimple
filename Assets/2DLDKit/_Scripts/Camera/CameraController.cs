using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera _camera = null;
    [SerializeField][Tooltip("Technically this is the 'Size' property on the camera" +
        "but it effectively simulates distance in 2D Orthographic projection")] 
    private float _cameraDistance = 5;
    [SerializeField]
    [Tooltip("Vertical distance camera centers to away from player. 0 centers player")]
    private float _defaultVerticalOffset = 0;
    [SerializeField]
    [Tooltip("Horizontal distance camera centers to away from player. 0 centers player")]
    private float _defaultHorizontalOffset = 0;

    [Header("Smoothing")]
    [SerializeField]
    private bool _useSmoothing = false;
    [SerializeField]
    [Range(3,7)]
    private float _smoothSpeed = 5;

    private float _yOffset = 0;
    private float _xOffset = 0;
    private float _zOffset = 0;

    private Vector3 _offset;
    private Transform _objectToFollow = null;

    private void Awake()
    {
        FindCameraIfEmpty();
        CalculateOffsets();
        _camera.orthographicSize = _cameraDistance;
    }

    private void FindCameraIfEmpty()
    {
        // if we didn't find a camera, find it automatically
        if (_camera == null)
        {
            _camera = GetComponentInChildren<Camera>();
            // if we STILL can't find a camera, it's an error in setup
            if (_camera == null)
            {
                Debug.LogWarning("No camera set on the Camera Rig!");
            }
        }
    }

    private void CalculateOffsets()
    {
        // save our defaults into our current. This way we can return to defaults
        _xOffset = _defaultHorizontalOffset;
        _yOffset = _defaultVerticalOffset;
        // we don't change z in 2D, so just retain starting value
        _zOffset = transform.position.z;
        // store it into a Vector3 for easier manipulation
        _offset = new Vector3(_xOffset, _yOffset, _zOffset);
    }

    private void LateUpdate()
    {
        // if object is specified, move the camera
        if(_objectToFollow != null)
        {
            if (_useSmoothing)
            {
                Vector3 targetPosition = _objectToFollow.position + _offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position,
                    targetPosition, _smoothSpeed * Time.deltaTime);
                // move the camera to the new position
                transform.position = smoothedPosition;
            }
            else
            {
                transform.position = _objectToFollow.position + _offset;
            }
        }
    }

    public void FollowNewTarget(Transform newTarget)
    {
        _objectToFollow = newTarget;
    }
}
