using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.Locomotion;
using UnityEngine;

public class SoundMovementManager : MonoBehaviour
{
    [SerializeField]
    private PlayerLocomotor _playerLocomotor;

    [SerializeField]
    private Transform _player;

    /*
    [SerializeField] private FMODUnity.StudioEventEmitter[] _eventEmitters;
    */

    //RIGHE AGGIUNTE DA VIRGINIA - dichiarazione AudioSources
    [Space(10)]
    [Header("Audio Sources")]
    public AudioSource AtterraggioErba;
    public AudioSource AtterraggioSabbia;
    public AudioSource AtterraggioRoccia;

    [Space(10)]
    [Header("Elementi utili per check di movimento in base al terreno e loading scene")]
    public bool firstOnSabbia = false;
    public bool firstOnErba = false;
    [SerializeField] private TutorialManager tutorialManager;

    /*
    private void Start()
    {
        foreach (var emitter in _eventEmitters)
        {
            emitter.Play();
        }
    }
    */

    private void OnEnable()
    {
        _playerLocomotor.WhenLocomotionEventHandled += OnLocomotionEventHandled;
    }

    private void OnDisable()
    {
        _playerLocomotor.WhenLocomotionEventHandled -= OnLocomotionEventHandled;
    }

    private void OnLocomotionEventHandled(LocomotionEvent locomotionEvent, Pose pose)
    {
        // Rileva i collider con cui il player è in contatto
        DetectColliders();
    }

    private void DetectColliders()
    {
        bool audioPlayed = false;

        Collider[] hitColliders = Physics.OverlapSphere(_player.position, 0.1f);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.gameObject.name + " + " + hitCollider.gameObject.tag);

            /*
            FMODUnity.StudioEventEmitter component = GameObject.Find(hitCollider.gameObject.name).GetComponent<FMODUnity.StudioEventEmitter>();
            if (component != null && hitCollider.gameObject.tag != "Roccia")
            {
                component.Play();
            }
            */

            // Gestione del primo contatto con l'erba
            if (hitCollider.gameObject.tag == "Erba" && firstOnErba == false)
            {
                Debug.Log("Primo contatto con l'erba");
                firstOnErba = true;
                tutorialManager.DialogoIntroduzioneGaia();
            }

            //RIGHE AGGIUNTE DA VIRGINIA PER GESTIRE LOGICA EMISSIONE AUDIOSOURCE
            if (hitCollider.gameObject.tag == "Erba")
            {
                PlayAudioSource(AtterraggioErba);
                audioPlayed = true;
            }
            else if (hitCollider.gameObject.tag == "Sabbia")
            {
                PlayAudioSource(AtterraggioSabbia);
                audioPlayed = true;
            }
            else if (hitCollider.gameObject.tag == "Roccia")
            {
                PlayAudioSource(AtterraggioRoccia);
                audioPlayed = true;
            }
        }

        if (!audioPlayed)
        {
            StopAllAudioSources();
        }
    }

    private void PlayAudioSource(AudioSource sourceToPlay)
    {
        StopAllAudioSources();
        if (sourceToPlay != null && !sourceToPlay.isPlaying)
        {
            sourceToPlay.Play();
        }
    }

    private void StopAllAudioSources()
    {
        if (AtterraggioErba != null) AtterraggioErba.Stop();
        if (AtterraggioSabbia != null) AtterraggioSabbia.Stop();
        if (AtterraggioRoccia != null) AtterraggioRoccia.Stop();
    }
}
