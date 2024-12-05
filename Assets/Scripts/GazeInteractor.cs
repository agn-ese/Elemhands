using System.Collections;
using System.Collections.Generic;
using Meta.WitAi.Attributes;
using UnityEngine;
using UnityEngine.UI;
using static GazeReticle;

public class GazeInteractor : MonoBehaviour
{

    [SerializeField] private float _maxDetectionDistance;
    [SerializeField] private float _minDetectionDistance;
    [SerializeField] private LayerMask _layerMask; //layer for raycasting

    protected Ray _ray;
    private RaycastHit _hit;

    [SerializeField] private GazeReticle _reticle;
    [SerializeField] private Image _reticleImageComponent;
    [SerializeField] private Sprite _baseReticle;
    [SerializeField] private Sprite _reticleAir;
    [SerializeField] private Sprite _reticleEarth;
    [SerializeField] private ReticleType _reticleType;
    private GazeInteractable _interactable;

    private float _enterStartTime;
    void Start()
    {
        _reticle.SetInteractor(this);
        _reticle.SetType(_reticleType);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * _hit.distance, Color.red);
        if (Physics.Raycast(_ray, out _hit, _maxDetectionDistance, _layerMask))
        {
            var distance = Vector3.Distance(transform.position, _hit.transform.position);
            if(distance< _minDetectionDistance)
            {
                _reticle.Enable(false);
                Reset();
                return;
            }

            if(_reticleImageComponent != null && _hit.collider.gameObject.layer == LayerMask.NameToLayer("Gaze"))
            {
                if(_hit.collider.gameObject.tag == "Air")
                {
                    _reticleImageComponent.sprite = _reticleAir;
                }
                else if(_hit.collider.gameObject.tag == "Earth")
                {
                    _reticleImageComponent.sprite = _reticleEarth;
                } 
                else
                {
                    _reticleImageComponent.sprite = _baseReticle;
                }
            }

            _reticle.SetTarget(_hit);
            _reticle.Enable(true);

            var interactable = _hit.collider.transform.GetComponent<GazeInteractable>();
            if(interactable == null)
            {
                Reset();
                return;
            }

            if(interactable != _interactable)
            {
                Reset();
                _enterStartTime = Time.time;
                _interactable = interactable;
                _interactable.GazeEnter(this, _hit.point);
            }

            _interactable.GazeStay(this, _hit.point);

            return;
        }
        _reticle.Enable(false);
        Reset();
    }

    private void Reset()
    {
        if(_interactable != null)
        {
            _interactable.GazeExit(this);
            _interactable = null;
        }
    }
}
