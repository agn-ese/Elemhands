using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDelay : MonoBehaviour
{
    [SerializeField] private Platformsmanager platformsManager;

    public AudioSource portalAudioSource;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public async void openPortal()
    {
        if (transform.gameObject.activeSelf)
        {
            StartCoroutine(delayOpenPortal(5));
            if (portalAudioSource != null)
            {
                portalAudioSource.PlayDelayed(4);
            }
        }

    }

    IEnumerator delayOpenPortal(int delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        // GetComponent<FMODUnity.StudioEventEmitter>().Play();
        platformsManager.OpenNewArea();
        transform.gameObject.SetActive(false);
    }
}
