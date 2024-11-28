using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cassa : MonoBehaviour
{
    private bool galleggiamento = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Mare")
        {
            Debug.Log("Cassa tocca il mare");

            // fai galleggiare l'oggetto e tienielo a galla
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            // crea un animazione di galleggiamento con il Lerp
            StartCoroutine(Galleggia(this.gameObject));
            galleggiamento = true;



        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Mare")
        {
            Debug.Log("Cassa esce dal mare");

            // fai tornare l'oggetto a cadere
            //other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            //galleggiamento = false;
        }
    }

    IEnumerator Galleggia(GameObject oggetto)
    {
        while (galleggiamento)
        {
            oggetto.transform.position = Vector3.Lerp(oggetto.transform.position, new Vector3(oggetto.transform.position.x, 0.5f, oggetto.transform.position.z), 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
