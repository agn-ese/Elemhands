using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    [SerializeField] private float abbassamentoRoccia = 7f;
    [SerializeField] private float abbassamentoErba = 5f;
    [SerializeField] private float abbassamentoSabbia = 2f;
    [SerializeField] private float forzaEspulsione = 10f; // Forza con cui l'oggetto viene espulso
    [SerializeField] private float durataAnimazione = 1f; // Durata dell'animazione di movimento

    private Transform sopra;
    private GameObject oggettoDentro; // Variabile per tenere traccia dell'oggetto attualmente all'interno
    private GameObject oggettoDentroPrecedente;

    private void Start()
    {
        // prendi il figlio "Sopra" del geyser
        sopra = transform.Find("Sopra");
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Roccia" || other.gameObject.tag == "Erba" || other.gameObject.tag == "Sabbia") && other.gameObject != oggettoDentro)
        {
            float abbassamento = 0f;

            switch (other.gameObject.tag)
            {
                case "Roccia":
                    abbassamento = abbassamentoRoccia;
                    break;
                case "Erba":
                    abbassamento = abbassamentoErba;
                    break;
                case "Sabbia":
                    abbassamento = abbassamentoSabbia;
                    break;
            }

            transform.position = new Vector3(transform.position.x, transform.position.y - abbassamento, transform.position.z);
            Debug.LogError("Mi sono abbassato di " + abbassamento);
        }

        if (other.GetComponent<Solleva>() && other.gameObject != oggettoDentro)
        {
            Debug.LogError("qualcosa è entrato");

            // faccio togliere il sollevamento
            other.GetComponent<Solleva>().RilasciaOggetto();

            // Se c'è già un oggetto dentro, espellilo
            if (oggettoDentro != null && oggettoDentro != other.gameObject)
            {
                oggettoDentroPrecedente = oggettoDentro;
                oggettoDentro = other.gameObject;

                EspelliOggetto(oggettoDentroPrecedente);
            }

            if (oggettoDentro == null)
            {
                oggettoDentro = other.gameObject;
            }

            // Avvia l'animazione di movimento
            StartCoroutine(MuoviOggettoAlPuntoSopra(oggettoDentro));
        }


    }

    private void OnTriggerExit(Collider other)
    {
        // caso in cui il player rimuove volontariamente l'oggetto
        if (other.gameObject == oggettoDentro)
        {
            Debug.LogError("Oggetto uscito, geyser abbassato");
            // rialza il geyser in base al tipo di oggetto
            if (other.tag == "Roccia")
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + abbassamentoRoccia, transform.position.z);
            }
            else if (other.tag == "Erba")
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + abbassamentoErba, transform.position.z);
            }
            else if (other.tag == "Sabbia")
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + abbassamentoSabbia, transform.position.z);
            }

            oggettoDentro = null;
            oggettoDentroPrecedente = null;
        }
    }

    private IEnumerator MuoviOggettoAlPuntoSopra(GameObject oggetto)
    {
        Vector3 posizioneIniziale = oggetto.transform.position;
        Vector3 posizioneFinaleXZ = new Vector3(sopra.position.x, posizioneIniziale.y, sopra.position.z);
        Vector3 posizioneFinale = sopra.position;
        float tempoTrascorso = 0f;

        //oggetto.GetComponent<Collider>().enabled = false;

        // Muovi l'oggetto verso la posizione finale nelle coordinate x e z
        while (tempoTrascorso < durataAnimazione / 2)
        {
            oggetto.transform.position = Vector3.Lerp(posizioneIniziale, posizioneFinaleXZ, tempoTrascorso / durataAnimazione);
            tempoTrascorso += Time.deltaTime;
            yield return null;
        }

        // Reset tempoTrascorso per la seconda fase del movimento
        tempoTrascorso = 0f;
        posizioneIniziale = oggetto.transform.position;

        // Muovi l'oggetto verso la posizione finale nella coordinata y
        while (tempoTrascorso < durataAnimazione)
        {
            oggetto.transform.position = Vector3.Lerp(posizioneIniziale, posizioneFinale, tempoTrascorso / durataAnimazione);
            oggetto.transform.rotation = Quaternion.Lerp(oggetto.transform.rotation, sopra.rotation, tempoTrascorso / durataAnimazione);
            tempoTrascorso += Time.deltaTime;
            yield return null;
        }

        oggetto.transform.position = sopra.position;

        //oggetto.GetComponent<Collider>().enabled = true;
    }

    private void EspelliOggetto(GameObject oggetto)
    {
        // Trova il giocatore
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("Player non trovato!");
            return;
        }

        // Espelli l'oggetto verso il giocatore
        Rigidbody rb = oggetto.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            Vector3 direzioneEspulsione = (player.transform.position - oggetto.transform.position).normalized;
            direzioneEspulsione = new Vector3(direzioneEspulsione.x, Mathf.Max(direzioneEspulsione.y, 0.5f), direzioneEspulsione.z).normalized;
            // disegna retta di debug
            Debug.DrawRay(oggetto.transform.position, direzioneEspulsione * 10f, Color.red, 2f);
            rb.AddForce(direzioneEspulsione * forzaEspulsione, ForceMode.Impulse);
        }

        Debug.LogError("Oggetto " + oggetto.name + " espulso, geyser abbassato");
        // rialza il geyser in base al tipo di oggetto
        if (oggetto.tag == "Roccia")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + abbassamentoRoccia, transform.position.z);
        }
        else if (oggetto.tag == "Erba")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + abbassamentoErba, transform.position.z);
        }
        else if (oggetto.tag == "Sabbia")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + abbassamentoSabbia, transform.position.z);
        }


    }
}