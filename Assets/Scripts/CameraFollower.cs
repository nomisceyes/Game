using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Camera _camera;
    private Animator _animator;

    private float _smoothTime = 0.2f;
    private float _zoomSpeed = 2f;
    private ContactFilter2D _ground;

    private Vector3 _velocity = Vector3.zero;
   
    private void Start()
    {
        _camera = GetComponent<Camera>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
            _camera.orthographicSize += Input.mouseScrollDelta.y * -_zoomSpeed;
        
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y + 3, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
}
