using Oculus.Interaction;
using Oculus.Interaction.DistanceReticles;
using Oculus.Interaction.Locomotion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class Solleva : MonoBehaviour
{
    private IActiveState state { get; set; }

    private Transform father;
    Vector3 newPosition = new(0, 0, 6);
    [SerializeField] private bool isInAir = false;
    [SerializeField] private bool waitTime = false;
    [SerializeField] private float time = 3f;
    Rigidbody rigidbody;

    [SerializeField] private TutorialManager manager;
    [SerializeField] private Transform target;
    [SerializeField] private TeleportInteractable[] teleports;
    [SerializeField] private ReticleDataTeleport[] reticles;
    [SerializeField] private SplineAnimate spline;

    [SerializeField] private AudioSource sound;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        /* state = _activeState as IActiveState;
        this.AssertField(state, nameof(state)); */
    }

    private void Start()
    {
        father = GameObject.Find("GazeInteractor").transform;
        if (father == null)
        {
            Debug.LogError("GazeInteractor not found");
        }

        state = GameObject.Find("Air - Right to Left").GetComponent<IActiveState>();

        if (state == null)
        {
            Debug.LogError("ActiveState not found");
        }

    }

    private void Update()
    {
        if (waitTime)
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
        if (state.Active)
        {
            if (!isInAir && !waitTime)
            {
                waitTime = true;
                transform.SetParent(father);
                transform.localPosition = newPosition;
                isInAir = true;
                rigidbody.useGravity = false;
                rigidbody.isKinematic = true;
                if(manager != null)
                    manager.DialogoCassaSollevata();

            }
            else if (isInAir && !waitTime)
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
                    spline.Play();
                    sound.Play();

                }
                else
                {
                    rigidbody.useGravity = true;
                    rigidbody.isKinematic = false;
                }
                if(manager != null)
                    manager.DialogoTrasportoCassa();
            }
        }
    }
}
