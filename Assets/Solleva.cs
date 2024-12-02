using Oculus.Interaction;
using Oculus.Interaction.DistanceReticles;
using Oculus.Interaction.Locomotion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

public class Solleva : MonoBehaviour
{
    private IActiveState state { get; set; }

    private Transform father;
    Vector3 newPosition = new(0, 0, 3);
    [SerializeField] private bool isInAir = false;
    [SerializeField] private bool waitTime = false;
    [SerializeField] private float timeCountdown = 3f;
    private float time;
    Rigidbody rigidbody;

    [SerializeField] private TutorialManager manager;
    [SerializeField] private Transform target;
    [SerializeField] private TeleportInteractable[] teleports;
    [SerializeField] private ReticleDataTeleport[] reticles;
    [SerializeField] private SplineAnimate spline;

    [SerializeField] private AudioSource sound;

    [SerializeField] private AudioClip soundGrab;
    [SerializeField] private AudioClip soundLeave;

    [SerializeField] private bool firstGrab;
    [SerializeField] private UnityEvent onFirstGrab;
    [SerializeField] private bool firstRelease;
    [SerializeField] private UnityEvent onFirstRelease;
    //[SerializeField] private FMODUnity.StudioEventEmitter _eventEmitter; // Riferimento FMOD commentato

    private void Awake()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
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

        firstGrab = false;
        firstRelease = false;
        time = timeCountdown;

        soundGrab = Resources.Load<AudioClip>("Sounds/PotereAria");

        soundLeave = Resources.Load<AudioClip>("Sounds/PortaleSiApre");

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


                if (sound != null)
                {
                    sound.clip = soundGrab;
                    sound.Play();
                }

                if (manager != null)
                    manager.DialogoCassaSollevata();

                if (!firstGrab)
                {
                    firstGrab = true;
                    onFirstGrab.Invoke();
                }

                if (this.gameObject.GetComponent<Fluttua>())
                {
                    this.gameObject.GetComponent<Fluttua>().SetFluttuamento(true);
                    this.gameObject.GetComponent<Fluttua>().SetGainFluttuamento(0.0001f);
                }
                else
                {
                    // fai fluttuare l'oggetto
                    this.gameObject.AddComponent<Fluttua>();
                    this.gameObject.GetComponent<Fluttua>().SetFluttuamento(true);
                    this.gameObject.GetComponent<Fluttua>().SetGainFluttuamento(0.0001f);
                }




            }
            else if (isInAir && !waitTime)
            {
                waitTime = true;
                transform.SetParent(null);
                isInAir = false;

                this.gameObject.GetComponent<Fluttua>().SetFluttuamento(false);

                if (sound != null)
                {
                    sound.clip = soundLeave;
                    sound.Play();
                }




                if (!firstRelease)
                {
                    firstRelease = true;
                    onFirstRelease.Invoke();
                }


                if (target && Vector3.Distance(target.position, transform.position) <= 5f)
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


                }
                else
                {
                    rigidbody.useGravity = true;
                    rigidbody.isKinematic = false;

                }


            }
        }
    }

    public void RilasciaOggetto()
    {
        if (isInAir)
        {
            waitTime = true;
            transform.SetParent(null);
            isInAir = false;
            this.gameObject.GetComponent<Fluttua>().SetFluttuamento(false);
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
        }
    }
}

