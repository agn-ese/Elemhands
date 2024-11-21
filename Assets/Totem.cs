using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    public TutorialManager tutorialManager;

    private void OnTriggerEnter(Collider other)
    {
        // se ha il componente solleva
        if (other.GetComponent<Solleva>())
        {
            // faccio togliere il sollevamento
            other.GetComponent<Solleva>().RilasciaOggetto();
        }

        if (other.gameObject.tag == this.gameObject.tag)
        {
            switch (this.gameObject.tag)
            {
                case "Roccia":
                    tutorialManager.rockTotem = true;
                    GetComponent<FMODUnity.StudioEventEmitter>().Play();
                    tutorialManager.CheckTotem();
                    break;
                case "Erba":
                    tutorialManager.grassTotem = true;
                    tutorialManager.DialogoTutorialTerraFine();
                    GetComponent<FMODUnity.StudioEventEmitter>().Play();
                    tutorialManager.CheckTotem();
                    break;
                case "Sabbia":
                    tutorialManager.sandTotem = true;
                    GetComponent<FMODUnity.StudioEventEmitter>().Play();
                    tutorialManager.CheckTotem();
                    break;
            }
        }
    }
}
