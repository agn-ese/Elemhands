using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    [SerializeField] private float abbassamentoRoccia = 7f; // Abbassamento per la roccia
    [SerializeField] private float abbassamentoErba = 5f; // Abbassamento per l'erba
    [SerializeField] private float abbassamentoSabbia = 2f; // Abbassamento per la sabbia
    [SerializeField] private float forzaEspulsione = 10f; // Forza con cui l'oggetto viene espulso
    [SerializeField] private float durataAnimazione = 1f; // Durata dell'animazione di movimento

    [Header ("SuonoGeyser")]
    public AudioSource AudioSource;

    private Transform sopra; // Riferimento al punto sopra il geyser
    private GameObject oggettoDentro; // Variabile per tenere traccia dell'oggetto attualmente all'interno
    private GameObject oggettoDentroPrecedente; // Variabile per tenere traccia dell'oggetto precedentemente espulso
    private GameObject player; // Riferimento al giocatore

    private void Start()
    {
        //SUONO
        if (AudioSource != null)
        {
            AudioSource.Play();
        }

        // Prendi il figlio "Sopra" del geyser
        sopra = transform.Find("Sopra");

        // Prendi il giocatore
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("Player non trovato!");
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Se l'oggetto entrato è quello che è stato appena espulso, esci
        if (oggettoDentroPrecedente != null && other.gameObject == oggettoDentroPrecedente)
        {
            //Debug.LogError("Oggetto entrato, ma è quello che ho appena espulso");
            return;
        }

        // Se l'oggetto entrato è una roccia, erba o sabbia e non è già dentro
        if ((other.gameObject.tag == "Roccia" || other.gameObject.tag == "Erba" || other.gameObject.tag == "Sabbia") && other.gameObject != oggettoDentro)
        {
            float abbassamento = 0f;

            // Determina l'abbassamento in base al tipo di oggetto
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

            // Abbassa il geyser
            transform.position = new Vector3(transform.position.x, transform.position.y - abbassamento, transform.position.z);
            //Debug.LogError("Mi sono abbassato di " + abbassamento);
        }

        // Se l'oggetto ha il componente "Solleva" e non è già dentro
        if (other.GetComponent<Solleva>() && other.gameObject != oggettoDentro)
        {
            // Fai rilasciare l'oggetto dal potere dell'aria
            other.GetComponent<Solleva>().RilasciaOggetto();

            // Se c'è già un oggetto dentro, espellilo
            if (oggettoDentro != null && oggettoDentro != other.gameObject)
            {
                oggettoDentroPrecedente = oggettoDentro;
                oggettoDentro = other.gameObject;

                StartCoroutine(AnnullaOggettoDentroPrecedente());

                EspelliOggetto(oggettoDentroPrecedente);
                //Debug.LogError("Oggetto dentro: " + oggettoDentro.name);
                //Debug.LogError("Oggetto dentro precedente: " + oggettoDentroPrecedente.name);
            }

            // Se non c'è nessun oggetto dentro, aggiorna la variabile
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
        // Caso in cui il player rimuove volontariamente l'oggetto con il potere dell'aria
        if (other.gameObject == oggettoDentro)
        {
            // Rialza il geyser in base al tipo di oggetto
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

    private IEnumerator AnnullaOggettoDentroPrecedente()
    {
        // Attendi un tempo specificato prima di annullare oggettoDentroPrecedente
        yield return new WaitForSeconds(durataAnimazione + durataAnimazione / 2);
        oggettoDentroPrecedente = null;
    }

    private IEnumerator MuoviOggettoAlPuntoSopra(GameObject oggetto)
    {
        Vector3 posizioneIniziale = oggetto.transform.position;
        Vector3 posizioneFinaleXZ = new Vector3(sopra.position.x, posizioneIniziale.y, sopra.position.z);
        Vector3 posizioneFinale = sopra.position;
        float tempoTrascorso = 0f;

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

        // Imposta la posizione finale e disabilita la fisica
        oggetto.transform.position = sopra.position;
        oggetto.GetComponent<Rigidbody>().isKinematic = true;
        oggetto.GetComponent<Rigidbody>().useGravity = false;
    }

    private void EspelliOggetto(GameObject oggetto)
    {
        // Espelli l'oggetto verso il giocatore
        Rigidbody rb = oggetto.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            Vector3 direzioneEspulsione = (player.transform.position - oggetto.transform.position).normalized;
            direzioneEspulsione = new Vector3(direzioneEspulsione.x, Mathf.Max(direzioneEspulsione.y, 0.5f), direzioneEspulsione.z).normalized;
            // Disegna retta di debug
            Debug.DrawRay(oggetto.transform.position, direzioneEspulsione * 10f, Color.red, 2f);
            rb.AddForce(direzioneEspulsione * forzaEspulsione, ForceMode.Impulse);
        }

        Debug.LogError("Oggetto " + oggetto.name + " espulso, geyser abbassato");
        // Rialza il geyser in base al tipo di oggetto
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
