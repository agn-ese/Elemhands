using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnPlatform : MonoBehaviour
{
    public bool onPlatform = false;
    public bool roccia = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            onPlatform = true;
            if (transform.CompareTag("Roccia"))
                roccia = true;
            else
                roccia = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlatform = false;
        }
    }
}
