using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDelay : MonoBehaviour
{
    [SerializeField]private Platformsmanager platformsManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openPortal()
    {
        if(transform.gameObject.activeSelf) 
            GetComponent<FMODUnity.StudioEventEmitter>().Play();
            StartCoroutine(delayOpenPortal(5));
    }

    IEnumerator delayOpenPortal(int delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        transform.gameObject.SetActive(false);
        platformsManager.OpenNewArea();
    }
}
