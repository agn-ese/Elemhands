using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    public TutorialManager tutorialManager;
    public AudioSource CorrectSrc;

    private void OnTriggerEnter(Collider other)
    {
        // Se ha il componente "Solleva"
        if (other.GetComponent<Solleva>())
        {
            // Rilascia l'oggetto sollevato
            other.GetComponent<Solleva>().RilasciaOggetto();
        }

        // Verifica se l'oggetto ha lo stesso tag del totem
        if (other.gameObject.tag == this.gameObject.tag)
        {
            // Gestisci il caso in base al tag del totem
            switch (this.gameObject.tag)
            {
                case "Roccia":
                    tutorialManager.rockTotem = true;
                    // Riproduci il suono di feedback corretto
                    if (CorrectSrc != null)
                    {
                        CorrectSrc.Play();
                    }
                    tutorialManager.CheckTotem();
                    break;

                case "Erba":
                    tutorialManager.grassTotem = true;
                    // Riproduci il suono di feedback corretto
                    if (CorrectSrc != null)
                    {
                        CorrectSrc.Play();
                    }
                    tutorialManager.DialogoTutorialTerraFine();
                    tutorialManager.CheckTotem();
                    break;

                case "Sabbia":
                    tutorialManager.sandTotem = true;
                    // Riproduci il suono di feedback corretto
                    if (CorrectSrc != null)
                    {
                        CorrectSrc.Play();
                    }
                    tutorialManager.CheckTotem();
                    break;
            }
        }
    }
}
