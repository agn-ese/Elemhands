using Oculus.Interaction;
using Oculus.Interaction.DistanceReticles;
using Oculus.Interaction.Locomotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

public class PotereTerra : MonoBehaviour
{
    private IActiveState state { get; set; }
    [SerializeField] private bool firstSpawn;
    [SerializeField] private UnityEvent onFirstSpawn;

    // prendo il prefab dell'oggetto da evocare che si chiama "OggettoEvocato"
    [SerializeField] private GameObject oggettoEvocato;
    //[SerializeField] private int massimoOggettiEvocati = 5;
    //private Queue<GameObject> oggettiEvocati = new Queue<GameObject>();

    [SerializeField] private float timeCountdown = 3f;
    private float time;
    [SerializeField] private bool waitTime = false;
    [SerializeField] private FMODUnity.StudioEventEmitter _eventEmitter;

    private void Start()
    {
        firstSpawn = false;

        state = GameObject.Find("Earth").GetComponent<IActiveState>();

        if (state == null)
        {
            Debug.LogError("ActiveState not found");
        }

        time = timeCountdown;
    }

    private void Update()
    {
        if (waitTime)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                waitTime = false;
                time = timeCountdown;
            }
        }
    }

    public void EvocaOggetto()
    {

        if (state.Active && !waitTime)
        {
            waitTime = true;
            if (!firstSpawn)
            {
                firstSpawn = true;
                onFirstSpawn.Invoke();
            }
            // creo un nuovo oggetto che posiziono davanti al giocatore con una rotazione su x di -90 gradi
            GameObject oggetto = Instantiate(oggettoEvocato, transform.position + transform.forward * 2 + Vector3.up * 2, Quaternion.Euler(-90, 0, 0));
            if (_eventEmitter != null)
                _eventEmitter.Play();
            //oggettiEvocati.Enqueue(oggetto);

            // se ci sono più di 5 oggetti evocati, distruggi il primo
            /* if (oggettiEvocati.Count > massimoOggettiEvocati)
            {
                GameObject oggettoDaDistruggere = oggettiEvocati.Dequeue();
                Destroy(oggettoDaDistruggere);
            } */

        }
    }
}
