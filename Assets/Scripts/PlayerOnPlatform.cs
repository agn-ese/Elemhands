using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnPlatform : MonoBehaviour
{
    public bool onPlatform = false;
    public Transform roccia;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            onPlatform = true;
        }
        if (other.CompareTag("Roccia") )
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
        if (other.CompareTag("Roccia") )
            roccia = null;
    }
}
