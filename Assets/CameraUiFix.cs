using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUiFix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Camera>().enabled = false;
        this.GetComponent<Camera>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
