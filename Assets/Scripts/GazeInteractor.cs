using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GazeReticle;

public class GazeInteractor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _maxDetectionDistance;
    [SerializeField] private float _minDetectionDistance;
    [SerializeField] private float _activationTime = 0.5f; //time required for activation
    [SerializeField] private LayerMask _layerMask; //layer for raycasting

    protected Ray _ray;
    private RaycastHit _hit;

    [SerializeField] private GazeReticle _reticle;
    [SerializeField] private ReticleType _reticleType;
    private GazeInteractable _interactable;

    private float _enterStartTime;
    void Start()
    {
        _reticle.SetInteractor(this);
        _reticle.SetType(_reticleType);
    }

    // Update is called once per frame
    void Update()
    {
        _ray = new Ray(transform.position, _hit.transform.position);
        if (Physics.Raycast(_ray, out _hit, _maxDetectionDistance, _layerMask))
        {
            var distance = Vector3.Distance(transform.position, _hit.transform.position);
            if(distance< _minDetectionDistance)
            {
                _reticle.Enable(false);
                Reset();
                return;
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
