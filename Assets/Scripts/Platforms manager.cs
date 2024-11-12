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
    private bool onRoccia = false;
    private List<Transform> PlatformList;
    private bool onBox = false;
    private bool lastOnPlatform2 = false;
    private bool lastOnPlatform3 = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (roccia1.GetComponent<PlayerOnPlatform>().onPlatform || roccia2.GetComponent<PlayerOnPlatform>().onPlatform || roccia3.GetComponent<PlayerOnPlatform>().onPlatform || roccia4.GetComponent<PlayerOnPlatform>().onPlatform || roccia5.GetComponent<PlayerOnPlatform>().onPlatform)
        {
            onRoccia = true;
        } else
        {
            onRoccia = false;
        }

        // if player on platform1, activate platform 2
        if (platform1.GetComponent<PlayerOnPlatform>().onPlatform)
        {
            lastOnPlatform2 = false;
            lastOnPlatform3 = false;
            platform2.GetComponent<TeleportInteractable>().enabled = true;
        } 

        if (platform2.GetComponent<PlayerOnPlatform>().onPlatform )
        {
            lastOnPlatform2 = true;
            lastOnPlatform3 = false;
        }
        
        if(lastOnPlatform2 && onRoccia)
        {
            platform3.GetComponent<TeleportInteractable>().enabled = true;
        } 



        // if player on platform3, player has the ability to go to the next platform and to the previous
        if (platform3.GetComponent<PlayerOnPlatform>().onPlatform )
        {
            lastOnPlatform2 = false;
            lastOnPlatform3 = true;
            //platform2.GetComponent<TeleportInteractable>().enabled = true;
        }
        /*else
        {
            platform2.GetComponent<TeleportInteractable>().enabled = false;
        }*/

        if(lastOnPlatform3 && onRoccia)
        {
            platform4.GetComponent<TeleportInteractable>().enabled = true;
        } else
        {
            platform4.GetComponent<TeleportInteractable>().enabled = false;
        }


        if (platform4.GetComponent<PlayerOnPlatform>().onPlatform )
        {
            lastOnPlatform2 = false;
            lastOnPlatform3 = false;
            platform2.GetComponent<TeleportInteractable>().enabled = false;
            //platform3.GetComponent<TeleportInteractable>().enabled = true;
        }

        if (!platform4.GetComponent<PlayerOnPlatform>().onPlatform && !platform3.GetComponent<PlayerOnPlatform>().onPlatform && !platform2.GetComponent<PlayerOnPlatform>().onPlatform && !platform1.GetComponent<PlayerOnPlatform>().onPlatform
            && onRoccia == false)
        {
            platform2.GetComponent<TeleportInteractable>().enabled = false;
            platform3.GetComponent<TeleportInteractable>().enabled = false;
            platform4.GetComponent<TeleportInteractable>().enabled = false;
        }
    }


}
