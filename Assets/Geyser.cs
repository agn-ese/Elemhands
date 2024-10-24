using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    [SerializeField] private float abbassamentoRoccia = 7f;
    [SerializeField] private float abbassamentoErba = 5f;
    [SerializeField] private float abbassamentoSabbia = 2f;

    private Transform sopra;

    private void Start()
    {
        sopra = transform.GetChild(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // se ha il componente solleva
        if (other.GetComponent<Solleva>())
        {
            // faccio togliere il sollevamento
            other.GetComponent<Solleva>().RilasciaOggetto();
        }

        // se entro in collisione con un oggetto col tag "Roccia"
        if (other.gameObject.tag == "Roccia")
        {
            // abbasso il geyser
            transform.position = new Vector3(transform.position.x, transform.position.y - abbassamentoRoccia, transform.position.z);

            // posiziono la roccia dove si trova "sopra"
            other.transform.position = new Vector3(sopra.position.x, sopra.position.y, sopra.position.z);
        }

        // se entro in collisione con un oggetto col tag "Erba"
        if (other.gameObject.tag == "Erba")
        {
            // abbasso il geyser
            transform.position = new Vector3(transform.position.x, transform.position.y - abbassamentoErba, transform.position.z);

            other.transform.position = new Vector3(sopra.position.x, sopra.position.y, sopra.position.z);
        }

        // se entro in collisione con un oggetto col tag "Sabbia"
        if (other.gameObject.tag == "Sabbia")
        {
            // abbasso il geyser
            transform.position = new Vector3(transform.position.x, transform.position.y - abbassamentoSabbia, transform.position.z);

            other.transform.position = new Vector3(sopra.position.x, sopra.position.y, sopra.position.z);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // se esco dalla collisione con un oggetto col tag "Roccia"
        if (other.gameObject.tag == "Roccia")
        {
            // riporto il geyser alla sua posizione originale
            transform.position = new Vector3(transform.position.x, transform.position.y + abbassamentoRoccia, transform.position.z);
        }

        // se esco dalla collisione con un oggetto col tag "Erba"
        if (other.gameObject.tag == "Erba")
        {
            // riporto il geyser alla sua posizione originale
            transform.position = new Vector3(transform.position.x, transform.position.y + abbassamentoErba, transform.position.z);
        }

        // se esco dalla collisione con un oggetto col tag "Sabbia"
        if (other.gameObject.tag == "Sabbia")
        {
            // riporto il geyser alla sua posizione originale
            transform.position = new Vector3(transform.position.x, transform.position.y + abbassamentoSabbia, transform.position.z);
        }


    }
}
