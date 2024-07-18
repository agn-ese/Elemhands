using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Camera camera;

    public float CameraDistance = 3.0F;
    public float smoothTime = 0.3F;
    public float yDifference = 0.5F;
    private Vector3 velocity = Vector3.zero;


    void Start()
    {

    }

    void Update()
    {

        Vector3 targetPosition = camera.transform.TransformPoint(new Vector3(0, yDifference, CameraDistance));

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        var lookAtPos = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        transform.LookAt(lookAtPos);
        transform.Rotate(0, 180, 0);


    }
}

