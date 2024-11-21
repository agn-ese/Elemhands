using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.Locomotion;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private PlayerLocomotor _playerLocomotor;

    [SerializeField]
    private Transform _player;

    //[SerializeField] private FMODUnity.StudioEventEmitter[] _eventEmitters;

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
        Collider[] hitColliders = Physics.OverlapSphere(_player.position, 0.1f);
       
        foreach (var hitCollider in hitColliders)
        {
          //  FMODUnity.StudioEventEmitter component = GameObject.Find(hitCollider.gameObject.name).GetComponent<FMODUnity.StudioEventEmitter>();
          //  if(component != null && !hitCollider.gameObject.CompareTag("Roccia")) {
           //     component.Play();
            }
        }
    }

