using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GazeInteractable : MonoBehaviour
{
    private const string WAIT_TO_EXIT_COROUTINE = "WaitToExit_Coroutine";

    public delegate void OnEnter(GazeInteractable interactable, GazeInteractor interactor, Vector3 point);
    public event OnEnter Enter;
    public delegate void OnStay(GazeInteractable interactable, GazeInteractor interactor, Vector3 point);
    public event OnStay Stay;
    public delegate void OnExit(GazeInteractable interactable, GazeInteractor interactor);
    public event OnExit Exit;


    [SerializeField] private float _exitDelay;

    [Tooltip("Unity event triggered when the gaze enters the interactable.")]
    public UnityEvent OnGazeEnter;

    [Tooltip("Unity event triggered while the gaze remains on the interactable.")]
    public UnityEvent OnGazeStay;

    [Tooltip("Unity event triggered when the gaze exits the interactable.")]
    public UnityEvent OnGazeExit;

    [Tooltip("Unity event triggered when the gaze toggle state changes.")]
    public UnityEvent<bool> OnGazeToggle;
    
    public bool IsEnabled
    {
        get { return _collider.enabled; }
        set { _collider.enabled = value; }
    }


    private Collider _collider;



    private void Awake()
    {
        if (!TryGetComponent<Collider>(out _collider))
        {
            Debug.LogError("Missing Collider");
        }
    }
    private void Start()
    {
        enabled = false;
    }

    
    public void Enable(bool enable)
    {
        gameObject.SetActive(enable);
    }


    public void GazeEnter(GazeInteractor interactor, Vector3 point)
    {
        StopCoroutine(WAIT_TO_EXIT_COROUTINE);

        Enter?.Invoke(this, interactor, point);

        OnGazeEnter?.Invoke();
        OnGazeToggle?.Invoke(true);
    }


    public void GazeStay(GazeInteractor interactor, Vector3 point)
    {
        Stay?.Invoke(this, interactor, point);

        OnGazeStay?.Invoke();
    }


    public void GazeExit(GazeInteractor interactor)
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(WAIT_TO_EXIT_COROUTINE, interactor);
        }
        else
        {
            InvokeExit(interactor);
        }
    }

    private IEnumerator WaitToExit_Coroutine(GazeInteractor interactor)
    {
        yield return new WaitForSeconds(_exitDelay);

        InvokeExit(interactor);
    }

    private void InvokeExit(GazeInteractor interactor)
    {
        Exit?.Invoke(this, interactor);

        OnGazeExit?.Invoke();
        OnGazeToggle?.Invoke(false);

    }
}
