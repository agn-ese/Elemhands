using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class InsertKey : MonoBehaviour
{
    [SerializeField] private SplineAnimate spline;
    [SerializeField] private AudioSource sound;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Key"))
        {
            spline.Play();
            sound.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Key"))
        {
            spline.Play();
            sound.Play();
        }
    }
}
