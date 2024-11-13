using Oculus.Interaction.Locomotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformsmanager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform platform1;
    [SerializeField] private Transform platform2;
    [SerializeField] private Transform platform3;
    [SerializeField] private Transform platform4;
    [SerializeField] private Transform platform5;
    [SerializeField] private Transform roccia1;
    [SerializeField] private Transform roccia2;
    [SerializeField] private Transform roccia3;
    [SerializeField] private Transform roccia4;
    [SerializeField] private Transform roccia5;
    private bool onRoccia1 = false;
    private bool onRoccia3 = false;
    private bool onRoccia4 = false;
    private List<Transform> PlatformList;
    private bool lastOnPlatform1 = false;
    [SerializeField]private bool lastOnPlatform3 = false;
    private bool lastOnPlatform4 = false;


    // Variables to not consider conditions for every frame
    private float timeToWait = 1f;
    private float time = 0;
    private bool ready = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!ready)
        {
            time += Time.deltaTime;
            if (time > timeToWait)
            {
                ready = true;
            }
        }

        // Check if any Roccia is on platform2
        if (platform1.GetComponent<PlayerOnPlatform>().roccia == roccia1 && roccia1.GetComponent<PlayerOnPlatform>().onPlatform)
        {
            onRoccia1 = true;
        }
        else if (platform1.GetComponent<PlayerOnPlatform>().roccia == roccia2 && roccia2.GetComponent<PlayerOnPlatform>().onPlatform)
        {
            onRoccia1 = true;
        }
        else if (platform1.GetComponent<PlayerOnPlatform>().roccia == roccia3 && roccia3.GetComponent<PlayerOnPlatform>().onPlatform)
        {
            onRoccia1 = true;
        }
        else if (platform1.GetComponent<PlayerOnPlatform>().roccia == roccia4 && roccia4.GetComponent<PlayerOnPlatform>().onPlatform)
        {
            onRoccia1 = true;
        }
        else if (platform1.GetComponent<PlayerOnPlatform>().roccia == roccia5 && roccia5.GetComponent<PlayerOnPlatform>().onPlatform)
        {
            onRoccia1 = true;
        }
        else
        {
            onRoccia1 = false;
        }

        if (platform3.GetComponent<PlayerOnPlatform>().roccia == roccia1 && roccia1.GetComponent<PlayerOnPlatform>().onPlatform)
        {
            onRoccia3 = true;
        }
        else if (platform3.GetComponent<PlayerOnPlatform>().roccia == roccia2 && roccia2.GetComponent<PlayerOnPlatform>().onPlatform)
        {
            onRoccia3 = true;
        }
        else if (platform3.GetComponent<PlayerOnPlatform>().roccia == roccia3 && roccia3.GetComponent<PlayerOnPlatform>().onPlatform)
        {
            onRoccia3 = true;
        }
        else if (platform3.GetComponent<PlayerOnPlatform>().roccia == roccia4 && roccia4.GetComponent<PlayerOnPlatform>().onPlatform)
        {
            onRoccia3 = true;
        }
        else if (platform3.GetComponent<PlayerOnPlatform>().roccia == roccia5 && roccia5.GetComponent<PlayerOnPlatform>().onPlatform)
        {
            onRoccia3 = true;
        }
        else
        {
            onRoccia3 = false;
        }

        if (platform4.GetComponent<PlayerOnPlatform>().roccia == roccia1 && roccia1.GetComponent<PlayerOnPlatform>().onPlatform)
        {
            onRoccia4 = true;
        }
        else if (platform4.GetComponent<PlayerOnPlatform>().roccia == roccia2 && roccia2.GetComponent<PlayerOnPlatform>().onPlatform)
        {
            onRoccia4 = true;
        }
        else if (platform4.GetComponent<PlayerOnPlatform>().roccia == roccia3 && roccia3.GetComponent<PlayerOnPlatform>().onPlatform)
        {
            onRoccia4 = true;
        }
        else if (platform4.GetComponent<PlayerOnPlatform>().roccia == roccia4 && roccia4.GetComponent<PlayerOnPlatform>().onPlatform)
        {
            onRoccia4 = true;
        }
        else if (platform4.GetComponent<PlayerOnPlatform>().roccia == roccia5 && roccia5.GetComponent<PlayerOnPlatform>().onPlatform)
        {
            onRoccia4 = true;
        }
        else
        {
            onRoccia4 = false;
        }

        if (ready)
        {

            if (platform1.GetComponent<PlayerOnPlatform>().onPlatform)
            {
                lastOnPlatform1 = true;
            }

            if (lastOnPlatform1 && onRoccia1)
            {
                platform2.GetComponent<TeleportInteractable>().enabled = true;
            }

            if (platform2.GetComponent<PlayerOnPlatform>().onPlatform)
            {
                platform3.GetComponent<TeleportInteractable>().enabled = true;
                lastOnPlatform1 = false;
                lastOnPlatform3 = false;
            }

            if (platform3.GetComponent<PlayerOnPlatform>().onPlatform)
            {
                lastOnPlatform3 = true;
                lastOnPlatform4 = false;
            }

            if (lastOnPlatform3 && onRoccia3)
            {
                platform4.GetComponent<TeleportInteractable>().enabled = true;
            }


            if (platform4.GetComponent<PlayerOnPlatform>().onPlatform)
            {
                lastOnPlatform3 = false;
                lastOnPlatform4 = true;
            }

            if(lastOnPlatform4 && onRoccia4)
            {
                platform5.GetComponent<TeleportInteractable>().enabled = true;
            }

            if (platform4.GetComponent<PlayerOnPlatform>().onPlatform)
            {
                lastOnPlatform4 = false;
            }

            if (!platform5.GetComponent<PlayerOnPlatform>().onPlatform && !platform4.GetComponent<PlayerOnPlatform>().onPlatform && !platform3.GetComponent<PlayerOnPlatform>().onPlatform && !platform2.GetComponent<PlayerOnPlatform>().onPlatform && !platform1.GetComponent<PlayerOnPlatform>().onPlatform
                &&  !lastOnPlatform1 && !lastOnPlatform4 && !lastOnPlatform3)
            {
                platform2.GetComponent<TeleportInteractable>().enabled = false;
                platform3.GetComponent<TeleportInteractable>().enabled = false;
                platform4.GetComponent<TeleportInteractable>().enabled = false;
                platform5.GetComponent <TeleportInteractable>().enabled = false;
            }

            ready = false;
            time = 0;
        }
    }


}
