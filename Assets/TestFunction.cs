using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class TestFunction : MonoBehaviour
{
    [Tooltip("The IActiveState to debug.")]
    [SerializeField, Interface(typeof(IActiveState))]
    private UnityEngine.Object _activeState;
    private IActiveState state { get; set; }

    private void Awake()
    {
        state = _activeState as IActiveState;
        this.AssertField(state, nameof(state));
    }

    // Update is called once per frame
    void Update()
    {
        if(state.Active)
        {
            Debug.Log("Gesto Fatto");
        }
    }
}
