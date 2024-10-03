using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerGhost : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    //[SerializeField] private Vector3 offset;

    //[SerializeField] private float yPos = 0.0f;

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(objectToFollow.transform.position.x, yPos, objectToFollow.transform.position.z) + offset;
        transform.rotation = objectToFollow.rotation;
        //transform.position = Camera.main.ScreenToWorldPoint( new Vector3(Screen.width* offset.x, Screen.height*offset.y, offset.z));
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * (- 1/4), Screen.height * (-2.5f), 0.5f));
        Debug.Log(Screen.width + " " + Screen.height);

    }


}
