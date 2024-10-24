using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDelay : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openPortal()
    {
        StartCoroutine(delayOpenPortal(5));
    }

    IEnumerator delayOpenPortal(int delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        transform.gameObject.SetActive(false);
    }
}
