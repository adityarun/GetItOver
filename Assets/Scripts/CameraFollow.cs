using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField, Range(0,1)] private float _smoothTime;

    private Vector3 offset;
    private Vector3 cameraVelocity = Vector3.zero;
    void Start()
    {
        offset = transform.position /*- _player.transform.position*/;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetCameraPos = _player.transform.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetCameraPos,ref cameraVelocity,_smoothTime);
    }
}
