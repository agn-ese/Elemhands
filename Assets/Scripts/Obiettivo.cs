using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obiettivo : MonoBehaviour
{
    public UnityEvent onTriggerPlayer;

    public void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            onTriggerPlayer.Invoke();
        }
    }
}
