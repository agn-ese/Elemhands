using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obiettivo : MonoBehaviour
{
    public UnityEvent onTriggerPlayer;
    public AudioSource BubbleSound;

    private void Start()
    {
        if (BubbleSound != null && !BubbleSound.isPlaying)
        {
            BubbleSound.loop = true;
            BubbleSound.Play();
        } 
    }

    public void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            onTriggerPlayer.Invoke();

            if (BubbleSound != null && BubbleSound.isPlaying)
            {
                BubbleSound.Stop();
            }
        }
    }
}
