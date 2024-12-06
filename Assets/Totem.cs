using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    public TutorialManager tutorialManager;
    public AudioSource CorrectSrc;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se l'oggetto ha lo stesso tag del totem
        if (other.gameObject.tag == this.gameObject.tag)
        {
            if (other.GetComponent<Solleva>()) other.GetComponent<Solleva>().RilasciaOggetto();
            // Gestisci il caso in base al tag del totem
            switch (this.gameObject.tag)
            {
                case "Roccia":
                    tutorialManager.rockTotem = true;
                    // Se ha il componente "Solleva"
                    other.transform.localPosition = new Vector3(21.9876f, 0.6058649f, 14.01956f);
                    other.transform.localRotation = Quaternion.Euler(-90f, 0, 0);
                    // Riproduci il suono di feedback corretto
                    if (CorrectSrc != null)
                    {
                        CorrectSrc.Play();
                    }
                    tutorialManager.CheckTotem();
                    break;

                case "Erba":
                    tutorialManager.grassTotem = true;
                    other.transform.localPosition = new Vector3(21.96f, 0.5051451f, 19.02f);
                    other.transform.localRotation = Quaternion.Euler(0, 0, 0);
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
                    other.transform.localPosition = new Vector3(22.03f, 0.5675723f, 9.03f);
                    other.transform.localRotation = Quaternion.Euler(-90f, 0, 180f);
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
