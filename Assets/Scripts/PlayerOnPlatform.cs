using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnPlatform : MonoBehaviour
{
    public bool onPlatform = false;
    public bool rocciaOnPlatform = false;
    public Transform roccia;
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
        }
        if (other.CompareTag("Roccia") && transform.name == "Cylinder (1)")
        {
            roccia = other.GetComponent<Transform>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlatform = false;
        }
        if (other.CompareTag("Roccia") && transform.name == "Cylinder (1)")
            roccia = null;
    }
}
