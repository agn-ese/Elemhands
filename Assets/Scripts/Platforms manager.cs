using Oculus.Interaction.Locomotion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField]private bool onRoccia1 = false;
    [SerializeField]private bool onRoccia3 = false;
    [SerializeField]private bool onRoccia4 = false;
    private List<Transform> PlatformList;
    private bool lastOnPlatform1 = false;
    private bool lastOnPlatform3 = false;
    private bool lastOnPlatform4 = false;

    [SerializeField] private Transform secondArea;

    // Variables to not consider conditions for every frame
    private float timeToWait = 1f;
    private float time = 0;
    private bool ready = false;

    Transform[] roccias;
    
    public void Start() {
        roccias = new Transform[] { roccia1, roccia2, roccia3, roccia4 };
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

        //Check if any Roccia is on platform

        onRoccia1 = IsAnyRocciaOnPlatform(platform1, roccias); //Check if any Roccia is on platform1
        onRoccia3 = IsAnyRocciaOnPlatform(platform3, roccias); //Check if any Roccia is on platform3
        onRoccia4 = IsAnyRocciaOnPlatform(platform4, roccias); //Check if any Roccia is on platform4

        if (ready)
        {
            if (platform1.GetComponent<PlayerOnPlatform>().onPlatform) //Cioè se il player è sulla piattaforma 1
            {
                lastOnPlatform1 = true; //Allora l'ultima piattaforma su cui è stato il player è la piattaforma 1
            }

            if (lastOnPlatform1 && onRoccia1) //Cioè se il player è sulla piattaforma 1 e c'è una roccia sulla piattaforma 1
            {
                SetTeleportInteractable(platform2, true); //Abilita il TeleportInteractable della piattaforma 2
            }

            if (platform2.GetComponent<PlayerOnPlatform>().onPlatform) //Cioè se il player è sulla piattaforma 2
            {
                SetTeleportInteractable(platform3, true); //Abilita il TeleportInteractable della piattaforma 3
                lastOnPlatform1 = false; //L'ultima piattaforma su cui è stato il player non è più la piattaforma 1
                lastOnPlatform3 = false; //E nemmeno la piattaforma 3
            }

            if (platform3.GetComponent<PlayerOnPlatform>().onPlatform) //Cioè se il player è sulla piattaforma 3
            {
                lastOnPlatform3 = true; //Allora l'ultima piattaforma su cui è stato il player è la piattaforma 3
                lastOnPlatform4 = false; //E non è la piattaforma 4
            }

            if (lastOnPlatform3 && onRoccia3) //Cioè se l'ultima piattaforma su cui è stato l'utente è la 3 e c'è una roccia sulla piattaforma 3
            {
                SetTeleportInteractable(platform4, true); //Abilita il TeleportInteractable della piattaforma 4
            }

            if (platform4.GetComponent<PlayerOnPlatform>().onPlatform) //Cioè se il player è sulla piattaforma 4
            {
                lastOnPlatform3 = false; //L'ultima piattaforma su cui è stato il player non è più la piattaforma 3
                lastOnPlatform4 = true; //Ma è la piattaforma 4
            }

            if (lastOnPlatform4 && onRoccia4) //Cioè se l'ultima piattaforma su cui è stato l'utente è la 4 e c'è una roccia sulla piattaforma 4
            {
                SetTeleportInteractable(platform5, true); //Abilita il TeleportInteractable della piattaforma 5
            }

            if (platform5.GetComponent<PlayerOnPlatform>().onPlatform) //Cioè se il player è sulla piattaforma 5    
            {
                lastOnPlatform4 = false; //L'ultima piattaforma su cui è stato il player non è più la piattaforma 4
            }

            if (!platform1.GetComponent<PlayerOnPlatform>().onPlatform && !platform2.GetComponent<PlayerOnPlatform>().onPlatform &&
                !platform3.GetComponent<PlayerOnPlatform>().onPlatform && !platform4.GetComponent<PlayerOnPlatform>().onPlatform &&
                !platform5.GetComponent<PlayerOnPlatform>().onPlatform && !lastOnPlatform1 && !lastOnPlatform3 && !lastOnPlatform4) //Cioè se il player non è su nessuna piattaforma
            {
                SetTeleportInteractable(platform2, false);
                SetTeleportInteractable(platform3, false);
                SetTeleportInteractable(platform4, false);
                SetTeleportInteractable(platform5, false); //Disabilita tutti i TeleportInteractable
            }

            ready = false;
            time = 0;
        }
    }

    public void OpenNewArea() //Funzione per aprire la seconda area
    {
        secondArea.GetComponent<TeleportInteractable>().enabled = true; //Abilita il TeleportInteractable della seconda area
    }

    private bool IsAnyRocciaOnPlatform(Transform platform, Transform[] roccias) //Funzione per controllare se c'è una roccia su una piattaforma
    {
        foreach (var roccia in roccias) //Per ogni roccia
        {
            if (platform.GetComponent<PlayerOnPlatform>().roccia == roccia && roccia.GetComponent<PlayerOnPlatform>().onPlatform) //Se la roccia sulla piattaforma è la roccia corrente e la roccia è sulla piattaforma
            {
                return true;
            }
        }
        return false;
    }

    private void SetTeleportInteractable(Transform platform, bool enabled) //Funzione per abilitare/disabilitare il TeleportInteractable di una piattaforma
    {
        platform.GetComponent<TeleportInteractable>().enabled = enabled; //Abilita/disabilita il TeleportInteractable della piattaforma
    }
}
