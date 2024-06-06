using Oculus.Interaction;
using Oculus.Interaction.DistanceReticles;
using Oculus.Interaction.Locomotion;
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
    Vector3 newPosition = new(0, 0, 6);
    [SerializeField] private bool isInAir = false;
    [SerializeField] private bool waitTime = false;
    [SerializeField] private float time = 3f;
    Rigidbody rigidbody;

    [SerializeField] private TutorialManager manager;
    [SerializeField] private Transform target;
    [SerializeField] private TeleportInteractable[] teleports;
    [SerializeField] private ReticleDataTeleport[] reticles;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        state = _activeState as IActiveState;
        this.AssertField(state, nameof(state));
    }

    private void Update()
    {
        if(waitTime)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                waitTime = false;
                time = 5f;
            }
        }
    }

    public void SollevaOggetto()
    {
        if(state.Active)
        {
            if(!isInAir && !waitTime)
            {
                waitTime = true;
                transform.SetParent(father);
                transform.localPosition = newPosition;
                isInAir = true;
                rigidbody.useGravity = false;
                rigidbody.isKinematic = true;
                manager.DialogoCassaSollevata();
                
            } else if(isInAir && !waitTime)
            {
                waitTime = true;
                transform.SetParent(null);
                isInAir = false;
                if (Vector3.Distance(target.position, transform.position) <= 5f)
                {
                    transform.position = target.position;
                    rigidbody.useGravity = false;
                    rigidbody.isKinematic = true;
                    foreach (TeleportInteractable teleport in teleports)
                    {
                        teleport.AllowTeleport = true;
                    }
                    foreach (ReticleDataTeleport reticle in reticles)
                    {
                        reticle.ReticleMode = ReticleDataTeleport.TeleportReticleMode.ValidTarget;
                    }
                } else
                {
                    rigidbody.useGravity = true;
                    rigidbody.isKinematic = false;
                }
                manager.DialogoTrasportoCassa();
            }
        }
    }
}
