using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solleva : MonoBehaviour
{
    [Tooltip("The IActiveState to debug.")]
    [SerializeField, Interface(typeof(IActiveState))]
    private UnityEngine.Object _activeState;
    private IActiveState state { get; set; }

    public Transform father;
    Vector3 newPosition = new(0, 0, 3);
    [SerializeField] private bool isInAir = false;
    Rigidbody rigidbody;

    [SerializeField] private TutorialManager manager;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        state = _activeState as IActiveState;
        this.AssertField(state, nameof(state));
    }

    public void SollevaOggetto()
    {
        if(state.Active)
        {
            if(!isInAir)
            {
                transform.SetParent(father);
                transform.localPosition = newPosition;
                isInAir = true;
                rigidbody.useGravity = false;
                rigidbody.isKinematic = true;
                manager.DialogoCassaSollevata();
            } else
            {
                transform.SetParent(null);
                isInAir = false;
                rigidbody.useGravity = true;
                rigidbody.isKinematic = false;
            }
        }
    }
}
