using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GazeReticle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Canvas _canvas;
    [SerializeField] private float _scale = 0.0015f;
    [SerializeField] private float _offset = 0.1f;

    public enum ReticleType {  Default, Visible, Invisible};

    private GazeInteractor _interactor;
    private ReticleType _type;

    void Start()
    {
        _canvas.transform.localScale = Vector3.one * _scale;
    }

    // Update is called once per frame
    void Update()
    {
        if(_interactor == null ) { return; }
        var distance = Vector3.Distance(_interactor.transform.position, transform.position);
        var scale = distance * _scale;
        scale = Mathf.Clamp(scale, _scale, scale);
        _canvas.transform.localScale = Vector3.one * scale;
    }

    public void SetInteractor(GazeInteractor gazeInteractor)
    {
        _interactor = gazeInteractor;
        transform.SetParent(_interactor.transform);
        enabled = true;
    }

    public void SetType(ReticleType type)
    {
        _type = type;

        if (_type == ReticleType.Invisible)
            Enable(false);
    }
    public void Enable(bool enable)
    {
        if (_type == ReticleType.Visible && !enable || _type == ReticleType.Invisible && enable) return;
        gameObject.SetActive(enable);
    }

    public void SetTarget(RaycastHit hit)
    {
        var direction = _interactor.transform.position - hit.point;
        var rotation = Quaternion.FromToRotation(Vector3.forward, direction);
        var targetPosition = hit.point + transform.forward * _offset;

        transform.SetPositionAndRotation(targetPosition, rotation);
    }


}
