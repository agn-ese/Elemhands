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
            StartCoroutine(delayOpenPortal(5));
    }

    IEnumerator delayOpenPortal(int delayTime)
    {
        yield return new WaitForSeconds(delayTime);
       // GetComponent<FMODUnity.StudioEventEmitter>().Play();
        platformsManager.OpenNewArea();
        transform.gameObject.SetActive(false);
    }
}
