using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerGhost : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = objectToFollow.rotation;
        Vector3 targetPosition = objectToFollow.TransformPoint(offset);
        transform.position = targetPosition;
    }
}
