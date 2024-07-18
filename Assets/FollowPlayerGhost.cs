using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerGhost : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float yPos = 0.0f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(objectToFollow.transform.position.x, yPos, objectToFollow.transform.position.z) + offset;
        transform.rotation = objectToFollow.rotation;

    }


}
