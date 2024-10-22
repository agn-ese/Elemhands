using Oculus.Interaction;
using Oculus.Interaction.DistanceReticles;
using Oculus.Interaction.Locomotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class PotereTerra : MonoBehaviour
{
    [Tooltip("The IActiveState to debug.")]
    [SerializeField, Interface(typeof(IActiveState))]
    private UnityEngine.Object _activeState;
    //private IActiveState state { get; set; }

    // prendo il prefab dell'oggetto da evocare che si chiama "OggettoEvocato"
    [SerializeField] private GameObject oggettoEvocato;

    /*     private void Awake()
        {
            state = _activeState as IActiveState;
            this.AssertField(state, nameof(state));
        } */

    public void EvocaOggetto()
    {
        // se premo il tasto F o se premo il tasto B del controller meta quest
        if (/* state.Active || */ Input.GetKeyDown(KeyCode.F) || OVRInput.GetDown(OVRInput.Button.Two))
        {
            // creo un nuovo oggetto che posiziono davanti al giocatore con una rotazione su x di -90 gradi
            GameObject oggetto = Instantiate(oggettoEvocato, transform.position + transform.forward * 2, Quaternion.Euler(-90, 0, 0));
        }
    }
}
